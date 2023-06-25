using UnityEngine;
using BrHaviourTree;

/// <summary>
/// 自定義節點-攻擊
/// </summary>
public class TaskAttack : Node
{
    private Animator animator = null;
    private float attackTime = 0;
    private float attackCounter = 0;
    private float damage = 0;

    public TaskAttack(Animator _animator, float _attackTime, float _damage)
    {
        animator = _animator;
        attackTime = _attackTime;
        damage = _damage;
    }

    public override NodeState Evaluate()
    {
        var target = (Transform)GetData("Target");

        attackCounter += Time.deltaTime;
        if (attackCounter >= attackTime)
        {
            attackCounter = 0;
            var enemy = target.GetComponent<Enemy>();
            if (enemy == null)
            {
                parent.SetData("Target", null);
                state = NodeState.FAILURE;
                return state;
            }

            // Set damage and check enemy die or not
            if (!enemy.GetHit(damage))
            {
                GetRoot().SetData("Target", null);
                animator.Play("Run");
                state = NodeState.SUCCESS;
                return state;
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
