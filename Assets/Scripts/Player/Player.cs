using System;
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
        if (Input.GetKey(KeyCode.Mouse1) && Global.Mode is Mode.Photo && !Global.IsPause)
        {
            ReturnToMap();
        }
    }

    public void MakePhoto(float eclipseQuality, Vector3 place)
    {
        viewerPos = transform.position;
        viewerRot = transform.rotation;

        posTransition = PosTransition(place);
        StartCoroutine(posTransition);

        /*var tempObject = new GameObject();
        var tempTransform = tempObject.transform;
        tempTransform.position = transform.position;
        tempTransform.rotation = transform.rotation;
        tempTransform.LookAt(moon);
        var rotation = tempTransform.rotation;
        Destroy(tempObject);
        
        rotTransition = RotTransition(rotation);
        StartCoroutine(rotTransition);*/

        //transform.LookAt(moon, Vector3.back);
        //transform.rotation *= Quaternion.FromToRotation(Vector3.left, Vector3.forward);

        //Vector3 relativePos =  moon.position - transform.position;
        //Quaternion rotation = Quaternion.LookRotation(relativePos, new Vector3(0,1,0));
        //transform.rotation = rotation*Quaternion.Euler(0,90,0);
        
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

    private IEnumerator PosTransition(Vector3 pos)
    {
        var speed = 10f * Time.deltaTime;
        
        while (Vector3.Distance(transform.position, pos) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, pos, speed);
            yield return null;
        }

        transform.position = pos;
        posTransition = null;
        UpdateBlock();
        
        print("Pos unblocked");
    }
    
    private IEnumerator RotTransition(Quaternion rot)
    {
        var speed = 30 * Time.deltaTime;

        while (Quaternion.Angle(transform.rotation, rot) > 1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, speed);
            
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
}
