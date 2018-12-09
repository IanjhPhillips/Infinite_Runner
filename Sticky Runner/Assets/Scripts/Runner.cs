using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class handles player character
//Controls animation and player movement (jump)
//Handles collision detection with obstacles

public class Runner : MonoBehaviour {

    //Component attributes
    private Animator spriteAnimator;
    private Rigidbody2D rigidBody;

    //keeps track of whether or not player can/should be able to jump
    private bool grounded;

    private void OnTriggerEnter2D(Collider2D collider) {

        if (collider.transform.tag == "Cactus") {
          
            GameManager.LoadScene(0, true); //loads the current level
        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        
        if (collision.transform.tag == "Jumpable") {
        
            grounded = true; //can jump
        }

    }

    private void OnCollisionExit2D(Collision2D collision) {

        if (collision.transform.tag == "Jumpable") {

            grounded = false; //cannot jump
        }

    }

    //initialization
    void Start () {

        grounded = true;
        spriteAnimator = this.gameObject.GetComponent<Animator>();
        rigidBody = this.GetComponent<Rigidbody2D>(); 

	}
	
	//Handles player input
	void Update () {

        
        if (Input.GetKeyDown("space") && grounded ) {
            
            rigidBody.gravityScale = 1f;
            rigidBody.AddForce(new Vector2(0f, 28f), ForceMode2D.Impulse);
            
        }
        //When player either lets go or reaches maximum jump, gravity increases
        //This causes the falling to feel more responsive and real life like
        else if (Input.GetKeyUp("space") || rigidBody.velocity.y < 0) {

            rigidBody.gravityScale = 5f; 

        }
    }
}
