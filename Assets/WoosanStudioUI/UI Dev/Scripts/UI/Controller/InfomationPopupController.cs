using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;

namespace WoosanStudio.ZombieShooter
{
    public class InfomationPopupController : MonoBehaviour
    {
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Open()
        {
            canvasGroup.DOFade(1, 0.15f);
        }

        public void Close()
        {
            canvasGroup.DOFade(0, 0.15f);
        }

        #region [-TestCode]
        //void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.A))
        //    {
        //        Open();
        //    }

        //    if (Input.GetKeyDown(KeyCode.S))
        //    {
        //        Close();
        //    }
        //}
        #endregion

    }
}
