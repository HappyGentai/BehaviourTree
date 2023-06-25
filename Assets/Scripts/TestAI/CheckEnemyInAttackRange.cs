using UnityEngine;
using BrHaviourTree;

/// <summary>
/// 自定義節點-偵測是否進入至攻擊範圍
/// </summary>
public class CheckEnemyInAttackRange : Node
{
    private Transform self = null;
    private float attackRange = 0;
    private Animator animator = null;

    public CheckEnemyInAttackRange(Transform _self, float _attackRange, Animator _animator)
    {
        self = _self;
        attackRange = _attackRange;
        animator = _animator;
    }

    public override NodeState Evaluate()
    {
        var target = (Transform)GetData("Target");
        if (target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        var targetPos = target.position;
        if (Vector3.Distance(self.position, targetPos) <= attackRange)
        {
            animator.Play("Attack");
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
