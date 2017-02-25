using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationPlanet : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameUI.BindDestination(this.transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
