using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "sett_", menuName = "sett")]
public class SettingsSO : ScriptableObject
{
    public float soundVolume = 1;
    public bool showFPS = false;
}
