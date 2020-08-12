using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class ReloadEndEventListener : MonoBehaviour
    {
        public UnityEvent ReloadEndEvent = new UnityEvent();

        public void ReloadEnd()
        {
            ReloadEndEvent?.Invoke();
            Debug.Log("=>>>>>>>>> 리로드가 호출 됐습니다.");
        }
    }
}
