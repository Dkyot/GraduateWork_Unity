using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counter;
    [SerializeField] private CoinStorage coins;

    private void Start() {
        counter = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update() {
        if (coins != null)
            counter.text = coins.GetCoinAmount().ToString();
    }
}
