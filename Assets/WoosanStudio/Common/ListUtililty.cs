using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.Common
{
    public class ListUtililty : MonoBehaviour
    {
        /// <summary>
        /// Base리스트에서 삭제할 리스트를 제거 해서 반환
        /// </summary>
        /// <typeparam name="T">any 타입</typeparam>
        /// <param name="baseList">베이스 리스트</param>
        /// <param name="removeList">삭제할 아이템 리스트</param>
        /// <returns></returns>
        static public List<T> RemoveList<T>(List<T> baseList, List<T> removeList)
        {
            //Debug.Log("현재 base count = " + baseList.Count + "     remove Count = " + removeList.Count);

            for (int i = 0; i < removeList.Count; i++)
            {
                for (int j = 0; j < baseList.Count; j++)
                {
                    if (removeList[i].Equals(baseList[j]))
                    {
                        baseList.RemoveAt(j);
                    }
                }
            }

            //Debug.Log("남음 base count = " + baseList.Count);

            return baseList;
        }
    }
}
