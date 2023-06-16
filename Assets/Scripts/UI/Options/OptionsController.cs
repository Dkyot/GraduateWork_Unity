using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public SettingsSO settings;
    public Slider slider;
    public Toggle toggle;

    public void SetValues() {
        slider.value = settings.soundVolume;
        toggle.isOn = settings.showFPS;
    }

    public void SaveVolume() {
        settings.soundVolume = slider.value;
    }

    public void SwitchBoolFPS() {
        settings.showFPS = toggle.isOn;
    }
}
