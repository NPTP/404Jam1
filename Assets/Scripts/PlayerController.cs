using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 0.001f;
    public float physicsSpeed = 5000f;
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
    private float camX;
    private float camZ;
    private AudioClip[] bounceSounds;
    private AudioClip[] smashSounds;
    private int roundRobinIndex = 0;
    private bool movingForward = false;
    private bool movingBackward = false;
    private bool movingLeft = false;
    private bool movingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        c = GetComponent<Collider>();
        distToGround = c.bounds.extents.y;
        jumpChargeTimer = 0f;
        audioSource = GetComponent<AudioSource>();
        bounceSounds = Resources.LoadAll<AudioClip>("bounce");
        smashSounds = Resources.LoadAll<AudioClip>("smash");
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


        if (Input.GetKeyDown(KeyCode.W))
        {
            movingForward = true;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            movingBackward = true;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            movingLeft = true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            movingRight = true;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            movingForward = false;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            movingBackward = false;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            movingLeft = false;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            movingRight = false;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(jumpMovement);
        camX = armadilloCam.transform.forward.x;
        camZ = armadilloCam.transform.forward.z;

        if (movingForward)
        {
            rb.AddForce(physicsSpeed * Time.deltaTime * camX, 0, physicsSpeed * Time.deltaTime * camZ);
        }
        else if (movingBackward)
        {
            rb.AddForce(-physicsSpeed * Time.deltaTime * camX, 0, -physicsSpeed * Time.deltaTime * camZ);
        }

        if (movingLeft)
        {
            rb.AddForce(-physicsSpeed * Time.deltaTime * camZ, 0, physicsSpeed * Time.deltaTime * camX);
        }
        else if (movingRight)
        {
            rb.AddForce(physicsSpeed * Time.deltaTime * camZ, 0, -physicsSpeed * Time.deltaTime * camX);
        }
    }

    // Plays random bounce sound
    public void PlayBounceSound()
    {
        audioSource.PlayOneShot(bounceSounds[Random.Range(0, bounceSounds.Length)]);
    }

    // Plays round robin smash sound
    public void PlaySmashSound()
    {
        if (roundRobinIndex == smashSounds.Length)
        {
            roundRobinIndex = 0;
        }
        audioSource.PlayOneShot(smashSounds[roundRobinIndex]);
        roundRobinIndex++;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bouncy")
        {
            PlayBounceSound();
        }
        else if (other.gameObject.tag == "Destructible")
        {
            PlaySmashSound();
        }
    }

}
