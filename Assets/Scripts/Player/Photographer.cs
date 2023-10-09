using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Photographer : MonoBehaviour
{
    [SerializeField] PostProcessVolume postProcessVolume;
    private DepthOfField depthOfField;
    private float cameraRatio;

    private const float MIN_APERTURE = 0.1f;
    private const float MAX_APERTURE = 5.6f;

    private const float MIN_FOCAL_LENGTH = 50;
    private const float MAX_FOCAL_LENGTH = 300;

    private void Start()
    {
        postProcessVolume.profile.TryGetSettings(out depthOfField);
    }

    private void Update()
    {
        if (Global.IsPause) return;

        var input = Input.GetAxis("Mouse ScrollWheel");

        if (input != 0) SetupCamera(input);
    }

    public void SetupCamera(float input)
    {
        var newCameraRatio = cameraRatio + input;
        
        if (newCameraRatio > -1 && newCameraRatio < 1) cameraRatio += input;
        else return;

        if (cameraRatio < 0)
        {
            depthOfField.aperture.value = MAX_APERTURE + cameraRatio * (MAX_APERTURE - MIN_APERTURE);
        }
        else if (cameraRatio > 0)
        {
            depthOfField.focalLength.value = MIN_FOCAL_LENGTH + cameraRatio * (MAX_FOCAL_LENGTH - MIN_FOCAL_LENGTH);
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("scroll wheel input: " + Input.GetAxis("Mouse ScrollWheel"));
        GUILayout.Label("camera ratio: " + cameraRatio);
    }
}
