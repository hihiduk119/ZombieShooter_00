using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAhead : MonoBehaviour
{
    public Transform Target;
    public float Speed = 100f;

    public void Look(Vector3 target)
    {
        //transform.LookAt(target);

        Vector2 temp;

        temp.x = UltimateJoystick.GetHorizontalAxis("LookAhead");
        temp.y = UltimateJoystick.GetVerticalAxis("LookAhead");

        Debug.Log(temp.x + "," + temp.y + " = " + Vector2.Angle(new Vector2(-1f, 0f), temp));

        //180 - 360
        Vector3 rot = new Vector3(0, Vector2.Angle(new Vector2(-1f, 0f), temp) +180f, 0);
        transform.rotation = Quaternion.Euler(rot);
    }

    private void Update()
    {
        
        Vector3 rot = transform.localRotation.eulerAngles;
        rot.x = 0;
        transform.localRotation = Quaternion.Euler(rot);   
    }
}
