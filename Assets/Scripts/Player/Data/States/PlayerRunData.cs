using System;
using UnityEngine;

[Serializable]
public class PlayerRunData
{
    [field: SerializeField] [field: Range(1f, 2f)] public float speedModifier { get; private set;} = 1f;
    // [SerializeField]
    // [Range(1f, 2f)]
    // private float stateMachine.reusableData.speedModifier = 1f;

    // public float stateMachine.reusableData.speedModifier { 
    //     get {return stateMachine.reusableData.speedModifier;}
    //     private set {stateMachine.reusableData.speedModifier = value;}
    // }

}
