using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 1;
    public float rotationSpeed = 0;

    private float movementX;
    private float movementY;
    private bool jump;

    
    private Rigidbody rb;

    public float jumpHeight = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = false;
    }

    void Update()
    {
        if (rb.velocity.y == 0)
        {
            jump = false;
        }
        
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float jumpMovement = 0f;
        if (Input.GetKeyDown(KeyCode.Space) && !jump)
        {
            jumpMovement = jumpHeight;
            jump = true;
        }
        Vector3 movement = new Vector3(moveHorizontal, jumpMovement, moveVertical);
        rb.AddForce(movement * movementSpeed);
    }

}
