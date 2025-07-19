using UnityEngine;

public class Viewer : MonoBehaviour
{
    private const float TRANSPORT_DISTANCE = 60;
    private const float PLANET_DISTANCE = 120;
    private const float SOLAR_DISTANCE = 6000;
    
    [SerializeField] private Transform sun;
    [SerializeField] private Transform earth;
    private Transform earthAxis;
    
    private Transform target;

    private void Start()
    {
        earthAxis = earth.parent;
        
        switch (Global.MapScale)
        {
            case Scale.Transport:
                TransportScale();
                break;
            
            case Scale.Planet:
                PlanetScale();
                break;
            
            case Scale.Solar:
                SolarScale();
                break;
        }
    }

    private void Update()
    {
        if (Global.IsPlayerBlocked || Global.IsPause) return; 
        
        if (Input.GetKey(KeyCode.Mouse1))
        {
            RotateCameraAround();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            ChangeScale(-Input.GetAxis("Mouse ScrollWheel"));
        }
    }

    private void RotateCameraAround()
    {
        var mouseCorrection = 3f * Global.MouseSens * Time.deltaTime / Time.timeScale;
        var scrollCorrection = 3000f * Global.ScrollSens * Time.deltaTime / Time.timeScale;
            
        transform.RotateAround(target.position, transform.up, Input.GetAxis("Mouse X") * mouseCorrection);
        transform.RotateAround(target.position, transform.right, -Input.GetAxis("Mouse Y") * mouseCorrection);
        transform.RotateAround(target.position, transform.forward, Input.GetAxis("Mouse ScrollWheel") * scrollCorrection);
    }
    
    private void ChangeScale(float input)
    {
        var isDistancing = input > 0;
        
        switch (Global.MapScale)
        {
            case Scale.Transport:
                if (isDistancing) PlanetScale();
                break;
            
            case Scale.Planet:
                if (isDistancing) SolarScale();
                else TransportScale();
                break;
            
            case Scale.Solar:
                if (!isDistancing) PlanetScale();
                break;
        }
    }

    private void TransportScale()
    {
        var eulerAngles = transform.eulerAngles;
        eulerAngles.z = -23.44f;
        transform.eulerAngles = eulerAngles;

        transform.position = earth.position + new Vector3(0, 0, TRANSPORT_DISTANCE);
                    
        target = earth;
        transform.parent = target;
        transform.LookAt(target);
        
        Global.SetMapScale(Scale.Transport);
    }

    private void PlanetScale()
    {
        var eulerAngles = transform.eulerAngles;
        eulerAngles.z = 0;
        transform.eulerAngles = eulerAngles;

        transform.position = earthAxis.position + new Vector3(0, 0, PLANET_DISTANCE);
                    
        target = earthAxis;
        transform.parent = target;
        transform.LookAt(target);
        
        Global.SetMapScale(Scale.Planet);
    }

    private void SolarScale()
    {
        transform.position = sun.position + new Vector3(0, 0, SOLAR_DISTANCE);
                    
        target = sun;
        transform.parent = target;
        transform.LookAt(target);
        
        Global.SetMapScale(Scale.Solar);
    }
}
