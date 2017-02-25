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

		rb.AddForce(up * acceleration);

		if (Input.GetKey(KeyCode.LeftArrow))
			transform.Rotate(-Vector3.back * rotationSpeed * Time.deltaTime);
		else if (Input.GetKey(KeyCode.RightArrow))
			transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
	}
}
