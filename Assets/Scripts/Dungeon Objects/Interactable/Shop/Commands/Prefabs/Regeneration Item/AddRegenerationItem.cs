using UnityEngine;

public class AddRegenerationItem : Command
{
    public GameObject regenerationItem;
    
    public override void Execute(PlayerSO data, Transform player, Equipment equipment) {
        if (regenerationItem == null) return;

        GameObject obj;
        obj = Instantiate(regenerationItem, player.position, Quaternion.identity);
        obj.transform.SetParent(player);
        if (!isActive)
            equipment.AddCommand(this);
        isActive = true;
    }
}
