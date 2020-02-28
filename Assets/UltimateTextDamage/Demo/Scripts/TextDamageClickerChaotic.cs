using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WoosanStudio.ZombieShooter;

namespace Guirao.UltimateTextDamage
{
    /// <summary>
    /// 이거 테스트용이다. 참고만 하고 다시만들어야 한다.
    /// </summary>
    public class TextDamageClickerChaotic : MonoBehaviour
    {
        public UltimateTextDamageManager textManager;
        public Transform overrideTransform;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.1f);

            UltimateTextDamageManager manager = AllTextDamageManager.Instance.GetTextDamageManager();
            if(manager != null) { textManager = manager; }
        }

        private void OnMouseUpAsButton( )
        {
            if (textManager == null) return;

            if( Random.value < 0.3f )
                textManager.Add( ( Random.Range( 50f , 200f ) ).ToStringScientific( ) , overrideTransform != null ? overrideTransform : transform , "critical" );
            else if(Random.value >= 0.3f && Random.value < 0.6f )
            {
                textManager.Add("status", overrideTransform != null ? overrideTransform : transform, "status");
            }
            else
            {
                textManager.Add(Random.Range(1f, 50f).ToStringScientific(), overrideTransform != null ? overrideTransform : transform);
            }
                
        }

        public bool autoclicker = true;
        public float clickRate = 1;

        float lastTimeClick;
        private void Update( )
        {
            if( !autoclicker )
                return;

            if( Time.time - lastTimeClick >= 1f / clickRate )
            {
                lastTimeClick = Time.time;
                OnMouseUpAsButton( );
            }
        }
    }
}
