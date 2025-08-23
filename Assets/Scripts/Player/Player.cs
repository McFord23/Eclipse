using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform moon;
    
    private Photographer photographer;
    private Viewer viewer;

    private Vector3 viewerPos;
    private Quaternion viewerRot;

    private IEnumerator posTransition;
    private IEnumerator rotTransition;

    private void Start()
    {
        photographer = GetComponent<Photographer>();
        viewer = GetComponent<Viewer>();
        
        photographer.enabled = false;
        viewer.enabled = true;
    }

    private void Update()
    {
        if (Control.RightPress && Global.Mode is Mode.Photo && !Global.IsPause)
        {
            ReturnToMap();
        }
    }

    public void EnablePhotoMode(Transform targetPoint)
    {
        viewerPos = transform.position;
        viewerRot = transform.rotation;

        posTransition = PosTransition(targetPoint);
        StartCoroutine(posTransition);
        
        viewer.enabled = false;
        photographer.enabled = true;
        
        CursorController.Lock();
        Global.SetMode(Mode.Photo);
    }

    public void ReturnToMap()
    {
        transform.position = viewerPos;
        transform.rotation = viewerRot;
                
        photographer.enabled = false;
        viewer.enabled = true;

        CursorController.Unlock();
        Global.SetMode(Mode.Map);
    }

    private IEnumerator PosTransition(Transform target)
    {
        float speed = 0;
        var startPos = transform.position;
        
        while (Vector3.Distance(transform.position, target.position) > 0.01f)
        {
            speed += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, target.position, speed);
            yield return null;
        }

        transform.position = target.position;
        posTransition = null;
        UpdateBlock();
        
        print("Pos unblocked");
    }
    
    private IEnumerator RotTransition(Quaternion rot)
    {
        float speed = 0;
        var startRot = transform.rotation;

        while (Quaternion.Angle(transform.rotation, rot) > 1f)
        {
            speed += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(startRot, rot, speed);
            
            print("Current: " + transform.rotation);
            print("Target: " + rot);
            
            yield return null;
        }

        transform.rotation = rot;
        rotTransition = null;
        UpdateBlock();
        
        print("Rot unblocked");
        
        transform.LookAt(moon);
    }

    private void UpdateBlock()
    {
        var posBlock = (posTransition != null);
        var rotBlock = (rotTransition != null);
        Global.IsPlayerBlocked = posBlock && rotBlock;
    }
    
    private void OnGUI()
    {
        GUILayout.Label("pos transition: " + (posTransition != null));
        GUILayout.Label("rot transition: " + (rotTransition != null));
    }
}
