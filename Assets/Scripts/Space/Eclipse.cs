using System.Collections.Generic;
using UnityEngine;

public class Eclipse : MonoBehaviour
{
    private const float ECLIPSE_MAX_RADIUS = 6f;
    private const float RADIUS_SUN = 273.25f;

    [SerializeField] private TotalMesh totalMesh;
    
    [Space(15)]
    [SerializeField] private List<GameObject> triggers;

    [Space(15)]
    [SerializeField] private Transform sun;
    [SerializeField] private Transform planet;
    [SerializeField] private float radiusPlanet;

    [Header("Test")]
    [SerializeField] private Transform testObject;

    public void Start()
    {
        Global.OnChangeMode += OnChangeMode;
    }

    private void FixedUpdate()
    {
        CalculateTotalCross();
    }

    public void SetActiveTriggers(bool value)
    {
        foreach (var trigger in triggers)
        {
            trigger.SetActive(value);
        }
    }
    
    public float GetQuality(Vector3 _playerPos)
    {
        var playerPos = new Vector2(_playerPos.x, _playerPos.y);
        var eclipsePos = new Vector2(transform.position.x, transform.position.y);
        var distance = Vector2.Distance(playerPos, eclipsePos);

        return ECLIPSE_MAX_RADIUS / Mathf.Min(distance, 0.001f);
    }

    private void OnChangeMode(Mode mode)
    {
        switch (mode)
        {
            case Mode.Map:
                gameObject.SetActive(true);
                SetActiveTriggers(true);
                break;
            
            case Mode.Photo:
                gameObject.SetActive(false);
                SetActiveTriggers(false);
                break;
        }
    }
    
    private void CalculateTotalCross()
    {
        var sunPos = sun.position;
        var sunUpPoint = new Vector3(sunPos.x, sunPos.y + RADIUS_SUN, sunPos.z);
        var sunDownPoint = new Vector3(sunPos.x, sunPos.y - RADIUS_SUN, sunPos.z);

        var planetPos = transform.position;
        var planetUpPoint = new Vector3(planetPos.x, planetPos.y + radiusPlanet, planetPos.z);
        var planetDownPoint = new Vector3(planetPos.x, planetPos.y - radiusPlanet, planetPos.z);

        var crossPoint = GetCrossPoint(sunUpPoint, planetUpPoint, sunDownPoint, planetDownPoint);

        if (crossPoint != Vector3.zero)
        {
            totalMesh.SetCrossPos(crossPoint);
            //testObject.position = crossPoint;
        }
    }

    private Vector3 GetCrossPoint(Vector3 a0, Vector3 a1, Vector3 b0, Vector3 b1)
    {
        var uxy = (b0.x - a0.x - (b0.y - a0.y) / (a1.y - a0.y) * (a1.x - a0.x)) / ((b1.y - b0.y) / (a1.y - a0.y) * (a1.x - a0.x) - (b1.x - b0.x));
        var uxz = (b0.x - a0.x - (b0.z - a0.z) / (a1.z - a0.z) * (a1.x - a0.x)) / ((b1.z - b0.z) / (a1.z - a0.z) * (a1.x - a0.x) - (b1.x - b0.x));
        var uyz = (b0.y - a0.y - (b0.z - a0.z) / (a1.z - a0.z) * (a1.y - a0.y)) / ((b1.z - b0.z) / (a1.z - a0.z) * (a1.y - a0.y) - (b1.y - b0.y));

        var u = !float.IsNaN(uxy) ? uxy : (!float.IsNaN(uxz) ? uxz : uyz);
        
        var tx = (b0.x + u * (b1.x - b0.x) - a0.x) / (a1.x - a0.x);
        var ty = (b0.y + u * (b1.y - b0.y) - a0.y) / (a1.y - a0.y);
        var tz = (b0.z + u * (b1.z - b0.z) - a0.z) / (a1.z - a0.z);

        var t = !float.IsNaN(tx) ? tx : (!float.IsNaN(ty) ? ty : tz);

        return (a1 - a0) * t + a0;
    }
}
