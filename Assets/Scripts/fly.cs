﻿using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


public class fly : MonoBehaviour {
	
	private float distanceToGround;
	
	public float speed = 0;
	
	public float accel = 2;
	
	public float maxSpeed = 20;
	
	private FirstPersonController movementScript;
	
	private bool isFlying = false;
	
	private Camera cam;
	
	public float camShakeSpeed = 0.1f;
	
	private float timeFlying;
	

	

	// Use this for initialization
	void Start () {
		distanceToGround = GetComponent<Collider>().bounds.extents.y;
		movementScript = GetComponent<FirstPersonController>();
		cam = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		
		//Only fly if already in the air
		if (!isGrounded()) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				isFlying = true;
				movementScript.setFlying(true);
			}
			
			
			if (isFlying) {
				if (Input.GetKey(KeyCode.Space)) {
					timeFlying += camShakeSpeed;
					shake(cam, timeFlying);
					movementScript.setUpSpeed(speed);
					if (speed < maxSpeed) {
						speed += accel;
					} else {
						speed = maxSpeed;
					}			
				} else {
					timeFlying = 0;
					speed = 0;
					isFlying = false;
					movementScript.setFlying(false);
					
					//Resets the camera to the default position
					cam.transform.Rotate(new Vector3(0,0,0));
				}
			}
			
		}
		

	}
	
	
	//Sends a raycast straight down to check if the player is on the ground.
	private bool isGrounded() {
		return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f);
	}
	
	private void shake(Camera cam, float time) {
		 cam.transform.Rotate(new Vector3(0, 0, Mathf.Sin(time)/2));
	}
}