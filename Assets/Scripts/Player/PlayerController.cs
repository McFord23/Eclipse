using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player player;
    private Vector3 rotateDir;

    private void LateUpdate()
    {
        var correction = Global.mouseSens * Time.deltaTime;

        rotateDir.x = Input.GetAxis("Mouse X") * correction;
        rotateDir.y = -Input.GetAxis("Mouse Y") * correction;

        player.RotateView(rotateDir);

        Input.GetAxis("Mouse X");
    }
}
