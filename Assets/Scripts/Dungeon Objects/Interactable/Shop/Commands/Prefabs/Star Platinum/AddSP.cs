using UnityEngine;

public class AddSP : Command
{
    public GameObject sp;
    
    public override void Execute(PlayerSO data, Transform player, Equipment equipment) {
        if (sp == null) return;

        GameObject obj;
        obj = Instantiate(sp, player.position, Quaternion.identity);
        obj.transform.SetParent(player);
        if (!isActive)
            equipment.AddCommand(this);
        isActive = true;
    }
}
