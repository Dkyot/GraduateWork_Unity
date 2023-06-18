using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public new AudioSource audio;

    public SettingsSO settings;

    private void Awake() {
        SetVolume();
    }
    
    public void SetVolume() {
        audio.volume = settings.soundVolume;
    }
}
