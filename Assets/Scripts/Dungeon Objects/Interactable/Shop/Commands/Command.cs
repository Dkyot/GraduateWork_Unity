using UnityEngine;

public abstract class Command : MonoBehaviour
{
    public bool isActive;
    public abstract void Execute(PlayerSO data, Transform player, Equipment equipment);
}
