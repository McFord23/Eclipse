using UnityEngine;

public class Viewer : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        if (Global.IsPause) return;

        if (Input.GetKey(KeyCode.Mouse1))
        {
            var correction = Global.MouseSens * Time.deltaTime / Time.timeScale;
            transform.RotateAround(target.position, transform.up, 3f * Input.GetAxis("Mouse X") * correction);
            transform.RotateAround(target.position, transform.right, 3f * -Input.GetAxis("Mouse Y") * correction);
            transform.RotateAround(target.position, transform.forward, 3000f * Input.GetAxis("Mouse ScrollWheel") * correction);
        }
    }
}
