using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
namespace WoosanStudio.ZombieShooter.Test
{
    public class TestScrollViewMover : MonoBehaviour
    {
        RectTransform mRectTransform;

        float value = 100;

        private void Awake()
        {
            mRectTransform = GetComponent<RectTransform>();
        }

        //private void Update()
        //{
        //    if(Input.GetKeyDown(KeyCode.A))
        //    {
        //        value = mRectTransform.anchoredPosition.x;
        //        value += 100;

        //        mRectTransform.anchoredPosition = new Vector2(value, mRectTransform.anchoredPosition.y);
        //        Debug.Log(value);
        //    }

        //    if (Input.GetKeyDown(KeyCode.S))
        //    {
        //        value = mRectTransform.anchoredPosition.x;
        //        value -= 100; 

        //        mRectTransform.anchoredPosition = new Vector2(value, mRectTransform.anchoredPosition.y);
        //        Debug.Log(value);
        //    }
        //}
    }
}
