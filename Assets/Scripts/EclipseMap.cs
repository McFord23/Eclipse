using UnityEngine;

public class EclipseMap : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    private const float MAX_DISTANCE = 25;
    
    [SerializeField] private GameObject eclipseCheckerPrefab;
    public Eclipse eclipse;

    [Space(15)]
    [SerializeField] private Player player;
    private Vector3 potentialPhotoPlace;

    private void OnMouseDown()
    {
        if (!eclipse.enabled) return;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray.origin, ray.direction, out hit, MAX_DISTANCE, layer))
        {
            potentialPhotoPlace = hit.point;

            var checkerGO = Instantiate(eclipseCheckerPrefab, potentialPhotoPlace, 
                eclipseCheckerPrefab.transform.rotation, transform);

            var checker = checkerGO.GetComponent<EclipseChecker>();
            checker.eclipseMap = this;
        }
    }

    public void OnCheckInEclipse(float quality)
    {
        player.MakePhoto(quality, potentialPhotoPlace);
    }
}
