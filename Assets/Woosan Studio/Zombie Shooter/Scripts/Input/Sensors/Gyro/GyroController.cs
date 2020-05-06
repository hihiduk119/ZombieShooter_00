using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.Common
{
    /// <summary>
    /// 모바일 폰의 자이로 센서의 값을 가져와 컨트롤.
    /// </summary>
    public class GyroController : MonoBehaviour
    {
        private bool gyroEnable = false;
        private Gyroscope gyro;

        private GameObject cameraContainer;
        private Quaternion rot;

        /*private void Awake()
        {
            cameraContainer = new GameObject("Camera Container");
            cameraContainer.transform.position = transform.position;
            transform.SetParent(cameraContainer.transform);

            //if(!gyroEnable)
            //{
            //    Debug.Log("자이로 활성화 실패");
            //} else
            //{
            //    Debug.Log("자이로 활성화 성공");
            //}
        }

        private void Start()
        {
            StartCoroutine(InitializeGyro());
        }

        IEnumerator InitializeGyro()
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0);
            rot = new Quaternion(0, 0, 1, 0);

            yield return new WaitForSeconds(1f);

            gyro.enabled = true;

            yield return new WaitForSeconds(1f);
        }

        private void Update()
        {
            //if (gyroEnable)
            //{
            //    transform.localRotation = gyro.attitude * rot;
            //}

            if( gyro != null)
            {
                if(gyro.enabled)
                {
                    Debug.Log("자이로 값 = " + Input.gyro.attitude.ToString());
                } else
                {
                    Debug.Log("자이로 활성 실패");
                }
            } else
            {
                Debug.Log("자이로 null");
            }
        }*/

        private void Start()
        {
            gyro = Input.gyro;
            gyro.enabled = true;
        }
    }
}
