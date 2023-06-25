using UnityEngine;
using BrHaviourTree;

/// <summary>
/// 自定義節點-像目標移動
/// </summary>
public class TaskGoToTarget : Node
{
    private Transform self = null;

    public TaskGoToTarget(Transform _self)
    {
        self = _self;
    }

    public override NodeState Evaluate()
    {
        var target = (Transform)GetData("Target");

        if (Vector3.Distance(self.position, target.position) > 0.01f)
        {
            float speed = 0;
            float.TryParse(GetData("Speed").ToString(), out speed);
            var targetPos = target.position;
            self.position = Vector3.MoveTowards(self.position, targetPos, speed * Time.deltaTime);
            var selfPos = self.position;
            
            var face = Vector3.one;
            if (selfPos.x <= targetPos.x)
            {
                self.localScale = face;
            }
            else if (selfPos.x > targetPos.x)
            {
                face.x *= -1;
                self.localScale = face;
            }
        }
        state = NodeState.RUNNING;
        return state;
    }
}
