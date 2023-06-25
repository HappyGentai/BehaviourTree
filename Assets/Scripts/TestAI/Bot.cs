using UnityEngine;
using BrHaviourTree;
using System.Collections.Generic;

/// <summary>
/// �۩w�qAI�A�~�Ӧ�BrHaviourTree.Tree
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
         *  ���󶰦X�`�I�A�̾��u���F�����󪺸`�I�i����򨫳X�ΰ���  
         */
        Node root = new Selector(new List<Node> {
            /*
             * ���Ƕ��X�`�I�A���T�{�ؼЬO�_��F�����d��A�P�_�n���n����
             */
            new Sequence(new List<Node>{
                new CheckEnemyInAttackRange(this.transform, m_AttackRange, m_Animator),
                new TaskAttack(m_Animator, m_AttackTime, m_Damage)
            }),
            /*
             * ���Ƕ��X�`�I,���T�{�d�򤺦��L�ؼСA�Y���N�|����¥ؼв���
             */
            new Sequence(new List<Node>{
                new CheckEnemyInFOVRange(m_Animator, this.transform, m_EnemyMask, m_SearchRadius),
                new TaskGoToTarget(this.transform)
            }),
            /*
             * �@��`�I�A���ۨ����I�i�沾�ʩM�ݾ�
             */
            new TaskPatrol(m_Animator, this.transform, m_WayPoints)
        });
            
        //  Set speed << �]�m���`�I�ϥΪ����
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
