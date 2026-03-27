using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class EnemyBehaviour : MonoBehaviour, InputSystem_Actions.IPlayerActions
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
    public bool LoS = false;
    public LayerMask obstacleMask;
    public LayerMask playerMask;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (target == null) target = GameObject.FindWithTag("Player").transform;
        SelectState();

        
    }

    private InputSystem_Actions controls;

    private void OnEnable()
    {
        if (controls == null)
        {
            controls = new InputSystem_Actions();
            controls.Player.SetCallbacks(this);
        }

        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
    private void Update()
    {
        CheckConditions();
        if (currentState != null) currentState.OnUpdate(this);
        Debug.Log(LoS);

        Vector3 origin = transform.position + Vector3.up * 1.0f;
        Vector3 dir = (target.position - origin).normalized;
        float dist = Mathf.Min(Vector3.Distance(origin, target.position), 20f);

        RaycastHit hit;
        LoS = false;

        if (Physics.Raycast(origin, dir, out hit, dist, obstacleMask | playerMask))
        {

            if (((1 << hit.collider.gameObject.layer) & playerMask) != 0)
                LoS = true;
        }

        
     
    }

    private void CheckConditions()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        isDead = HP <= 0;
        
        isChasing = distance <= visionRange && !isDead && LoS;
        isAttacking = distance <= attackRangeDist && !isDead && LoS;
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
    public void DealDamage()
    {
        Debug.Log("te pego");
    }

    public void OnDamage(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            TakeDamage(1);
            Debug.Log("enemy -1 vida");
        }

    }
}