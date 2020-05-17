using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public class VectorNormalizeTest : MonoBehaviour
    {
        void PrintNormalize()
        {
            Vector3 rot = transform.localRotation.eulerAngles;
            Vector3 rot2 = transform.localRotation.normalized.eulerAngles;

            rot.Scale(new Vector3(1, 1, 0));
            
            Debug.Log("rot = [" + rot.x + "]:[" + rot.y + "]:[" + rot.z+"]");
            Debug.Log("rot = [" + rot2.x + "]:[" + rot2.y + "]:[" + rot2.z + "]");
            Debug.Log("========================================");
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.T))
            {
                PrintNormalize();
            }
        }
    }
}
