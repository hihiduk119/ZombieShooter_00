using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WoosanStudio.ZombieShooter;

namespace Guirao.UltimateTextDamage
{
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
                textManager.Add( ( Random.Range( 100800f , 2008000f ) ).ToStringScientific( ) , overrideTransform != null ? overrideTransform : transform , "critical" );
            else
                textManager.Add( Random.Range( 900000f , 1100000f ).ToStringScientific( ) , overrideTransform != null ? overrideTransform : transform );
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
