using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class NavMeshEnemyAI : MonoBehaviour
{
    [SerializeField] private Transform target;
    private NavMeshAgent agent;

    [SerializeField] private float stoppingDistance = 2f;
    [SerializeField] private float attackingDistance = 2.5f;
    [SerializeField] private float chasingDistance = 25f;

    private float chTimer = 0;
    private float aTimer = 0;
    [SerializeField] private float chasingInterval = 0.5f;
    [SerializeField] private float attackDelay = 0.5f;

    public UnityEvent<Vector2> OnPointerInput;
    public UnityEvent OnAttack;

    private float distance = 99;
    
    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        target = FindObjectOfType<Player>().transform;

        agent.speed = Random.Range(3f, 6f);
        agent.stoppingDistance = stoppingDistance;
    }

    private void Update() {
        chTimer += Time.deltaTime;
        if (chTimer >= chasingInterval) {
            distance = Vector2.Distance(target.position, transform.position);
            chTimer = 0;

            if (distance > chasingDistance) {
                //idle
            }
            else {
                agent.SetDestination(target.position); 
            }
        }
        
        if (distance <= chasingDistance) {
            aTimer += Time.deltaTime;
            OnPointerInput?.Invoke(target.position - transform.position);
            if (distance <= attackingDistance) {
                if (aTimer >= attackDelay) {
                    OnAttack?.Invoke();
                    //Debug.Log("attack");
                    aTimer = 0;
                }
            }
        }
    }
}
