using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public enum Swipe { None, Up, Down, Left, Right };

public class SwipeManager : MonoBehaviour , IPointerDownHandler , IPointerUpHandler
{
    public float minSwipeLength = 200f;
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    public static Swipe swipeDirection;

    void Update()
    {
        DetectSwipe();

        //Debug.Log(swipeDirection.ToString());
    }

    public void DetectSwipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }

            if (t.phase == TouchPhase.Ended)
            {
                secondPressPos = new Vector2(t.position.x, t.position.y);
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                // Make sure it was a legit swipe, not a tap
                if (currentSwipe.magnitude < minSwipeLength)
                {
                    swipeDirection = Swipe.None;
                    return;
                }

                currentSwipe.Normalize();

                // Swipe up
                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
                    swipeDirection = Swipe.Up;
                    // Swipe down
                } else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
                    swipeDirection = Swipe.Down;
                    // Swipe left
                } else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
                    swipeDirection = Swipe.Left;
                    // Swipe right
                } else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
                    swipeDirection = Swipe.Right;
                }
            }
        }
        else
        {
            swipeDirection = Swipe.None;
        }


        /*if(Input.GetMouseButton(0))
        {
            if(Input.GetMouseButtonDown(0))
            {
                Debug.Log("Down");
            }

            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Up");
            }

            if (Input.GetMouseButtonUp(1))
            {
                Debug.Log("Up1");
            }

            if (Input.GetMouseButtonUp(2))
            {
                Debug.Log("Up2");
            }
        }*/
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(name + "Game Object Click in Progress");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log(name + "No longer being clicked");
    }
}
