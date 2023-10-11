using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform moon;
    
    private Photographer photographer;
    private Viewer viewer;

    private Vector3 viewerPos;
    private Quaternion viewerRot;
    
    private void Start()
    {
        photographer = GetComponent<Photographer>();
        viewer = GetComponent<Viewer>();
        
        photographer.enabled = false;
        viewer.enabled = true;

        Global.Mode = Mode.Map;
    }

    public void MakePhoto(float eclipseQuality, Vector3 place)
    {
        viewerPos = transform.position;
        viewerRot = transform.rotation;

        transform.position = place;
        transform.LookAt(moon);
            
        viewer.enabled = false;
        photographer.enabled = true;
        
        CursorController.Lock();
        Global.Mode = Mode.Photo;
    }

    public void ReturnToMap()
    {
        transform.position = viewerPos;
        transform.rotation = viewerRot;
                
        photographer.enabled = false;
        viewer.enabled = true;

        CursorController.Unlock();
        Global.Mode = Mode.Map;
    }
}
