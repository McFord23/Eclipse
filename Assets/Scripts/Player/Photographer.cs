using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class Photographer : MonoBehaviour
{
    [SerializeField] private Transform earth;
    
    [SerializeField] private Transform view;
    private Vector3 inputDir;

    [SerializeField] private GameObject hud;
    
    [SerializeField] private PostProcessVolume postProcessVolume;
    private DepthOfField depthOfField;
    private float cameraRatio;

    private const float MIN_APERTURE = 0.1f;
    private const float MAX_APERTURE = 5.6f;

    private const float MIN_FOCAL_LENGTH = 50;
    private const float MAX_FOCAL_LENGTH = 300;

    [Header("Photo Taker")]
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame;
    [SerializeField] private AudioSource photoSound;

    [Header("FlashEffect")]
    [SerializeField] private Animator cameraFlash;

    [Header("Photo Fader Effect")]
    [SerializeField] private Animator fade;

    private Texture2D screenCapture;
    private bool viewingPhoto;

    private void Start()
    {
        inputDir = new (transform.localEulerAngles.y, 0, 0);
        
        postProcessVolume.profile.TryGetSettings(out depthOfField);
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    }

    private void Update()
    {
        if (Global.IsPlayerBlocked || Global.IsPause) return;

        if (!viewingPhoto)
        {
            var correction = 200f * Global.MouseSens * Time.deltaTime;
            var input = Input.GetAxis("Mouse ScrollWheel") * correction;
            if (input != 0) SetupCamera(input);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && Global.Mode is Mode.Photo)
        {
            if (viewingPhoto) RemovePhoto();
            else StartCoroutine(CapturePhoto());
        }
    }

    private void FixedUpdate()
    {
        if (Global.IsPlayerBlocked || Global.IsPause) return;
        
        Quaternion rotation = Quaternion.FromToRotation(-transform.up, earth.position - transform.position);
        transform.rotation *= rotation;
    }

    private void LateUpdate()
    {
        if (Global.IsPlayerBlocked || Global.IsPause) return;

        var correction = Global.MouseSens * Time.deltaTime;

        var rotateDir = new Vector3
        {
            x = Input.GetAxis("Mouse X") * correction,
            y = -Input.GetAxis("Mouse Y") * correction
        };

        RotateView(rotateDir);
    }
    
    private void RotateView(Vector3 viewDir)
    {
        //inputDir = new Vector3(transform.localEulerAngles.y, 0, transform.localEulerAngles.x);
        inputDir.x += viewDir.x;
        inputDir.y += viewDir.y;
        
        transform.localEulerAngles = new Vector3(inputDir.y, inputDir.x, 0);
        //view.localEulerAngles = new Vector3(inputDir.y, 0, 0);

        /*var newDir = inputDir.y + viewDir.y;
        if (newDir > -70 && newDir < 80)
        {
            inputDir = new Vector3(inputDir.x, newDir, 0);
        }*/
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

    private IEnumerator CapturePhoto()
    {
        hud.SetActive(false);
        viewingPhoto = true;

        yield return new WaitForEndOfFrame();

        var regionToRead = new Rect(0, 0, Screen.width, Screen.height);
        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();
        ShowPhoto();
        
        photoSound.Play();
    }

    private void ShowPhoto()
    {
        var photoSprite = Sprite.Create
        (
            screenCapture, 
            new Rect(0f, 0f, screenCapture.width, screenCapture.height), 
            new Vector2(0.5f, 0.5f), 
            100f
        );

        photoDisplayArea.sprite = photoSprite;
        
        photoFrame.SetActive(true);
        cameraFlash.Play("Flash");
        fade.Play("Fade");
    }
    
    private void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);
        hud.SetActive(true);
    }
    
    private void OnGUI()
    {
        GUILayout.Label("scroll wheel input: " + Input.GetAxis("Mouse ScrollWheel"));
        GUILayout.Label("camera ratio: " + cameraRatio);
    }
}
