using UnityEngine;

[CreateAssetMenu(fileName = "ChaseState", menuName = "Scriptable Objects/ChaseState")]
public class ChaseStateSO : NodeSO
{


    public override bool StateCondition(EnemyBehaviour eb)
        => eb.isChasing && !eb.isAttacking && !eb.isDead;

    public override void OnStart(EnemyBehaviour eb)
    {
        eb.agent.speed = 3.5f; 
    }

    public override void OnUpdate(EnemyBehaviour eb)
    {
        if (eb.target == null)
        {
            eb.isChasing = false;
            eb.SelectState();
            return;
        }

    

  

     

        if (!eb.LoS)
        {
            eb.isChasing = false;
            
            eb.agent.ResetPath();
            eb.SelectState();
            return;
        }
        
      
        eb.agent.SetDestination(eb.target.position);

      
        if (eb.isAttacking || !eb.isChasing || eb.isDead)
        {
            eb.SelectState();
        }
    }
}