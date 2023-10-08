using System.Collections.Generic;
using UnityEngine;

public class Eclipse : MonoBehaviour
{
    public Transform target;
    private Rigidbody rb;

    [SerializeField] private Shadow shadowFull;
    [SerializeField] private Shadow shadowHalf;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 distance;
        Vector3 oldPos = rb.position;

        rb.MovePosition(target.position);
        distance = rb.position - oldPos;

        shadowFull.Move(distance);
        shadowHalf.Move(distance);
    }
}
