using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public bool redKey;
    public bool greenKey;
    public bool whiteKey;

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
