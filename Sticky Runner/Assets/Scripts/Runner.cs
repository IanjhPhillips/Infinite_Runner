using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Runner : MonoBehaviour {

    private Animator spriteAnimator;
    private Rigidbody2D rigidBody;

    private bool grounded;

    private void OnTriggerEnter2D(Collider2D collider)
    {

        // print("colliding");
        // print(collider.transform.tag);
        if (collider.transform.tag == "Cactus")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            print("You died!");
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // print("colliding");
        // print(collider.transform.tag);
        if (collision.transform.tag == "Jumpable")
        {
            grounded = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        // print("colliding");
        // print(collider.transform.tag);
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

        /*
        if (rigidBody.velocity.y > 0)
            rigidBody.gravityScale = 1f;
        
        else
            rigidBody.gravityScale = 10f;
        */
        if (Input.GetKeyDown("space") && grounded ) {

            
            rigidBody.gravityScale = 2f;
            rigidBody.AddForce(new Vector2(0f, 15f), ForceMode2D.Impulse);
            
        }
        else if (Input.GetKeyUp("space") || rigidBody.velocity.y < 0)
        {
            print("release");
            rigidBody.gravityScale = 5f;   

        }


    }
}
