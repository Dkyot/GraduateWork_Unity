using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public bool redKey   {get; private set;}
    public bool greenKey {get; private set;}
    public bool whiteKey {get; private set;}

    public void PickKey(KeyColors pickedKey) {
        switch (pickedKey) {
            case KeyColors.Red: redKey = true; break;
            case KeyColors.Green: greenKey = true; break;
            case KeyColors.White: whiteKey = true; break;
            default: return;
        }
    }
}

public enum KeyColors {Red, Green, White};
