using UnityEngine;

public class InLineMonitoring : MonoBehaviour
{
    [SerializeField] private GameObject eclipsePrefab;
    private GameObject eclipse;

    [SerializeField] private Transform target;

    [SerializeField] private Transform from;
    [SerializeField] private Transform to;
    
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask checkLayer;

    private bool isInLine;

    private void FixedUpdate()
    {
        CheckInLine();
    }

    private void CheckInLine()
    {
        RaycastHit hit;
        Vector3 direction = (to.position - from.position).normalized;

        if (Physics.Raycast(from.position, direction, out hit, maxDistance, checkLayer))
        {
            if (!isInLine && hit.transform.name == target.name)
            {
                EclipseBegin();
            }
        }
        else
        {
            if (isInLine) EclipseEnd();
        }
    }

    private void EclipseBegin()
    {
        isInLine = true;
        eclipse = Instantiate(eclipsePrefab, eclipsePrefab.transform.position, eclipsePrefab.transform.rotation);
        eclipse.GetComponent<Eclipse>().target = target;
    }

    private void EclipseEnd()
    {
        isInLine = false;
        Destroy(eclipse);
    }
}
