using UnityEngine;
using System.Collections;

public class CharacterMover : MonoBehaviour {

	public float speed = 6.0F;
	public float gravity = 0.0F;
	
	private Vector3 moveDirection = Vector3.zero;
	public CharacterController controller;

	void Start(){
		// Store reference to attached component
		controller = GetComponent<CharacterController>();
	}

	void Update() 
	{
		Quaternion rotation = GameObject.Find ("CenterEyeAnchor").transform.rotation;
		//Debug.Log (new Vector2 (Input.GetAxis ("Vertical"), Input.GetAxis ("Horizontal")));
		//moveDirection = rotation * Vector3.forward * Input.GetAxis("Vertical") + rotation * Vector3.right * Input.GetAxis("Horizontal");
        //moveDirection *= speed;
        float vertical = (OVRInput.GetDown(OVRInput.Button.DpadUp) ? speed : 0) - (OVRInput.GetDown(OVRInput.Button.DpadDown) ? speed : 0);
        float horitzontal = (OVRInput.GetDown(OVRInput.Button.DpadRight) ? speed : 0) - (OVRInput.GetDown(OVRInput.Button.DpadLeft) ? speed : 0);
        moveDirection = rotation * Vector3.forward * vertical + rotation * Vector3.right * horitzontal;
		// Character is on ground (built-in functionality of Character Controller)
//		if (controller.isGrounded) 
//		{
			// Use input up and down for direction, multiplied by speed
			//moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			//moveDirection = transform.TransformDirection(moveDirection);
			//moveDirection *= speed;
//		}
		// Apply gravity manually.
		//moveDirection.y -= gravity * Time.deltaTime;
		// Move Character Controller

		controller.Move(moveDirection * Time.deltaTime);
	}
}
