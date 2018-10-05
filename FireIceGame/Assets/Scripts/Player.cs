using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 10;
    public float jumpForce = 100;
    //public float aimOffset = 1;
    public Transform groundCheck;
    public Transform aim;

    private Rigidbody2D rb2d;
    private bool grounded;
    
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        grounded = Physics2D.OverlapPoint(groundCheck.position);
        if (Input.GetAxis("Horizontal") != 0) {
            rb2d.AddForce(new Vector2(speed * Input.GetAxis("Horizontal"), 0));
        }
        if (Input.GetButton("Jump") && grounded) {
            rb2d.AddForce(new Vector2(0, jumpForce));
        }
        float x = Input.GetAxisRaw("RightHorizontal");
        float y = Input.GetAxisRaw("RightVertical");
       
        Vector3 direction = new Vector3(x, y, 0);
        Debug.Log(direction);
        aim.right = direction;
    }
}
