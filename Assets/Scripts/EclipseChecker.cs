using System.Collections;
using UnityEngine;

public class EclipseChecker : MonoBehaviour
{
    public EclipseMap eclipseMap { set; private get; }

    private void Start()
    {
        eclipseMap.eclipse.SetActiveTriggers(true);
        StartCoroutine(WaitingCollision());
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var quality = eclipseMap.eclipse.GetQuality(transform.position);
        eclipseMap.eclipse.gameObject.SetActive(false);
        eclipseMap.OnCheckInEclipse(quality);
        Delete();
    }

    private void Delete()
    {
        eclipseMap.eclipse.SetActiveTriggers(false);
        Destroy(gameObject);
    }

    private IEnumerator WaitingCollision()
    {
        yield return new WaitForFixedUpdate();
        
        Delete();
    }
}
