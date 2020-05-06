using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.Test
{
    public class LayerAinmationTest : MonoBehaviour
    {
        float moveValue = 0;

        Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void SetMove()
        {
            animator.SetFloat("Move", moveValue);
        }

        void OnGUI()
        {
            GUI.Label(new Rect(500, 300+400, 200, 40), " Move = " + moveValue);
            if (GUI.Button(new Rect(500, 350 + 400, 200, 40), "+0.05")) {
                moveValue += 0.05f;
                SetMove();
            }
            if (GUI.Button(new Rect(500, 400 + 400, 200, 40), "-0.05")) {
                moveValue -= 0.05f;

                if (moveValue >= 0)
                    moveValue = 0;

                SetMove();
            }
        }
    }
}
