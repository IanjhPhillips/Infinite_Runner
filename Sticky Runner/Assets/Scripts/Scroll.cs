using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class handles scrolling parallax background
//Updates texture offset each frame

public class Scroll : MonoBehaviour {

	public float baseSpeed = 0.5f;
	public float acceleration = 0.00004f;
	private float speed;

	// Use this for initialization
	void Start () {
		
		speed = baseSpeed;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		speed += acceleration;
		Vector2 offSet = new Vector2 (Time.time*speed, 0);
		gameObject.GetComponent<Renderer>().material.mainTextureOffset = offSet;

	}
}
