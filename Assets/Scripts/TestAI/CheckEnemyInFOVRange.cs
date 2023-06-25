using BrHaviourTree;
using UnityEngine;

/// <summary>
/// 自定義節點-偵測範圍內有無敵人
/// </summary>
public class CheckEnemyInFOVRange : Node
{
    private Transform self = null;
    private LayerMask enemyMask = 0;
    private float searchRadius = 0;
    private Animator animator = null;

    public CheckEnemyInFOVRange(Animator _animator, Transform _self, LayerMask _enemyMask, float _searchRadius)
    {
        animator = _animator;
        self = _self;
        enemyMask = _enemyMask;
        searchRadius = _searchRadius;
    }

    public override NodeState Evaluate()
    {
        object target = GetData("Target");
        if (target == null)
        {
            Collider2D collider = Physics2D.OverlapCircle(self.position, searchRadius, enemyMask);
            if (collider != null)
            {
                //  向根節點設置資料，這樣根節點底下的其他子節點也可以參考到相關資料
                GetRoot().SetData("Target", collider.transform);
                animator.Play("Run");
                state = NodeState.SUCCESS;
                return state;
            } else
            {
                state = NodeState.FAILURE;
                return state;
            }
        }
        state = NodeState.SUCCESS;
        return state;
    }
}
