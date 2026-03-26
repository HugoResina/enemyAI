using UnityEngine;

[CreateAssetMenu(fileName = "DeadState", menuName = "Scriptable Objects/DeadState")]
public class DeadStateSO : NodeSO
{
    public override bool StateCondition(EnemyBehaviour eb) => eb.isDead;

    public override void OnStart(EnemyBehaviour eb)
    {
        eb.agent.isStopped = true;
        eb.GetComponent<Animator>().SetBool("Dead", true);
        eb.enabled = false; // Desactiva la IA
    }
}