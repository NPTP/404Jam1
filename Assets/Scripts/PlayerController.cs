using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 0.001f;
    public float jumpAdjust = 1f;
    public float jumpHeight = 100f;
    public float jumpTimerAdjust = 2f;

    private float movementX;
    private float movementY;
    private float jumpMove;
    private float jumpChargeTimer;
    private float distToGround;
    private Vector3 jumpMovement;

    public Quaternion pastRotation;
    
    private Rigidbody rb;
    private Collider c;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        c = GetComponent<Collider>();
        distToGround = c.bounds.extents.y;
        jumpChargeTimer = 0f;
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
        }

        if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
        {
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

        this.pastRotation = this.transform.rotation;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb.transform.position = rb.transform.position + new Vector3(moveHorizontal * movementSpeed, 0, moveVertical * movementSpeed);
    }

    private void FixedUpdate()
    {
        rb.AddForce(jumpMovement);
    }
}
