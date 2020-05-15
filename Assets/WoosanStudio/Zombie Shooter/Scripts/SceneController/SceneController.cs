using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 씬로드를 실행
    /// 
    ///  - 씬로드 시퀀스
    /// [0. 최초 호출부분]
    /// [1. 현재 씬을 페이드 out 시킴]
    /// [2. 페이드 완료 후 로딩 화면 씬 실행 ]
    /// [3. 로딩씬 호출 완료되면 뚜렷해짐 실행]
    /// [4. 뚜렷해짐 완료후 1초 대기 후 다음씬 호출]
    /// [5. 다음씬 호출 완료시 뚜렸해짐 실행]
    /// [6. 뚜렷해짐 완료후 씬 로드 완료]
    /// </summary>
    public class SceneController : MonoBehaviour
    {
        public static SceneController instance;
        
        [Header("[로딩 전용씬 이름]")]
        [SerializeField]
        private string _loadingScene = "0.LoadingScene";
        public string loadingScene { get => _loadingScene; set => _loadingScene = value; }

        [Header("[로딩 상태 완료]")]
        [SerializeField]
        private bool _isDone = true;
        public bool isDone { get => _isDone; set => _isDone = value; }

        [Header("[로딩 완료 이벤트]")]
        [SerializeField]
        private UnityEvent _loadCompleteEvent = new UnityEvent();
        public UnityEvent loadCompleteEvent { get => _loadCompleteEvent; set => _loadCompleteEvent = value; }

        /// <summary>
        /// 콜백시 어떤 씬 로드였는지 확인용 플래그
        /// </summary>
        private enum State
        {
            CallLoadingScene = 0,
            CallNextScene,
        }

        /// <summary>
        /// 페이드 In Out 구분 플래그
        /// </summary>
        private enum Fade
        {
            In,
            Out,
        }

        //로딩 싱크를 마추는 코루틴
        private Coroutine loadAsyncScene;
        //페이드 시킬 이미지가 있는 캔버스 그룹
        private CanvasGroup canvasGroup;
        //로컬 씬 네임
        private string nextScene;
        //씬 완료 이벤트
        private LoadingStepCompleteEvent loadingStepCompleteEvent = new LoadingStepCompleteEvent();

        private class LoadingStepCompleteEvent : UnityEvent<int> {}

        private void Awake()
        {
            //싱글톤 패턴
            instance = this;
            //삭제 불가 등록
            DontDestroyOnLoad(this);
            //페이드용 캔버스
            canvasGroup = transform.GetComponentInChildren<CanvasGroup>();
            //씬 호출 완료시 콜백 등록
            loadingStepCompleteEvent.AddListener(LoadCompleteEventHander);
        }

        /// <summary>
        /// 코루틴 람다식
        /// </summary>
        /// <param name="time"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        private IEnumerator waitThenCallback(float time, Action callback)
        {
            yield return new WaitForSeconds(time);
            callback();
        }

        /// <summary>
        /// 순수 로딩만 하는 부분
        /// </summary>
        /// <param name="name"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        IEnumerator LoadAsyncScene(string name, State state)
        {
            AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(name);

            while (!async.isDone)
            {
                yield return null;
            }

            //로딩 스탭 완료 콜
            loadingStepCompleteEvent.Invoke((int)state);
        }

        /// <summary>
        /// 로딩 스탭 콜백 핸들러
        /// </summary>
        /// <param name="stateIndex"></param>
        void LoadCompleteEventHander(int stateIndex)
        {
            switch ((State)stateIndex)
            {
                case State.CallLoadingScene:        //[3. 로딩씬 호출 완료되면 뚜렷해짐 실행]
                    DoFade(Fade.In, () => LoadingStepThree(1f));
                    break;
                case State.CallNextScene:           //[5. 다음씬 호출 완료시 뚜렸해짐 실행]
                    DoFade(Fade.In, () => LoadingStepFour());
                    break;
            }
        }

        /// <summary>
        /// 캔버스 페이드
        /// </summary>
        /// <param name="fade"></param>
        /// <param name="callback"></param>
        void DoFade(Fade fade, TweenCallback callback)
        {
            switch (fade)
            {
                case Fade.Out:          //사라짐
                    canvasGroup.alpha = 0f;
                    canvasGroup.DOFade(1f, 1f).OnComplete(callback);
                    break;
                case Fade.In:           //뚜렷해짐
                    canvasGroup.alpha = 1f;
                    canvasGroup.DOFade(0f, 1f).OnComplete(callback);
                    break;
            }
        }

        //==================================[로딩 스탭 부분]==================================


        /// <summary>
        /// [0. 최초 호출부분]
        /// </summary>
        /// <param name="name"></param>
        public void Load(string name)
        {
            if (!isDone)
            {
                Debug.Log("씬이 완전히 로드 되지 않았습니다. 로드가 완전히 끝난 후 다시 호출해 주세요.");
                return;
            }

            nextScene = name;
            isDone = false;
            LoadingStepOne();
        }

        /// <summary>
        /// [1. 현재 씬을 페이드 out 시킴]
        /// </summary>
        void LoadingStepOne()
        {
            DoFade(Fade.Out, () => LoadingStepTwo());
        }

        /// <summary>
        /// [2. 페이드 완료 후 로딩 화면 씬 실행 ]
        /// </summary>
        void LoadingStepTwo()
        {
            if (loadAsyncScene != null) StopCoroutine(loadAsyncScene);
            loadAsyncScene = StartCoroutine(LoadAsyncScene(loadingScene , State.CallLoadingScene));
        }

        /// <summary>
        /// [4. 뚜렷해짐 완료후 1초 대기 후 다음씬 호출]
        /// </summary>
        /// <param name="delay"></param>
        void LoadingStepThree(float delay)
        {
            Debug.Log("[LoadingStepThree] delayTime = " + delay);
            StartCoroutine(waitThenCallback(delay, () => {
                if (loadAsyncScene != null) StopCoroutine(loadAsyncScene);
                loadAsyncScene = StartCoroutine(LoadAsyncScene(nextScene, State.CallNextScene));
            }));
        }

        /// <summary>
        /// [6. 뚜렷해짐 완료후 씬 로드 완료]
        /// </summary>
        void LoadingStepFour()
        {
            isDone = true;
            loadCompleteEvent.Invoke();
        }

        /// <summary>
        /// [테스트용 코드]
        /// </summary>
        //private void Update()
        //{
        //    if(Input.GetKeyDown(KeyCode.Alpha1))
        //    {
        //        Load("1.TestScene");
        //    }

        //    if (Input.GetKeyDown(KeyCode.Alpha2))
        //    {
        //        Load("2.TestScene");
        //    }

        //    if (Input.GetKeyDown(KeyCode.Alpha3))
        //    {
        //        Load("3.TestScene");   
        //    }
        //}
    }
}
