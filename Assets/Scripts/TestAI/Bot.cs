using UnityEngine;
using BrHaviourTree;
using System.Collections.Generic;

/// <summary>
/// 自定義AI，繼承至BrHaviourTree.Tree
/// </summary>
public class Bot : BrHaviourTree.Tree
{
    [SerializeField]
    private Animator m_Animator = null;
    [SerializeField]
    private Transform[] m_WayPoints = null;
    [SerializeField]
    private float Speed = 2f;
    [SerializeField]
    private LayerMask m_EnemyMask = 0;
    [SerializeField]
    private float m_SearchRadius = 2f;
    [SerializeField]
    private float m_AttackRange = 1;
    [SerializeField]
    private float m_AttackTime = 0.3f;
    [SerializeField]
    private float m_Damage = 20;

    protected override Node SetupTree()
    {
        /*
         *  條件集合節點，依據優先達成條件的節點進行後續走訪及執行  
         */
        Node root = new Selector(new List<Node> {
            /*
             * 順序集合節點，先確認目標是否到達攻擊範圍再判斷要不要攻擊
             */
            new Sequence(new List<Node>{
                new CheckEnemyInAttackRange(this.transform, m_AttackRange, m_Animator),
                new TaskAttack(m_Animator, m_AttackTime, m_Damage)
            }),
            /*
             * 順序集合節點,先確認範圍內有無目標，若有將會持續朝目標移動
             */
            new Sequence(new List<Node>{
                new CheckEnemyInFOVRange(m_Animator, this.transform, m_EnemyMask, m_SearchRadius),
                new TaskGoToTarget(this.transform)
            }),
            /*
             * 一般節點，像著巡邏點進行移動和待機
             */
            new TaskPatrol(m_Animator, this.transform, m_WayPoints)
        });
            
        //  Set speed << 設置給節點使用的資料
        root.SetData("Speed", Speed);
        return root;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.25f);
        Gizmos.DrawSphere(this.transform.position, m_SearchRadius);
        Gizmos.color = new Color(0, 1, 0, 0.25f);
        Gizmos.DrawSphere(this.transform.position, m_AttackRange);
    }
}
