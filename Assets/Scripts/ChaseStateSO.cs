using UnityEngine;

[CreateAssetMenu(fileName = "ChaseState", menuName = "Scriptable Objects/ChaseState")]
public class ChaseStateSO : NodeSO
{
    public override bool StateCondition(EnemyBehaviour eb) => eb.isChasing && !eb.isAttacking && !eb.isDead;

    public override void OnStart(EnemyBehaviour eb)
    {
        eb.agent.speed = 3.5f; // Aumentar velocidad al perseguir
    }

    public override void OnUpdate(EnemyBehaviour eb)
    {
        eb.agent.SetDestination(eb.target.position);

        if (eb.isAttacking || !eb.isChasing || eb.isDead) eb.SelectState();
    }
}