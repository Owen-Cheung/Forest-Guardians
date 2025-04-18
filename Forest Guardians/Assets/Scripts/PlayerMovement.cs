using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private new Camera camera;
    private Rigidbody2D body;
    //private Collider2D capsuleCollider;

    private Vector2 velocity;
    private float inputAxis;

    public float speed = 8f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2);

    public bool grounded { get; private set; }
    public bool jumping { get; private set; }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        camera = Camera.main;
        //capsuleCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        HorizontalMovement();

        grounded = body.Raycast(Vector2.down);

        if (grounded)
        {
            GroundedMovement();
        }

        ApplyGravity();
    }
    private void FixedUpdate()
    {
        Vector2 position = body.position;
        position += velocity * Time.fixedDeltaTime;

        body.MovePosition(position);
    }
    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * speed, speed * Time.deltaTime);

        if (velocity.x > 0f)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (velocity.x < 0f)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
    private void GroundedMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;

        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
            jumping = true;
        }
    }
    private void ApplyGravity()
    {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;

        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }
}