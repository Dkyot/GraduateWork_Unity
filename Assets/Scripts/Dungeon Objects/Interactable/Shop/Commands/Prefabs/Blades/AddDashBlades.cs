using UnityEngine;

public class AddDashBlades : Command
{
    public GameObject blades;
    
    public override void Execute(PlayerSO data, Transform player, Equipment equipment) {
        if (blades == null) return;

        GameObject obj;
        obj = Instantiate(blades, player.position, Quaternion.identity);
        obj.transform.SetParent(player);
        if (!isActive)
            equipment.AddCommand(this);
        isActive = true;
    }
}
