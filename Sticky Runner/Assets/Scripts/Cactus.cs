using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Cactus : MonoBehaviour {

    private const int spriteCount = 3;

    private static Cactus lastCactus;

    private PolygonCollider2D poly;

    private SpriteRenderer spriteRender;

    private static float spawnDistance = 24f;
    private static Vector3 spawn = new Vector3(spawnDistance, -4.5f, 0f);
    private static float spawnRandomMax = 4f;

    private static float endX = -16f;
    private static float speed = 15f;
    private static float acceleration = 0.001f;

    private static Sprite[] cactiSprites;

    private void Awake()
    {
        poly = this.GetComponent<PolygonCollider2D>();
        if (lastCactus == null)
        {
            lastCactus = this;
            this.gameObject.transform.position = spawn;
            cactiSprites = new Sprite[spriteCount];
            for (int i = 0; i < spriteCount; i++)
            {

                // cactiSprites[i] = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Obstacles/cactus" + i, typeof(Sprite));
                cactiSprites[i] = (Sprite) Resources.Load<Sprite>("Sprites/Obstacles/cactus" + i);
                print(cactiSprites[i]);

            }
        }
        else
            Respawn();
    }


    // Use this for initialization
    void Start () {

        

        speed = 15f;
        endX = -16f;
        spawnDistance = 24f;
        spawnRandomMax = 4f;

        spriteRender = this.GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {

        //spawnDistance += acceleration;
        spawn = new Vector3(spawnDistance, -4.5f, 0f);
        //spawnRandomMax -= acceleration;

        speed += acceleration;
        gameObject.transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);
        if (gameObject.transform.position.x <= endX)
        {
            Respawn((int)Random.Range(0,3));
        }

    }

    private void Respawn()
    {


        //spriteRender.sprite = cactiSprites[index];
        Destroy(poly);
        poly = this.gameObject.AddComponent<PolygonCollider2D>();
        poly.isTrigger = true;
        this.transform.position = spawn + new Vector3(lastCactus.gameObject.transform.position.x + Random.Range(-spawnRandomMax, spawnRandomMax), 0f, 0f);
        lastCactus = this;

    }

    private void Respawn(int index) {

        if (!(cactiSprites.Length <= index || cactiSprites[index] == null))
            spriteRender.sprite = cactiSprites[index];
        else print("bad index");

        Destroy(poly);
        poly = this.gameObject.AddComponent<PolygonCollider2D>();
        poly.isTrigger = true;
        this.transform.position = spawn + new Vector3(lastCactus.gameObject.transform.position.x + Random.Range(-spawnRandomMax,spawnRandomMax), 0f, 0f);
        lastCactus = this;

    }
}
