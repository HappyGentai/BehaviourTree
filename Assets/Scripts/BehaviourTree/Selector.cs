using System.Collections.Generic;

namespace BrHaviourTree
{
    /// <summary>
    /// �S��: �̷ӱ������
    /// </summary>
    public class Selector : Node
    {
        public Selector() : base() { }
        public Selector(List<Node> _children) : base(_children) { }

        public override NodeState Evaluate()
        {
            var childCount = children.Count;
            /*
             *  �u���ŦX����]�N�O�Ĥ@�Ӱ��榨�\�ΰ��椤����H�N�|�Q�^�Ǩä��_�j��
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
