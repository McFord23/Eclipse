using UnityEngine;
using UnityEngine.UI;

public class MapMenu : MonoBehaviour
{
    [SerializeField] private Slider timeSlider;

    public void ChangeTimeScale(Slider slider)
    {
        Time.timeScale = slider.value;
    }
}
