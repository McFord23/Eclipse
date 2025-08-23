using UnityEngine;

public class EclipseMap : MonoBehaviour
{
    public Eclipse eclipse;
    [SerializeField] private GameObject eclipseCheckerPrefab;
    
    [SerializeField] private LayerMask layer;
    private const float MAX_DISTANCE = 50;
    
    [Space(15)]
    [SerializeField] private Player player;
    [SerializeField] private Transform targetPoint;

    private void OnMouseDown()
    {
        if (Global.IsPause) return;
        if (!eclipse.enabled) return;
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray.origin, ray.direction, out hit, MAX_DISTANCE, layer))
        {
            targetPoint.position = hit.point;
            targetPoint.LookAt(transform);

            var checkerGO = Instantiate
            (
                eclipseCheckerPrefab, 
                targetPoint.position, 
                eclipseCheckerPrefab.transform.rotation, 
                transform
            );

            var checker = checkerGO.GetComponent<EclipseChecker>();
            checker.eclipseMap = this;
        }
    }

    public void OnCheckInEclipse()
    {
        player.EnablePhotoMode(targetPoint);
    }
}
