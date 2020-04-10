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

        //폭발 포지션
        private List<Transform> points;

        private void Awake()
        {
            points = new List<Transform>(GetComponentsInChildren<Transform>());

            points.ForEach(value => Debug.Log(value.name));
        }

        public void Show(ExplosionSetting setting)
        {
            //points.ForEach(value =>
            //{
            //    GameObject clone = Instantiate(setting.prefabs[Random.Range(0, setting.prefabs.Length)]);
            //    clone.transform.position = value.position;
            //});

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
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.A)){
                Show(settings[(int)ExplosionType.RedOriginalFire]);
            }   
        }
        #endregion
    }
}
