using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Photographer : MonoBehaviour
{
    [SerializeField] private Transform view;
    private Vector3 inputDir;
    
    [SerializeField] private PostProcessVolume postProcessVolume;
    private DepthOfField depthOfField;
    private float cameraRatio;

    private const float MIN_APERTURE = 0.1f;
    private const float MAX_APERTURE = 5.6f;

    private const float MIN_FOCAL_LENGTH = 50;
    private const float MAX_FOCAL_LENGTH = 300;

    private void Start()
    {
        inputDir = transform.localEulerAngles;
        
        postProcessVolume.profile.TryGetSettings(out depthOfField);
    }

    private void Update()
    {
        if (Global.IsPause) return;

        var correction = 200f * Global.MouseSens * Time.deltaTime;
        var input = Input.GetAxis("Mouse ScrollWheel") * correction;

        if (input != 0) SetupCamera(input);
    }
    
    private void LateUpdate()
    {
        if (Global.IsPause) return;

        var correction = Global.MouseSens * Time.deltaTime;

        var rotateDir = new Vector3
        {
            x = -Input.GetAxis("Mouse X") * correction,
            y = -Input.GetAxis("Mouse Y") * correction
        };

        RotateView(rotateDir);
    }
    
    private void RotateView(Vector3 viewDir)
    {
        inputDir.x += viewDir.x;
        inputDir.y += viewDir.y;

        /*var newDir = inputDir.y + viewDir.y;
        if (newDir > -70 && newDir < 80)
        {
            inputDir = new Vector3(inputDir.x, newDir, 0);
        }*/

        transform.localEulerAngles = new Vector3(0, 0, inputDir.x);
        view.localEulerAngles = new Vector3(inputDir.y, 0, 0);
    }

    private void SetupCamera(float input)
    {
        var newCameraRatio = cameraRatio + input;
        
        if (newCameraRatio is > -1 and < 1) cameraRatio += input;
        else return;

        switch (cameraRatio)
        {
            case < 0:
                depthOfField.aperture.value = MAX_APERTURE + cameraRatio * (MAX_APERTURE - MIN_APERTURE);
                break;
            
            case > 0:
                depthOfField.focalLength.value = MIN_FOCAL_LENGTH + cameraRatio * (MAX_FOCAL_LENGTH - MIN_FOCAL_LENGTH);
                break;
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("scroll wheel input: " + Input.GetAxis("Mouse ScrollWheel"));
        GUILayout.Label("camera ratio: " + cameraRatio);
    }
}
