using UnityEngine;
using UnityEngine.SceneManagement;

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
        Global.Mode = Mode.Photo;
        SceneManager.LoadScene("Photo");
    }

    public void Move(Vector3 distance)
    {
        rb.MovePosition(rb.position + distance);
    }
}
