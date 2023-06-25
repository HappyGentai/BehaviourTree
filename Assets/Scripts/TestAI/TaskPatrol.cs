using UnityEngine;
using BrHaviourTree;

/// <summary>
/// �۩w�q�`�I-����
/// </summary>
public class TaskPatrol : Node
{
    #region �۩w�q�ݭn�ϥΤ��Ѽ�
    private Transform _transform;
    private Transform[] _wayPoints;

    private int _currentWayPointIndex = 0;

    private float _waitTime = 1f; // in seconds
    private float _waitCounter = 0;
    private bool _waiting = false;
    private Animator _animator = null;
    #endregion

    //  �۩w�q�غc��
    public TaskPatrol(Animator animator, Transform transform, Transform[] wayPoints)
    {
        _animator = animator;
        _transform = transform;
        _wayPoints = wayPoints;
    }

    public override NodeState Evaluate()
    {
        if (_waiting)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter >= _waitTime)
            {
                _waiting = false;
            } 
        } else
        {
            _animator.Play("Run");
            Transform wp = _wayPoints[_currentWayPointIndex];
            var selfPos = _transform.position;
            var wpPos = wp.position;
            var face = Vector3.one;
            if (selfPos.x <= wpPos.x)
            {
                _transform.localScale = face;
            } else if (selfPos.x > wpPos.x)
            {
                face.x *= -1;
                _transform.localScale = face;
            }
            if (Vector3.Distance(selfPos, wpPos) < 0.01f)
            {
                _transform.position = wpPos;
                _waitCounter = 0;
                _waiting = true;
                _currentWayPointIndex = (_currentWayPointIndex + 1) % _wayPoints.Length;
                _animator.Play("Idle");
                if (_currentWayPointIndex == 0)
                {
                    state = NodeState.SUCCESS;
                    return state;
                }
            }
            else
            {
                float speed = 0;
                //  �q������t�׸��
                float.TryParse(GetData("Speed").ToString(), out speed);
                _transform.position = Vector3.MoveTowards(selfPos, wpPos, speed * Time.deltaTime);
            }
        }
        state = NodeState.RUNNING;
        return state;
    }
}
