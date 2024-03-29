using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;
    public UnityEvent OnAttack;
    
    private Transform player;

    [SerializeField]
    private float chaseDistanceThreshold = 3, attackDistanceThreshold = 0.8f;

    [SerializeField]
    private float attackDelay = 1;
    private float passedTime = 1;

    private void Start() {
        player = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        if (player == null)  return;

        float distance = Vector2.Distance(player.position, transform.position);
        if (distance < chaseDistanceThreshold) {
            OnPointerInput?.Invoke(player.position - transform.position);
            if (distance <= attackDistanceThreshold) {
                OnMovementInput?.Invoke(Vector2.zero);
                if (passedTime >= attackDelay) {
                    passedTime = 0;
                    OnAttack?.Invoke();
                }
            }
            else {
                Vector2 direction = player.position - transform.position;
                OnMovementInput?.Invoke(direction.normalized);
            }
        }
        if(distance > chaseDistanceThreshold)
        {
            OnMovementInput?.Invoke(Vector2.zero);
        }

        if (passedTime < attackDelay) 
            passedTime += Time.deltaTime;
    }
}
