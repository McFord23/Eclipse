using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float angle;
    [SerializeField] private Vector3 axis;

    Rigidbody rb;
    Quaternion q;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        q = Quaternion.AngleAxis(angle, axis);
    }

    private void FixedUpdate()
    {
        q = Quaternion.AngleAxis(angle, axis);

        rb.MovePosition(q * (transform.position - target.position) + target.position);
        rb.MoveRotation(transform.rotation * q);
    }
}
