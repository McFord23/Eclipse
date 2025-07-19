using UnityEngine;

public class InLineMonitoring : MonoBehaviour
{
    [SerializeField] private Transform sun;
    [SerializeField] private Transform moon;
    [SerializeField] private Transform earth;
    private bool isInLine;
    
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask checkLayer;

    [SerializeField] private GameObject eclipse;

    private void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 direction = (earth.position - sun.position).normalized;

        if (Physics.Raycast(sun.position, direction, out hit, maxDistance, checkLayer))
        {
            if (!isInLine && hit.transform.name == moon.name)
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
        eclipse.SetActive(true);
    }

    private void EclipseEnd()
    {
        isInLine = false;
        eclipse.SetActive(false);
    }
}
