using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float acceleration;
	public float rotationSpeed = 30f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		Rigidbody rb = GetComponent<Rigidbody>();
		Vector3 up = transform.up * Input.GetAxis ("Vertical");

		if (Input.GetKey(KeyCode.UpArrow))
		{
			// TODO : GUI Stuff -> Addforce as long as we have fuel
			rb.AddForce(up * acceleration);
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			// TODO : Same
			rb.AddForce(-up * acceleration);
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
			transform.Rotate(-Vector3.back * rotationSpeed * Time.deltaTime);
		else if (Input.GetKey(KeyCode.RightArrow))
			transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
	}
}
