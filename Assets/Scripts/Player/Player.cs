using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject view;
    private Vector3 inputDir;

    public Mode mode { get; private set; }

    private void Start()
    {
        inputDir = new Vector3(transform.localEulerAngles.y, 0f, 0f);
    }

    public void RotateView(Vector3 viewDir)
    {
        inputDir.x += viewDir.x;

        var newDir = inputDir.y + viewDir.y;
        if (newDir > -70 && newDir < 80)
        {
            inputDir = new Vector3(inputDir.x, newDir, 0);
        }

        transform.localEulerAngles = new Vector3(0f, inputDir.x, 0f);
        view.transform.localEulerAngles = new Vector3(inputDir.y, 0f, 0f);
    }
}
