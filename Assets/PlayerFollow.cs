using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 playerpos = player.transform.position;
		playerpos.z -= 100;
		transform.position = playerpos;
	}
}
