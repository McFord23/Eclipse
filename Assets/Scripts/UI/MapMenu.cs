using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text timeLabel;
    [SerializeField] private Slider timeSlider;

    private void Start()
    {
        timeSlider.value = Global.TimeScale;
    }

    public void ChangeTimeScale()
    {
        var time = timeSlider.value;
        
        timeLabel.text = time switch
        {
            > 3600 => $"{(int) time / 3600} HRS/S",
            > 60 => $"{(int) time / 60} MINS/S",
            > 1 => $"{(int) time} SECS/S",
            _ => "REAL RATE"
        };
        
        Global.SetTimeScale(time);
    }
}
