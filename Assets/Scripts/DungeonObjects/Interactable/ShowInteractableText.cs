using UnityEngine;

public class ShowInteractableText : MonoBehaviour
{
    [SerializeField] private GameObject text;
    private bool isSold;
    
    public void HideInfo() {
        isSold = true;
        text.SetActive(false);
    }
    
    #region OnTrigger methods
    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            if (!isSold)
                text.SetActive(true);
        }
    }

    private void OnTriggerExit2D (Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            text.SetActive(false);
        }
    }
    #endregion
}
