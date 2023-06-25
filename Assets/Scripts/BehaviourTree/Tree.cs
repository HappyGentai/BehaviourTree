using UnityEngine;

namespace BrHaviourTree
{
    public abstract class Tree : MonoBehaviour
    {
        private Node _root = null;

        protected void Start()
        {
            //  �]�m�欰��
            _root = SetupTree();
        }

        private void Update()
        {
            /*
             *  �I�s�欰��Update
             *  ��z�W�|�M���ð���U�Ӹ`�I�A��ɲפ�M���欰���Ҷ�J���l�`�I�өw
             *  �Y��Selector�`�I:
             *  �N�|�M���ܥ��@�`�IEvaluate��k�^�Ǫ��A��SUCCESS��RUNNING����A�Y���L�ŦX���󤧹�H�̫�|���쪬�AFAILURE
             *  �Y��Sequence�`�I:
             *  �N�|����M���ð���Ҧ��`�I���D���~���쪬�AFAILURE�~�|����
             */
            _root?.Evaluate();
        }

        protected abstract Node SetupTree();
    }
}
