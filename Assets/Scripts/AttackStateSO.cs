using UnityEngine;

[CreateAssetMenu(fileName = "AttackState", menuName = "Scriptable Objects/AttackState")]
public class AttackStateSO : NodeSO
{
    public float attackCooldown = 0.75f;  
    private float _attackTimer;

    
    public override bool StateCondition(EnemyBehaviour eb)
        => eb.isAttacking && !eb.isDead;

    public override void OnStart(EnemyBehaviour eb)
    {
        eb.agent.isStopped = true;
        _attackTimer = 0f;
        Debug.Log("Enter Attack");

       
    }

    public override void OnUpdate(EnemyBehaviour eb)
    {
       
        if (!eb.isAttacking || eb.isDead)
        {
            eb.agent.isStopped = false;
            eb.SelectState();
            return;
        }

       
        if (eb.target != null)
        {
            Vector3 dir = (eb.target.position - eb.transform.position).normalized;
            dir.y = 0;
            if (dir != Vector3.zero)
            {
                Quaternion lookRot = Quaternion.LookRotation(dir);
                eb.transform.rotation = Quaternion.Slerp(eb.transform.rotation, lookRot, 0.1f);
            }
        }

       
        _attackTimer -= Time.deltaTime;
        if (_attackTimer <= 0f)
        {
            
            Debug.Log("Attack?");
                      

            _attackTimer = attackCooldown;
        }
    }
}