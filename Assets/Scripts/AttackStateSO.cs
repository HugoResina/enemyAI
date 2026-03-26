using UnityEngine;

[CreateAssetMenu(fileName = "AttackState", menuName = "Scriptable Objects/AttackState")]
public class AttackStateSO : NodeSO
{
    public override bool StateCondition(EnemyBehaviour eb) => eb.isAttacking && !eb.isDead;

    public override void OnStart(EnemyBehaviour eb)
    {
        eb.agent.isStopped = true; // Se detiene para atacar
        //eb.GetComponent<Animator>().SetTrigger("Attack");
    }

    public override void OnUpdate(EnemyBehaviour eb)
    {
        // Mirar al jugador mientras ataca
        Vector3 direction = (eb.target.position - eb.transform.position).normalized;
        eb.transform.rotation = Quaternion.Slerp(eb.transform.rotation, Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)), 0.1f);

        if (!eb.isAttacking || eb.isDead)
        {
            eb.agent.isStopped = false;
            eb.SelectState();
        }
    }
}