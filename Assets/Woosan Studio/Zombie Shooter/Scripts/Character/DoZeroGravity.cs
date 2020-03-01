using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public class DoZeroGravity : MonoBehaviour
    {
        public ChildsTransform childsTransform;
        //cache
        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        private void Start()
        {
            //ChildsTransform 에서 Awake에서 transforms List를 만드는데 싱크차이로 못가져올수 있기 때문에
            //Start에서 처리.
            //
            Rigidbody rd = null;

            childsTransform.GetTransforms().ForEach(value =>
            {
                rd = value.GetComponent<Rigidbody>();
                if (rd != null) { rigidbodies.Add(rd); }
            });
        }

        //리지드 바디를 그라비티 제로 상태로 변경.
        public void ZeroGravity(Vector3 velocity)
        {
            rigidbodies.ForEach(value => {
                value.useGravity = false;
                value.velocity = new Vector3(
                    Random.Range((int)velocity.x / 2, (int)velocity.x * 1.5f)
                    ,Random.Range((int)velocity.y / 2, (int)velocity.y * 1.5f)
                    ,0);
            });
        }

        /// <summary>
        /// 제로 그라비티 상태로 만든 부분 초기화
        /// </summary>
        public void Initialize()
        {
            rigidbodies.ForEach(value => {
                value.useGravity = true;
                value.velocity = Vector3.zero;
                value.angularVelocity = Vector3.zero;
            });
        }
    }
}
