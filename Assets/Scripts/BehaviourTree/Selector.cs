using System.Collections.Generic;

namespace BrHaviourTree
{
    /// <summary>
    /// 特性: 依照條件執行
    /// </summary>
    public class Selector : Node
    {
        public Selector() : base() { }
        public Selector(List<Node> _children) : base(_children) { }

        public override NodeState Evaluate()
        {
            var childCount = children.Count;
            /*
             *  優先符合條件也就是第一個執行成功或執行中的對象將會被回傳並中斷迴圈
             */
            for (int index = 0; index < childCount; ++index)
            {
                var node = children[index];
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}
