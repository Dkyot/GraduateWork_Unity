using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counter;
    private float timer, smoothD, refresh, avgFramarate;

    private void Start() {
        counter = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update() {
        float smoothD = Time.smoothDeltaTime;
        timer = timer <= 0 ? refresh : timer -= smoothD;

        if (timer <= 0) avgFramarate = (int)(1f / smoothD);
        counter.text = avgFramarate.ToString();
    }
}
