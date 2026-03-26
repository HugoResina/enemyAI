using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemyBehaviour : MonoBehaviour
{
    public NodeSO root, currentState;
    [HideInInspector] public NavMeshAgent agent;
    public Transform target;

    [Header("Configuración")]
    public float visionRange = 10f;
    public float attackRangeDist = 2f;
    public List<Transform> patrolPoints;
    public int HP = 10;

    [Header("Estados de Condición")]
    public bool isDead;
    public bool isChasing;
    public bool isAttacking;


    
    public CapsuleCollider attackCollider; 
    public int damage = 1;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (target == null) target = GameObject.FindWithTag("Player").transform;
        SelectState();
    }

    private void Update()
    {
        CheckConditions();
        if (currentState != null) currentState.OnUpdate(this);
    }

    private void CheckConditions()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        isDead = HP <= 0;
        
        isChasing = distance <= visionRange && !isDead;
        isAttacking = distance <= attackRangeDist && !isDead;
    }

    public void SelectState()
    {
        // Prioridad: Muerte > Ataque > Persecución > Patrulla (Idle/Default)
        foreach (var child in root.children)
        {
            if (child.StateCondition(this))
            {
                if (currentState != null) currentState.OnFinish(this);
                currentState = child;
                currentState.OnStart(this);
                break;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!isChasing || isDead) return;  

        if (collision.transform == target)
        {
           
            Debug.Log("Golpea al jugador");
        }
    }
   
    public void TakeDamage(int amount)
    {
        HP -= amount;
        if (HP <= 0) SelectState();
    }
}