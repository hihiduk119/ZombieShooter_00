using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 화면 전체에 폭발을 만듬.
    /// </summary>
    public class ExplosionFactory : MonoBehaviour
    {
        //싱글톤 패턴 인스턴스
        static public ExplosionFactory Instance;
        //폭발 객체의 종류
        public enum ExplosionType
        {
            BlueDemonFire,
            FunkyMixJokerFire,
            GreenWildFire,
            PurpleDarkFire,
            RedOriginalFire,
            RedOriginalFireNoSmoke,
            ClassicHeavyBomb,
        }

        [Header("[폭발 프리팹 세팅 리스튼")]
        public List<ExplosionSetting> settings = new List<ExplosionSetting>();

        public Transform Root;

        //폭발 포지션
        private List<Transform> points;

        private void Awake()
        {
            //싱글톤 생
            Instance = this;

            points = new List<Transform>(Root.GetComponentsInChildren<Transform>());

            //points.ForEach(value => Debug.Log(value.name));
        }

        public void Show(ExplosionSetting setting)
        {
            StartCoroutine(SequentialShow(setting));
        }

        IEnumerator SequentialShow(ExplosionSetting setting)
        {
            int index = 0;
            while(points.Count > index)
            {
                int rand = Random.Range(1, setting.prefabs.Length);
                GameObject clone = Instantiate(setting.prefabs[rand]);
                //GameObject clone2 = Instantiate(settings[6].prefabs[rand]);

                clone.transform.position = points[index].position;
                //clone2.transform.position = points[index].position;

                yield return new WaitForSeconds(0.05f);
                index++;
            }
        }

        #region [-TestCode]
        public void TestRun()
        {
            Show(settings[(int)ExplosionType.RedOriginalFire]);
            GlobalDamageController.Instance.DoDamage(1000);
        }

        //private void Update()
        //{
        //    if(Input.GetKeyDown(KeyCode.A)){
        //        TestRun();
        //    }   
        //}
        #endregion
    }
}
