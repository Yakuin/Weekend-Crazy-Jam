using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautController : MonoBehaviour {

    public float acceleration;
    public Sprite astroRight, astroLeft, astroNormal;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (Input.GetKey(KeyCode.LeftArrow))
            rb.AddForce(acceleration * -Vector3.right * Time.deltaTime);
        else if (Input.GetKey(KeyCode.RightArrow))
            rb.AddForce(acceleration * Vector3.right * Time.deltaTime);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        bool spriteChanged = false;
        if (rb.velocity.y > 0.3f || rb.velocity.y < 0.3f)
        {
            if (rb.velocity.x > 0.3f)
            {
                sr.sprite = astroRight;
                spriteChanged = true;
            }
            else if (rb.velocity.x < -0.3f)
            {
                sr.sprite = astroLeft;
                spriteChanged = true;
            }
        }
        if(!spriteChanged)
        {
            sr.sprite = astroNormal;
        }
    }
}
