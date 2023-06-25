using System.Collections.Generic;

namespace BrHaviourTree
{
    /// <summary>
    /// 特性: 照順序執行
    /// </summary>
    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> _children) : base(_children) { }

        public override NodeState Evaluate()
        {
            var anyChildIsRunning = false;
            var childCount = children.Count;
            /*
             *  依序執行子節點，中途出現NodeState.FAILURE才會終止執行
             */
            for (int index = 0; index < childCount; ++index)
            {
                var node = children[index];
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }

            state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }
    }
}
