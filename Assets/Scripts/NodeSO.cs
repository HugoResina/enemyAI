using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NodeState", menuName = "Scriptable Objects/NodeState")]

public class NodeSO : ScriptableObject
{
    public List<NodeSO> children;
    public virtual bool OnEndCondition(EnemyBehaviour eb)
    {
        return false;
    }
    public virtual bool StateCondition(EnemyBehaviour eb)
    {
        return true;
    }
    public virtual void OnStart(EnemyBehaviour eb)
    {

    }
    public virtual void OnFinish(EnemyBehaviour eb)
    {

    }
    public virtual void OnUpdate(EnemyBehaviour eb)
    {

    }
}
