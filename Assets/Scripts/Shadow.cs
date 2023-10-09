using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] private Type type;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private enum Type
    {
        Full,
        Half
    }

    private void OnMouseDown()
    {
        switch (type)
        {
            case Type.Half:
                print("Half");
                break;

            case Type.Full:
                print("Full");
                break;
        }
    }

    public void Move(Vector3 distance)
    {
        rb.MovePosition(rb.position + distance);
    }
}
