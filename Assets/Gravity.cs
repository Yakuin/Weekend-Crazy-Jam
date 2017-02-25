using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
	public int gravity;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay (Collider other)
	{
		var direction = -(other.attachedRigidbody.transform.position - transform.position);
        Debug.DrawLine(other.transform.position, other.transform.position + direction, Color.blue);
		var distance = Vector3.Distance(other.transform.position, transform.position);
        if (other.attachedRigidbody)
		{
			other.attachedRigidbody.AddForce(direction * gravity / distance);
		}

	}
		
}
