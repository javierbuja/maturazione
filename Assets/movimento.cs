using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimento : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;

    private Vector2 MoveDirection;

    // Update is called once per frame
    void Update()
    {
        // Processing Imputs
        ProcessingInputs();
    }

    private void FixedUpdate()
    {
        // Physics Calculations
        Move();
    }

    void ProcessingInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        MoveDirection = new Vector2(moveX, moveY);
    }

    void Move()
    {
        rb.velocity = new Vector2(MoveDirection.x * moveSpeed, MoveDirection.y * moveSpeed);
    }
}
