using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] PlayerSO data;

    public void AddCommand(Command command) {
        data.commands.Add(command);
    }

    public void UpdateData() {
        if (data.commands.Count == 0) {
            //Debug.Log("вещей нет");
            return;
        }

        //Debug.Log("вещи есть");
        foreach (Command c in data.commands) {
            c.Execute(data, transform, this);
        }
    }
}
