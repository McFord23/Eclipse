using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider mouseSensSlider;
    [SerializeField] private Slider scrollSensSlider;
    
    private void Start()
    {
        mouseSensSlider.value = Global.MouseSens;
        scrollSensSlider.value = Global.ScrollSens;
    }
    
    public void ChangeMouseSens(Slider slider)
    {
        Global.MouseSens = slider.value;
    }
    
    public void ChangeScrollSens(Slider slider)
    {
        Global.ScrollSens = slider.value;
    }
}
