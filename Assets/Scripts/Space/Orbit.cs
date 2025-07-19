using UnityEngine;

public class Orbit : MonoBehaviour
{
    [Tooltip("How many days take one turn")]
    [SerializeField] private float period;
    private float speed;

    [SerializeField] private Transform target;
    
    [SerializeField] private Axis axis;
    private Vector3 selectedAxis;

    private enum Axis
    {
        Up,
        Right,
        Forward
    }
    
    private void Start()
    {
        Global.OnChangeTimeScale += CalculateSpeed;
        CalculateSpeed();
        
        selectedAxis = axis switch
        {
            Axis.Up => transform.up,
            Axis.Right => transform.right,
            Axis.Forward => transform.forward,
            _ => Vector3.zero
        };
    }

    private void FixedUpdate()
    {
        transform.RotateAround(target.position, selectedAxis, -speed);
    }

    private void OnDestroy()
    {
        Global.OnChangeTimeScale -= CalculateSpeed;
    }

    private void CalculateSpeed()
    {
        speed = (Global.TimeScale) / (86400 * period * Time.fixedDeltaTime);
    }
}
