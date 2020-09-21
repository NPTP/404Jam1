using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 0.001f;
    public float jumpAdjust = 1f;
    public float jumpHeight = 100f;
    public float jumpTimerAdjust = 2f;
    public AudioClip ChargeUpSound;
    private float movementX;
    private float movementY;
    private float jumpMove;
    private float jumpChargeTimer;
    private float distToGround;
    private Vector3 jumpMovement;
    private AudioSource audioSource;
    private Rigidbody rb;
    private Collider c;
    public Camera armadilloCam;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        c = GetComponent<Collider>();
        distToGround = c.bounds.extents.y;
        jumpChargeTimer = 0f;
        audioSource = GetComponent<AudioSource>();
    }

    bool IsGrounded()
    {
        distToGround = c.bounds.extents.y;
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    void Update()
    {
        bool isGrounded = IsGrounded();
        float jumpMove = 0;
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            jumpChargeTimer += Time.deltaTime;
            audioSource.enabled = true;
            if (!audioSource.isPlaying)
            {

                audioSource.clip = ChargeUpSound;
                audioSource.Play();
            }
            jumpChargeTimer += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
        {
            GetComponent<AudioSource>().Stop();

            if (jumpChargeTimer > 0.5f && jumpChargeTimer < 2f)
            {
                jumpMove = jumpHeight * jumpChargeTimer * jumpTimerAdjust;
            }
            else if (jumpChargeTimer >= 2)
            {
                jumpMove = jumpHeight * 4 * jumpTimerAdjust;
            }
            else
            {
                jumpMove = jumpHeight;
                jumpChargeTimer = 0;
            }
            jumpMovement = new Vector3(0f, jumpMove, 0f);
        }

        if (!isGrounded)
        {
            jumpChargeTimer = 0;
            jumpMovement = new Vector3(0f, 0f, 0f);
        }

        // float moveHorizontal = Input.GetAxis("Horizontal");
        // float moveVertical = Input.GetAxis("Vertical");

        // rb.transform.position = rb.transform.position + new Vector3(moveHorizontal * movementSpeed, 0, moveVertical * movementSpeed);

        float x = armadilloCam.transform.forward.x;
        float z = armadilloCam.transform.forward.z;
        float speed = 5000f * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(speed * x, 0, speed * z);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            rb.AddForce(-speed * x, 0, -speed * z);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.AddForce(-speed * z, 0, speed * x);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            rb.AddForce(speed * z, 0, -speed * x);
        }

    }

    private void FixedUpdate()
    {
        rb.AddForce(jumpMovement);
    }
}
