using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float horizontal;
    public float speed; 
    private Rigidbody2D myRB;

    public GameObject groundCheck = null;
    public bool grounded;
    public float jumpHeight; // how high the player jumps normally
    public float bounceHeight; // how high the player jumps when on a bounce pad
    private float jump;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>(); // gets the rigidbody component on the player
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    // calls on the movement function every other frame
    void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        jump = Input.GetAxisRaw("Jump");

        PlayerMove();
    }

    void PlayerMove()
    {
        float currentYVal = myRB.velocity.y; // shortcut for the y value of the velocity for the player

        if (horizontal == 1 || horizontal == -1)
        {
            myRB.velocity = new Vector2(horizontal * speed, currentYVal);
        }
        else
        {
            myRB.velocity = new Vector2(0, currentYVal);
        }

        if (Physics2D.Linecast(transform.position, groundCheck.transform.position))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        if (grounded == true)
        {
            myRB.AddForce(new Vector2(0, jump * jumpHeight));
        }
    }

    // thic function runs the bounce pad mechanic
    void OnCollisionEnter2D(Collision2D other)
    {
        // makes the player jump when they land on the bounce pad
        if (other.gameObject.tag == "Bounce") 
        {
            myRB.AddForce(new Vector2(0, bounceHeight));
        }
    }
}
