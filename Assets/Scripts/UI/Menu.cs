using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject pauseMenu;

    private void Start()
    {
        //Application.targetFrameRate = 60;
        if (pauseMenu.activeSelf) Resume();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf) Resume();
            else Pause();
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        player.Pause();
        CursorController.Unlock();
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        player.Resume();
        pauseMenu.SetActive(false);
        CursorController.Lock();
        Time.timeScale = 1f;
    }

    public void ChangeMouseSens(Slider slider)
    {
        Global.mouseSens = slider.value;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
