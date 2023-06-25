using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D m_Rig2D = null;
    [SerializeField]
    private Animator m_Animator = null;
    [SerializeField]
    private float m_HP = 100;

    private void OnEnable()
    {
        m_HP = 100;
    }

    public bool GetHit(float dmg)
    {
        m_HP -= dmg;
        if (m_HP > 0)
        {
            m_Animator.Play("Hurt");
            return true;
        } else
        {
            m_Animator.Play("Death");
            m_Rig2D.simulated = false;
            return false;
        }
    }
}
