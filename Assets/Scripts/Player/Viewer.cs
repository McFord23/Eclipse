using UnityEngine;

public class Viewer : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        if (Global.IsPause) return;
        if (!Input.GetKey(KeyCode.Mouse1)) return;

        var mouseCorrection = 3f * Global.MouseSens * Time.deltaTime / Time.timeScale;
        var scrollCorrection = 3000f * Global.ScrollSens * Time.deltaTime / Time.timeScale;
            
        transform.RotateAround(target.position, transform.up, Input.GetAxis("Mouse X") * mouseCorrection);
        transform.RotateAround(target.position, transform.right, -Input.GetAxis("Mouse Y") * mouseCorrection);
        transform.RotateAround(target.position, transform.forward, Input.GetAxis("Mouse ScrollWheel") * scrollCorrection);
    }
}
