using UnityEngine;

namespace BrHaviourTree
{
    public abstract class Tree : MonoBehaviour
    {
        private Node _root = null;

        protected void Start()
        {
            //  設置行為樹
            _root = SetupTree();
        }

        private void Update()
        {
            /*
             *  呼叫行為樹之Update
             *  原理上會遍歷並執行各個節點，何時終止遍歷行為視所填入的子節點而定
             *  若為Selector節點:
             *  將會遍歷至任一節點Evaluate方法回傳狀態為SUCCESS或RUNNING為止，若都無符合條件之對象最後會收到狀態FAILURE
             *  若為Sequence節點:
             *  將會完整遍歷並執行所有節點除非中途收到狀態FAILURE才會停止
             */
            _root?.Evaluate();
        }

        protected abstract Node SetupTree();
    }
}
