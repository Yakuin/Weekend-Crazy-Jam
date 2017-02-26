using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautController : MonoBehaviour {

    public float acceleration;
    public float planetRadius;
    public Transform planetCenter;
    private float angle = Mathf.PI/2;
    private bool facingRight = true;
    public Sprite astroRight, astroLeft, astroNormal;
    Animator animation;

    // Use this for initialization
    void Start () {
        planetCenter = GameObject.FindGameObjectWithTag("Planet").GetComponent<Transform>();
        animation = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.rotation = Quaternion.EulerAngles(0f, 0f, angle - Mathf.PI / 2);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!facingRight)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                facingRight = true;
            }
            animation.Play("walk");
            angle -= 1f * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (facingRight)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                facingRight = false;
            }
            animation.Play("walk");
            angle += 1f * Time.deltaTime;
        }
        else
            animation.Play("idle");
        transform.position = planetCenter.position + planetRadius * (Vector3.right * Mathf.Cos(angle)) + planetRadius * (Vector3.up * Mathf.Sin(angle));
    }
    /*
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
    */
}
