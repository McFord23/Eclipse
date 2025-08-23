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
            var input = 200f * Control.Scrolling * Time.deltaTime;
            if (input != 0) SetupCamera(input);
        }

        if (Control.LeftPress && Global.Mode is Mode.Photo)
        {
            if (viewingPhoto) RemovePhoto();
            else StartCoroutine(CapturePhoto());
        }
    }

    private void LateUpdate()
    {
        if (Global.IsPlayerBlocked || Global.IsPause) return;
        RotateView(Control.RotateDirection * Time.deltaTime);
    }
    
    private void RotateView(Vector2 viewDir)
    {
        inputDir.x += viewDir.x;
        inputDir.y += viewDir.y;
        
        transform.localEulerAngles = new Vector3(inputDir.y, inputDir.x, 0);
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
}
