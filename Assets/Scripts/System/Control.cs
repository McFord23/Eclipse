using UnityEngine;

public static class Control
{
    public static bool LeftPress => Input.GetKeyDown(KeyCode.Mouse0);
    public static bool RightPress => Input.GetKeyDown(KeyCode.Mouse1);
    public static bool RightHold => Input.GetKey(KeyCode.Mouse1);

    public static Vector2 RotateDirection => new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y")) * Global.MouseSens;
    
    public static float Scrolling => Input.GetAxis("Mouse ScrollWheel") * Global.ScrollSens;

    public static bool Pause => Input.GetKeyDown(KeyCode.Escape);
}
