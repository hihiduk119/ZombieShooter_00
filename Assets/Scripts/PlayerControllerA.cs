using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerA : MonoBehaviour {

	private Rigidbody rb;
	public float speed = 5;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	void FixedUpdate () {
		//float moveHorizontal = Input.GetAxis ("Horizontal");
		//float moveVertical = Input.GetAxis ("Vertical");

        float moveHorizontal = UltimateJoystick.GetHorizontalAxis("Move");
        float moveVertical = UltimateJoystick.GetVerticalAxis("Move");

        Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);

		rb.AddForce (movement * speed);
	}
}
