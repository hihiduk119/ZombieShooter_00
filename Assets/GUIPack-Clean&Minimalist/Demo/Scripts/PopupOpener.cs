// Copyright (C) 2015-2019 ricimi - All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement.
// A Copy of the Asset Store EULA is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;

namespace Ricimi
{
    // This class is responsible for creating and opening a popup of the given prefab and add
    // it to the UI canvas of the current scene.
    public class PopupOpener : MonoBehaviour
    {
        public GameObject popupPrefab;

        protected Canvas m_canvas;

        protected void Start()
        {
            GameObject canvasObject = GameObject.Find("Canvas");

            if (canvasObject != null)
                m_canvas = canvasObject.GetComponent<Canvas>();

            canvasObject = GameObject.Find("Robby Canvas");

            if (canvasObject != null)
                m_canvas = canvasObject.GetComponent<Canvas>();
        }

        /// <summary>
        /// 팝업 생성
        /// </summary>
        public virtual void OpenPopup()
        {
            var popup = Instantiate(popupPrefab) as GameObject;
            popup.SetActive(true);
            popup.transform.localScale = Vector3.zero;
            popup.transform.SetParent(m_canvas.transform, false);
            popup.GetComponent<Popup>().Open();
        }

        /// <summary>
        /// 팝업 생성 및 리턴
        /// </summary>
        /// <returns></returns>
        public GameObject OpenPopupAndReturn()
        {
            var popup = Instantiate(popupPrefab) as GameObject;
            popup.SetActive(true);
            popup.transform.localScale = Vector3.zero;
            popup.transform.SetParent(m_canvas.transform, false);
            popup.GetComponent<Popup>().Open();

            return popup;
        }
    }
}
