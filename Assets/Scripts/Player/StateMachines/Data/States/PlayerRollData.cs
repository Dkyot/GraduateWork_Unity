using System;
using UnityEngine;

[Serializable]
public class PlayerRollData
{
    [field: SerializeField] [field: Range(1f, 10f)] public float rollSpeedMultiplier { get; private set;} = 1.5f;
    [field: SerializeField] [field: Range(1f, 100f)] public float rollSpeed { get; private set;} = 60f;
    [field: SerializeField] [field: Range(1f, 100f)] public float rollSpeedMinimum { get; private set;} = 10f;
}
