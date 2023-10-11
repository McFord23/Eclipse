using UnityEngine;

public class Menu : MonoBehaviour
{
    private bool isPauseMenuNull => pauseMenu == null;
    [SerializeField] private PauseMenu pauseMenu;

    private void Update()
    {
        if (!isPauseMenuNull)
        {
            pauseMenu.MonitoringInput();
        }
    }
}
