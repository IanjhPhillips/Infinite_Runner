using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Cactus obstacle class
//Handles movement, respawning, and sprite changing of cacti obstacles
//Can be extended for further complexity of obstacles

//TODO:
//Generalize spawning --> less hard coded values
//fix stuttering (makes me queezy)

public class Cactus : MonoBehaviour {

    private const int spriteCount = 3;

    //component attributes
    private Rigidbody2D rigidBody;
    private PolygonCollider2D poly;
    private SpriteRenderer spriteRender;

    //spawn attributes
    private static float spawnDistance = 24f;
    private static Vector3 spawn = new Vector3(spawnDistance, -4.5f, 0f);
    private static float spawnRandomMax = 4f;
    private static Cactus lastCactus;
    private static Sprite[] cactiSprites;

    //movement attributes
    private static float endX = -16f;
    private static float speed = 15f;
    private static float acceleration = 0.01f;

    

    private void Awake()
    {
        

        //Keeps track of furthest back cactus, uses that as reference point for respawning
        if (lastCactus == null)
        {
            lastCactus = this;
            this.gameObject.transform.position = spawn;
            cactiSprites = new Sprite[spriteCount];
            for (int i = 0; i < spriteCount; i++)
            {
                cactiSprites[i] = (Sprite) Resources.Load<Sprite>("Sprites/Obstacles/cactus" + i);
            }
        }
        else
            Respawn();
    }

    //initialize attributes
    void Start () {

        poly = this.GetComponent<PolygonCollider2D>();
        spriteRender = this.GetComponent<SpriteRenderer>();
        rigidBody = this.GetComponent<Rigidbody2D>(); 

        speed = 16f;
        endX = -30f;
        spawnDistance = 24f;
        spawnRandomMax = 4f;

    }
	
	//move cacti
    //check reset condition -> pass random index for new cacti sprite to Respawn()
	void Update () {

        speed += acceleration*Time.deltaTime;
        rigidBody.velocity = new Vector2(-speed, 0f);

        if (gameObject.transform.position.x <= endX)
        {
            Respawn((int)Random.Range(0,3));
        }

    }

    //resets position
    private void Respawn()
    {

        //create a new collider
        //Destroy(poly);
        //poly = this.gameObject.AddComponent<PolygonCollider2D>();
        //poly.isTrigger = true;
        this.transform.position = spawn + new Vector3(lastCactus.gameObject.transform.position.x + Random.Range(-spawnRandomMax, spawnRandomMax), 0f, 0f);
        lastCactus = this;

    }

    //takes an index for new cacti sprite, resets sprite and collider
    //resets position
    private void Respawn(int index) {

        //Update sprite using index in cacti sprite array
        if (!(cactiSprites.Length <= index || cactiSprites[index] == null))
            spriteRender.sprite = cactiSprites[index];
        else print("bad index");

        //reset collider and position
        Destroy(poly);
        poly = this.gameObject.AddComponent<PolygonCollider2D>();
        poly.isTrigger = true;
        this.transform.position = spawn + new Vector3(lastCactus.gameObject.transform.position.x + Random.Range(-spawnRandomMax,spawnRandomMax), 0f, 0f);
        lastCactus = this;

    }
}
