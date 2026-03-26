using UnityEngine;

[CreateAssetMenu(fileName = "PatrolState", menuName = "Scriptable Objects/PatrolState")]
public class PatrolStateSO : NodeSO
{
    private int currentPoint = 0;

    public override bool StateCondition(EnemyBehaviour eb) => !eb.isChasing && !eb.isDead;

    public override void OnUpdate(EnemyBehaviour eb)
    {
        if (eb.patrolPoints.Count == 0) return;

        eb.agent.SetDestination(eb.patrolPoints[currentPoint].position);

        if (!eb.agent.pathPending && eb.agent.remainingDistance < 0.5f)
        {
            currentPoint = (currentPoint + 1) % eb.patrolPoints.Count;
        }

        if (eb.isChasing || eb.isDead) eb.SelectState();
    }
}