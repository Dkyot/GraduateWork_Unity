using UnityEngine;
using UnityEngine.Events;

public class LoadData : MonoBehaviour
{
    [SerializeField] private UnityEvent OnLaunch;
    
    private void Awake() {
        SavePlayerDataBetweenScenes manager = GetComponent<SavePlayerDataBetweenScenes>();
        manager.LoadData();
    }

    private void Start() {
        OnLaunch?.Invoke();
    }
}
