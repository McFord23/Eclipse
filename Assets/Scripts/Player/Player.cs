using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject view;
    private Vector3 inputDir;

    private static Player instance;

    public State state;
    private State savedState;

    public enum State
    {
        Pause,
        Idle
    };

    private void Start()
    {
        inputDir = new Vector3(transform.localEulerAngles.y, 0f, 0f);
        Idle();
    }

    public void Idle()
    {
        state = State.Idle;
    }

    public void RotateView(Vector3 viewDir)
    {
        if (state != State.Pause)
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

    public void Pause()
    {
        savedState = state;
        state = State.Pause;
    }

    public void Resume()
    {
        state = savedState;
    }
}
