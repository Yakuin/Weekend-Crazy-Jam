using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

	private int planet;
	private	int planet1;
	private	int planet2;
	private	int planet3;

	// Use this for initialization
	void Start () {
		planet = 0;
		planet1 = 0;
		planet2 = 0;
		planet3 = 0;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter (Collision other)
	{
		GameObject engine = GameObject.Find("GameUI");
		GameUI ui = engine.GetComponent<GameUI>();

		var distance = Vector3.Distance(other.transform.position, transform.position + transform.up);
		Debug.Log(distance);
		Debug.Log(other.gameObject.tag);
		Debug.Log(other.relativeVelocity.magnitude);
		if (other.gameObject.tag == "Planet" && distance > 3.7 && other.relativeVelocity.magnitude < 5)
		{
			Debug.Log("good");
			if (planet == 0)
			{
				planet = 1;
				GameUI.self.AddActionText("good", this.gameObject, Color.green);
				ui.planetscore++;
			}
		}
		else if (other.gameObject.tag == "Planet1" && distance > 2.7 && other.relativeVelocity.magnitude < 5)
		{
			if (planet1 == 0)
			{
				planet1 = 1;
				GameUI.self.AddActionText("good", this.gameObject, Color.green);
				ui.planetscore++;
			}
		}
		else if (other.gameObject.tag == "Planet2" && distance > 3.1 && other.relativeVelocity.magnitude < 5)
		{
			if (planet2 == 0)
			{
				planet2 = 1;
				GameUI.self.AddActionText("good", this.gameObject, Color.green);
				ui.planetscore++;
			}
		}
		else if (other.gameObject.tag == "Planet3" && distance > 3.3 && other.relativeVelocity.magnitude < 5)
		{
			if (planet3 == 0)
			{
				planet3 = 1;
				GameUI.self.AddActionText("good", this.gameObject, Color.green);
				ui.planetscore++;
			}
		}
		else
		{

			ui.colonistsRemaining -= 1;
			GameUI.self.AddActionText("ouch", this.gameObject, Color.red);
		}
    }
}
