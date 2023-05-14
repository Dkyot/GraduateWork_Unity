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

    [SerializeField] private float runawayDistance = 5f;

    [SerializeField] private float speed = 2f;

    private float chTimer = 0;
    private float aTimer = 0;
    
    [SerializeField] private float chasingInterval = 0.2f;
    [SerializeField] private float attackDelay = 0.5f;

    public UnityEvent<Vector2> OnPointerInput;
    public UnityEvent OnAttack;

    [SerializeField] private bool runningAway;

    private float distance = 99;
    
    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        target = FindObjectOfType<Player>().transform;

        agent.speed = speed + Random.Range(0.5f, 1.5f);

        if (runningAway) {
            agent.stoppingDistance = runawayDistance - 0.5f;
            attackingDistance = runawayDistance + 3;
        }
        else {
            agent.stoppingDistance = stoppingDistance;
        }
    }

    private void Update() {
        chTimer += Time.deltaTime;

        distance = Vector2.Distance(target.position, transform.position);

        Attack();
        TakeDistance();
    }

    private void OnDrawGizmos() {
        Vector3 direction = (target.position - transform.position).normalized;
        Vector3 pos = transform.position - (direction * runawayDistance);
        Gizmos.DrawLine(transform.position, pos);
    }

    private void RunAway() {
        Vector3 direction = (target.position - transform.position).normalized;
        Vector3 pos = transform.position - (direction * runawayDistance);
        agent.SetDestination(pos);
    }

    private void Attack() {
        if (distance <= chasingDistance) {
            aTimer += Time.deltaTime;
            OnPointerInput?.Invoke(target.position - transform.position);

            if (distance <= attackingDistance) {
                if (aTimer >= attackDelay) {
                    OnAttack?.Invoke();
                    aTimer = 0;
                }
            }
        }
    }

    private void TakeDistance() {
        if (chTimer >= chasingInterval) {
            chTimer = 0;

            if (distance <= chasingDistance) {
                if (runningAway) {
                    if (distance < runawayDistance) RunAway();
                    else agent.SetDestination(target.position);
                }
                else {
                    agent.SetDestination(target.position); 
                }
            }
        }
    }
}
