using UnityEngine;
using UnityEngine.Events;

public class CoinStorage : MonoBehaviour
{
    [SerializeField] private int coins;

    [SerializeField] private UnityEvent OnSpend;
    [SerializeField] private UnityEvent OnSpendFail;
    [SerializeField] private UnityEvent OnAdd;

    private void Start() {
        coins = 0;
    }

    public void AddCoins(int coinAmount) {
        coins += coinAmount;
        OnAdd?.Invoke();
    }

    public void SpendCoins(int coinAmount) {
        if (coins < coinAmount) {
            OnSpendFail?.Invoke();
            return;
        }
        coins -= coinAmount;
        OnSpend?.Invoke();
    }

    public void SetCoinAmount(int coinAmount) {
        coins = coinAmount;
    }
}
