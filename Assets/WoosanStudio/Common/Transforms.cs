using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.Common
{
    public class Transforms : MonoBehaviour, ITransforms
    {
        [SerializeField]
        private List<Transform> items = new List<Transform>();
        public List<Transform> Items { get => items; }

        //[Header("[자식에서 비교해서 찾을 이름]")]
        //public string CompareName = "";

        /// <summary>
        /// 자식에서 이름으로 찾아서 리스트에 아이템에 넣기.
        /// </summary>
        /// <param name="items">찾아서 넣어줄 리스트</param>
        /// <param name="parent">찾을 트랜스폼 부모</param>
        /// <param name="CompareName">찾을 이름</param>
        static public void FindAllTransformToTheChild(ref List<Transform> items, Transform parent, string CompareName)
        {
            Transform[] childs = parent.GetComponentsInChildren<Transform>();
            for (int index = 0; index < childs.Length; index++)
            {
                if (childs[index].name.Equals(CompareName))
                {
                    items.Add(childs[index]);
                }
            }
        }

        /// <summary>
        /// parent 에서 자식을 찾아서 가져옴
        /// </summary>
        /// <param name="items"></param>
        /// <param name="parent"></param>
        static public void FindAll(ref List<Transform> items, Transform parent)
        {
            items = new List<Transform>(parent.GetComponentsInChildren<Transform>());
            //그냥 가저요면 부모가 0번이기 때문에 자식만 가져오게 하기위해 0번 삭제.
            items.RemoveAt(0);
        }

        /// <summary>
        /// 현재 트렌스폼의 자식을 모두 가져옴
        /// </summary> 
        /// <returns></returns>
        public List<Transform> GetChilds(string CompareName)
        {
            Transform[] childs = this.GetComponentsInChildren<Transform>();
            for (int index = 0; index < childs.Length; index++)
            {
                if (childs[index].name.Equals(CompareName))
                {
                    items.Add(childs[index]);
                }
            }

            return items;
        }
    }
}
