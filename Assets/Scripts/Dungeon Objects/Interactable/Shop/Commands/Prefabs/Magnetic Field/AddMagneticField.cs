using UnityEngine;

public class AddMagneticField : Command
{
    public GameObject magneticField;
    
    public override void Execute(PlayerSO data, Transform player, Equipment equipment) {
        if (magneticField == null) return;

        GameObject obj;
        obj = Instantiate(magneticField, player.position, Quaternion.identity);
        obj.transform.SetParent(player);
        if (!isActive)
            equipment.AddCommand(this);
        isActive = true;
    }
}
