using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ricimi;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// GUIPack-Clean&Minimalist 에셋  PopupOpener.cs 에서 가져와서 edited.
    /// </summary>
    public class MyPopupOpener : MonoBehaviour
    {
        public GameObject popupPrefab;

        public GameObject Instance;

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

        public virtual void OpenPopup()
        {
            if (Instance == null)
            {
                Instance = Instantiate(popupPrefab) as GameObject;
                Instance.transform.localScale = Vector3.zero;
                Instance.transform.SetParent(m_canvas.transform, false);
                //Instance.GetComponent<MyPopup>().Open();    
            }
            Instance.SetActive(true);
            //Instance.GetComponent<Popup>().Open();
            Instance.GetComponent<MyPopup>().Open();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                Instance.SetActive(true);
            }
        }
    }
}
