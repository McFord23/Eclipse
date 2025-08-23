using System.Collections;
using UnityEngine;

public class EclipseChecker : MonoBehaviour
{
    public EclipseMap eclipseMap { set; private get; }

    private void Start()
    {
        eclipseMap.eclipse.SetActiveTriggers(true);
        StartCoroutine(WaitingCollision());
        
        OnTriggerEnter(null);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        eclipseMap.OnCheckInEclipse();
    }

    private void Delete()
    {
        Destroy(gameObject);
    }

    private IEnumerator WaitingCollision()
    {
        yield return new WaitForFixedUpdate();
        
        Delete();
    }
}
