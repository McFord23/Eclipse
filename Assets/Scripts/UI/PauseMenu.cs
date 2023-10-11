using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private void Start()
    {
        Resume();
    }

    public void MonitoringInput()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        
        if (Global.IsPause) Resume();
        else Pause();
    }

    private void Pause()
    {
        gameObject.SetActive(true);
        Global.IsPause = true;
        Time.timeScale = 0f;
        if (Global.Mode == Mode.Photo) CursorController.Unlock();
    }

    public void Resume()
    {
        gameObject.SetActive(false);
        Global.IsPause = false;
        Time.timeScale = 1f;
        if (Global.Mode == Mode.Photo) CursorController.Lock();
    }
    
    public void Exit()
    {
        var scene = Scene.MainMenu.ToString();
        SceneManager.LoadScene(scene);
    }
}
