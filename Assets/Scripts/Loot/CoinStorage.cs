using UnityEngine;
using UnityEngine.Events;

public class CoinStorage : MonoBehaviour
{
    [SerializeField] private int coins;
    [SerializeField] private PlayerSO currentData;

    [SerializeField] private UnityEvent OnSpend;
    [SerializeField] private UnityEvent OnSpendFail;
    [SerializeField] private UnityEvent OnAdd;

    private void Start() {
        coins = currentData.coins;
    }

    public void AddCoins(int coinAmount) {
        coins += coinAmount;
        OnAdd?.Invoke();
    }

    public bool SpendCoins(int coinAmount) {
        if (coins < coinAmount) {
            OnSpendFail?.Invoke();
            return false;
        }
        coins -= coinAmount;
        OnSpend?.Invoke();
        return true;
    }

    public int GetCoinAmount() {
        return coins;
    }

    public void SetCoinAmount(int coinAmount) {
        coins = coinAmount;
    }
}
