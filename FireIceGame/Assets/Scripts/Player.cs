using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour {
    public string horizontalLeft = "Horizontal_p1_left";
    public string horizontalRight = "Horizontal_p1_right";
    public string verticalRight = "Vertical_p1_right";
    public string jump = "Jump_p1";
    public float maxSpeed = 5f;
    public float accelleration = 5f;
    public float deceleration = 10f;
    public float jumpForce = 100f;
    public float aimDeadZone = 0.05f;
    [SerializeField] private LayerMask groundCheckMask_;
    [SerializeField] private Vector2 groundCheckSize_ = Vector2.one;
    public Transform groundCheck;
    public Transform aim;
    [FormerlySerializedAs("jumpSource_")] [SerializeField] private AudioSource oneShotSource_;
    [SerializeField] private AudioClip jumpClip_;
    [SerializeField] private AudioClip landClip_;

    private Rigidbody2D rb2d;
    private float speed = 0;
    private bool grounded;
    private bool airborne;
    private bool goingRight;

    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update () {
        //Movement
        if (Input.GetAxis(horizontalLeft) != 0) {
            if (Input.GetAxis(horizontalLeft) > 0) {
                goingRight = true;
            }
            else {
                goingRight = false;
            }
            
            if(speed < maxSpeed) {
                speed += accelleration * Time.deltaTime;
            }
            if (speed > maxSpeed) {
                speed = maxSpeed;
            }
            transform.Translate(Input.GetAxis(horizontalLeft) * speed * Time.deltaTime, 0, 0);
        }
        else {
            if(speed > 0) {
                speed -= deceleration * Time.deltaTime;
                if (goingRight) {
                    transform.Translate(speed * Time.deltaTime, 0, 0);
                }
                else {
                    transform.Translate(-speed * Time.deltaTime, 0, 0);
                }
            }
            if(speed < 0) {
                speed = 0;
            }
        }

        //Jump
        grounded = IsGrounded();
        if (Input.GetButtonDown(jump) && grounded/* && !airborne*/) {
            rb2d.AddForce(new Vector2(0, jumpForce));
            airborne = true;
            oneShotSource_.PlayOneShot(jumpClip_);
        }

        //Aim Rotation
        float x = Input.GetAxisRaw(horizontalRight);
        float y = Input.GetAxisRaw(verticalRight);
        if (x > aimDeadZone || x < -aimDeadZone || y > aimDeadZone || y < -aimDeadZone) {
            Vector3 direction = new Vector3(x, y, 0);
            aim.right = direction;
        }
    }

    private void OnCollisionEnter2D (Collision2D collision) {
        if (!airborne)
        {
            return;
        }
        if (IsGrounded()) {
            airborne = false;
            oneShotSource_.PlayOneShot(landClip_);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundCheckSize_, groundCheckMask_);
    }
}
