using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] PlayerSO data;

    public GameObject magnet;

    public void UpdateData() {
        magnet?.SetActive(data.useMagneticField);
    }
}
