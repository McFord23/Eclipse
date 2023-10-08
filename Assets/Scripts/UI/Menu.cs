using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
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
        Global.IsPause = true;
        if (Global.Mode == Mode.Photo) CursorController.Unlock();
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Global.IsPause = false;

        if (Global.Mode == Mode.Photo) CursorController.Lock();
        Time.timeScale = 1f;
    }

    public void ChangeMouseSens(Slider slider)
    {
        Global.MouseSens = slider.value;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
