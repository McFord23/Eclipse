using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject newTripMenu;
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private GameObject equipmentMenu;
    [SerializeField] private GameObject albumMenu;

    private void Start()
    {
        shopMenu.SetActive(false);
        equipmentMenu.SetActive(false);
        albumMenu.SetActive(false);
    }

    public void Play()
    {
        var scene = Scene.Game.ToString();
        SceneManager.LoadScene(scene);
    }

    public void EnabledNewTripMenu()
    {
        gameObject.SetActive(false);
        newTripMenu.SetActive(true);
    }

    public void DisabledNewTripMenu()
    {
        gameObject.SetActive(true);
        newTripMenu.SetActive(false);
    }

    public void EnabledSettingsMenu()
    {
        gameObject.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void DisabledSettingsMenu()
    {
        gameObject.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void EnabledEquipmentMenu()
    {
        newTripMenu.SetActive(false);
        equipmentMenu.SetActive(true);
    }

    public void DisabledEquipmentMenu()
    {
        newTripMenu.SetActive(true);
        equipmentMenu.SetActive(false);
    }

    public void EnabledShopMenu()
    {
        newTripMenu.SetActive(false);
        shopMenu.SetActive(true);
    }

    public void DisabledShopMenu()
    {
        newTripMenu.SetActive(true);
        shopMenu.SetActive(false);
    }

    public void EnabledPhotoAlbumMenu()
    {
        newTripMenu.SetActive(false);
        albumMenu.SetActive(true);
    }

    public void DisabledPhotoAlbumMenu()
    {
        newTripMenu.SetActive(true);
        albumMenu.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
