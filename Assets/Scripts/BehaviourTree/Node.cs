using System.Collections.Generic;

namespace BrHaviourTree
{
    public class Node
    {
        protected NodeState state;

        public Node parent;
        protected List<Node> children = new List<Node>();

        private Dictionary<string, object> _DataContext = new Dictionary<string, object>();

        public Node()
        {
            parent = null;
        }

        public Node(List<Node> _children)
        {
            var childCount = _children.Count;
            for (int index = 0; index < childCount; ++index)
            {
                Attach(_children[index]);
            }
        }

        /// <summary>
        /// 搜尋該節點的根節點
        /// </summary>
        /// <returns></returns>
        public Node GetRoot()
        {
            Node root = null;
            root = parent;
            while (root.parent != null)
            {
                root = root.parent;
            }
            return root;
        }

        private void Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        /// <summary>
        /// 節點的運行區塊
        /// </summary>
        /// <returns></returns>
        public virtual NodeState Evaluate()
        {
            return NodeState.FAILURE;
        }

        public void SetData(string key, object value)
        {
            _DataContext[key] = value;
        }

        /// <summary>
        /// 獲取資料，會先從當前節點找起
        /// 沒有找到將會逐級往父層搜尋直到出現結果或都沒找到
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetData(string key)
        {
            object value = null;
            if (_DataContext.TryGetValue(key, out value)) {
                return value;
            }

            Node node = parent;
            while(node != null)
            {
                value = node.GetData(key);
                if (value != null)
                {
                    return value;
                }
                node = node.parent;
            }
            return null;
        }

        /// <summary>
        /// 清除資料，會先從當前節點找起
        /// 沒有找到將會逐級往父層搜尋直到出現結果或都沒找到
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ClearData(string key)
        {
            if (_DataContext.ContainsKey(key))
            {
                _DataContext.Remove(key);
                return true;
            }

            Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                {
                    return true;
                }
                node = node.parent;
            }
            return false;
        }
    }

    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }
}
