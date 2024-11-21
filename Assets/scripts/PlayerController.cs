using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI countText;
    private float speed = 5f; // Movement speed
    private float xDirection;
    private float zDirection;
    private int count;
    public GameObject winTextObject;

    public float jumpForce = 5f; // Jump strength
    private bool isGrounded = true;
    private Rigidbody rb;

    // Custom gravity settings
    public float customGravity = -20f; // Adjust this value to make gravity stronger or weaker

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void Update()
    {
        MoveBall();
        JumpBall();
    }

    void FixedUpdate()
    {
        // Apply custom gravity in FixedUpdate
        ApplyCustomGravity();
    }

    private void MoveBall()
    {
        xDirection = Input.GetAxis("Horizontal");
        zDirection = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(xDirection, 0, zDirection);
        rb.AddForce(direction * speed);
    }

    private void JumpBall()
    {
        // Check if the player is pressing the space bar and is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Prevent multiple jumps while in the air
        }
    }

    private void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 10)
        {
            winTextObject.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the player is touching the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Set grounded to true when touching the ground
        }
    }

    void OnCollisionStay(Collision collision)
    {
        // Keep grounded state true while staying on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // Custom gravity applied here to the player's Rigidbody
    private void ApplyCustomGravity()
    {
        if (!isGrounded)
        {
            rb.AddForce(Vector3.up * customGravity, ForceMode.Acceleration);
        }
    }
}




