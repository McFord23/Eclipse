using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private PauseMenu pauseMenu;

    private void Update()
    {
        if (pauseMenu)
        {
            pauseMenu.MonitoringInput();
        }
    }
}
