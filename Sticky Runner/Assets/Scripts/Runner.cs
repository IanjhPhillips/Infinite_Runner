using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Runner : MonoBehaviour {

    private Animator spriteAnimator;
    private Rigidbody2D rigidBody;

    private bool grounded;

    private void OnTriggerEnter2D(Collider2D collider)
    {

        
        if (collider.transform.tag == "Cactus")
        {
            GameManager.LoadScene(0, true); //loads the current level
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        
        if (collision.transform.tag == "Jumpable")
        {
            grounded = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.transform.tag == "Jumpable")
        {
            grounded = false;
        }

    }

    // Use this for initialization
    void Start () {

        grounded = true;
        spriteAnimator = this.gameObject.GetComponent<Animator>();
        rigidBody = this.GetComponent<Rigidbody2D>(); 

	}
	
	// Update is called once per frame
	void Update () {

        
        if (Input.GetKeyDown("space") && grounded ) {

            
            rigidBody.gravityScale = 1f;
            rigidBody.AddForce(new Vector2(0f, 28f), ForceMode2D.Impulse);
            
        }
        else if (Input.GetKeyUp("space") || rigidBody.velocity.y < 0)
        {
            
            rigidBody.gravityScale = 5f;   

        }


    }
}
