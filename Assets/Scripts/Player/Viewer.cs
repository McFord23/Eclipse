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
        
        if (Control.RightHold)
        {
            RotateCameraAround();
        }
        else if (Control.Scrolling != 0)
        {
            ChangeScale(Control.Scrolling);
        }
    }

    private void RotateCameraAround()
    {
        var mouseCorrection = 3f * Time.deltaTime / Time.timeScale;
        var scrollCorrection = 3000f * Time.deltaTime / Time.timeScale;
        
        transform.RotateAround(target.position, transform.up, Control.RotateDirection.x * mouseCorrection);
        transform.RotateAround(target.position, transform.right, Control.RotateDirection.y * mouseCorrection);
        transform.RotateAround(target.position, transform.forward, Control.Scrolling * scrollCorrection);
    }
    
    private void ChangeScale(float input)
    {
        var isDistancing = input < 0;
        
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
