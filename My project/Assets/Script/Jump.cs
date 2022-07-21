using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
     private Rigidbody2D rb;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    bool isGrounded;
    
    [Range(1,10)]
    public float jumpVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Application.targetFrameRate = 15;
    }

    void Update()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector2.up * jumpVelocity;
        }
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "ground")
        {
            isGrounded = false;
        }
    }
}
