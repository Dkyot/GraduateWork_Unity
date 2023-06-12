using UnityEngine;

public class GuidePlatformIdentificator : MonoBehaviour
{
    [SerializeField] private GameObject mobileGuide;
    [SerializeField] private GameObject pcGuide;
    
    private void Start() {
        if (Application.isMobilePlatform) {
            mobileGuide.SetActive(true);
            pcGuide.SetActive(false);
        }
        else {
            mobileGuide.SetActive(false);
            pcGuide.SetActive(true);
        }
    }
}
