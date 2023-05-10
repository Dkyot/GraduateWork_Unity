using UnityEngine;
using UnityEngine.Events;

public class BossfightFinish : MonoBehaviour
{  
    public UnityEvent OnFinish;
    
    public void DestroyBoss() {
        OnFinish?.Invoke();
        Destroy(gameObject);
    }
}
