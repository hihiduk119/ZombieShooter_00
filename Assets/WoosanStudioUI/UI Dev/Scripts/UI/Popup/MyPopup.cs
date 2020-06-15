using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using Ricimi;
using DG.Tweening;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// GUIPack-Clean&Minimalist 에셋  Popup.cs 에서 가져와서 edited.
    /// </summary>
    public class MyPopup : MonoBehaviour
    {
        public Color backgroundColor = new Color(10.0f / 255.0f, 10.0f / 255.0f, 10.0f / 255.0f, 0.6f);

        public float deactiveTime = 0.5f;

        private GameObject m_background;
        Image image;
        Rect rect;
        Sprite sprite;
        Texture2D bgTex;
        public GameObject canvas;
        Color newColor;
        CanvasGroup canvasGroup;

        Animator animator;

        private void Start()
        {
            canvasGroup = transform.GetComponent<CanvasGroup>();
        }

        public void Open()
        {
            AddBackground();

            if(canvasGroup == null) canvasGroup = transform.GetComponent<CanvasGroup>();

            canvasGroup.DOKill();
            canvasGroup.DOFade(1f, 0.5f);
            transform.localScale = Vector3.one;
        }

        public void Close()
        {
            //Debug.Log("Close");
            //기존 애니메이션은 사용시 칼럼 재정렬 문제사 생겨서 트윈으로 변경
            //스캐일 에니메이션은 칼럼 정렬에 문제를 일으킴.
            //var animator = GetComponent<Animator>();
            //if (animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
            //    animator.Play("Close");

            canvasGroup.DOKill();
            //추가된 트윈부분
            canvasGroup.DOFade(0f, 0.15f);

            RemoveBackground();
            StartCoroutine(RunPopupDeactive());
        }

        private IEnumerator RunPopupDeactive()
        {
            yield return new WaitForSeconds(deactiveTime);
            m_background.SetActive(false);
            gameObject.SetActive(false);
        }

        private void AddBackground()
        {
            //PopupBackground Tag를 사용하는 오브젝트 찾기
            //m_background 의 존제 유무 확인
            if (m_background == null)
            {
                //없다면 Tag로 찾기
                m_background = GameObject.FindGameObjectWithTag("PopupBackground");
                //Tag로도 못찾았다면 존제 하지 않기에 새로 생성
                if(m_background == null)
                {
                    m_background = new GameObject("PopupBackground");
                    m_background.tag = "PopupBackground";

                    bgTex = new Texture2D(1, 1);
                    bgTex.SetPixel(0, 0, backgroundColor);
                    bgTex.Apply();

                    image = m_background.AddComponent<Image>();
                    rect = new Rect(0, 0, bgTex.width, bgTex.height);
                    sprite = Sprite.Create(bgTex, rect, new Vector2(0.5f, 0.5f), 1);

                    image.material.mainTexture = bgTex;
                    image.sprite = sprite;
                    newColor = image.color;
                    image.color = newColor;
                    image.canvasRenderer.SetAlpha(0.0f);
                    image.CrossFadeAlpha(1.0f, 0.4f, false);

                    canvas = GameObject.Find("Canvas");
                    m_background.transform.localScale = new Vector3(1, 1, 1);
                    m_background.GetComponent<RectTransform>().sizeDelta = canvas.GetComponent<RectTransform>().sizeDelta;
                    m_background.transform.SetParent(canvas.transform, false);
                    m_background.transform.SetSiblingIndex(transform.GetSiblingIndex());
                }
            }

            m_background.SetActive(true);
        }

        private void RemoveBackground()
        {
            var image = m_background.GetComponent<Image>();
            if (image != null)
                image.CrossFadeAlpha(0.0f, 0.2f, false);
        }
    }
}