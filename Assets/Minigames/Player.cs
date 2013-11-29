using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour {

	public float speed = 10f;
	public float rotateSpeed = 3.0f;
	
	public float turnSmoothing = 15f;   // A smoothing value for turning the player.
	public float speedDampTime = 0.1f;  // The damping for the speed parameter

	public int ammunition = 3;
	public bool dualAnalogMovement;
	public bool isFacingWall = false;

	private Vector3 dashDirection;
	private float dashStartTime;
	public int dashSpeed = 200;
	public float dashDuration = .25f;
	public float dashCD = 1.0f;
	private bool inDash = false;

	CharacterController controller;
	public OuyaPlayer playerNumber;

	void Start() {
		Reset();
		dashStartTime = Time.time;
		controller = GetComponent<CharacterController>();
	}

	public void Reset() {
		if (GameController.currentGame != GameController.GameType.DiscArena) {
			transform.FindChild("DiscShooter").GetComponent<DiscShooter>().enabled = false;
		}
	}

	void Update () {
		float now = Time.time;
		bool inShoot = OuyaInput.GetButtonDown(OuyaButton.LB, playerNumber) || OuyaInput.GetButtonDown(OuyaButton.RB, playerNumber);
		Vector2 inLeft = new Vector2(OuyaInput.GetAxis(OuyaAxis.LX, playerNumber), OuyaInput.GetAxis(OuyaAxis.LY, playerNumber));
		Vector2 inRight = new Vector2(OuyaInput.GetAxis(OuyaAxis.RX, playerNumber), OuyaInput.GetAxis(OuyaAxis.RY, playerNumber));

		if(OuyaInput.GetButtonDown(OuyaButton.LB, playerNumber)) {
			if(inRight != Vector2.zero) 
				playerDash(inRight, now);
			else if(inLeft != Vector2.zero)
				playerDash (inLeft, now);
		}

		if(inDash) {
			if(now - dashStartTime > dashDuration)
			{
				inDash = false;
			} else {
				controller.Move(dashDirection * dashSpeed * Time.deltaTime);
			}
		} else {
			Move (inLeft.x, inLeft.y);
			if (dualAnalogMovement) {
				if (inRight != Vector2.zero) {
					objectRotate(inRight.x, inRight.y);
				}
			} else {
				if (inLeft != Vector2.zero) {
					objectRotate(inLeft.x, inLeft.y);
				}
			}
		}
	}

	void objectRotate (float horizontal, float vertical)
	{
		// Create a new vector of the horizontal and vertical inputs.
		Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
		
		// Create a rotation based on this new vector assuming that up is the global y axis.
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
		
		// Create a rotation that is an increment closer to the target rotation from the player's rotation.
		Quaternion newRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, turnSmoothing * Time.deltaTime);
		
		// Change the players rotation to this new rotation.
		rigidbody.MoveRotation(newRotation);
	}

	void playerDash(Vector2 dashDir, float now) {
		if(inDash == false && now - dashStartTime > dashCD) {
			dashDirection = new Vector3(dashDir.x, 0.0f, dashDir.y);
			transform.rotation = Quaternion.LookRotation(dashDirection, Vector3.up);
				
			inDash = true;
			dashStartTime = now;
		}
	}

	//		float now = Time.time;
	
	//		if (inSword) {
	//			Vector3 swing;
	//			if(rx != 0f || ry != 0f) {
	//				swing = new Vector3(rx, 0.0f, ry);
	//				hand.transform.rotation = Quaternion.LookRotation(swing, Vector3.up);
	//				
	//			} else {
	//				swing = new Vector3(0.0f, 0.0f, 0.0f);
//				hand.transform.rotation = transform.localRotation;
//			}
//			swingStart = now;
//			sword.SetActive(true);
//		}
//		if(sword.activeSelf) {
//			if(now - swingStart > swingLength) {
//				sword.SetActive (false);
//			} else {
//				hand.transform.Rotate(Vector3.up * Time.deltaTime * swingSpeed);
//			}
//		}
		
//		if (inDash) {
//			if(dash == false && now - dashStart > dashCD) {
//				if(rx != 0f || ry != 0f) {
//					movement = new Vector3(rx, 0.0f, ry);
//					transform.rotation = Quaternion.LookRotation(movement, Vector3.up);
//					
//					dash = true;
//					dashStart = now;
//				}
//			}
//		} 
//		if(now - dashStart > dashLength)
//		{
//			dash = false;
//		}
//		if(dash) {
//			controller.Move(movement * dashSpeed * Time.deltaTime);
//		} else {
//			movement = new Vector3(h, 0.0f, m);
//			controller.Move(movement * speed * Time.deltaTime);
//			if(h != 0f || m != 0f)
//				rotating(h, m);
//		}
	
	private void Move(float mvX, float mvY) {
		controller.Move(speed * new Vector3(mvX, 0, mvY) * Time.deltaTime);

	}

	private void Die() {
		//Initiate death animation
		enabled = false;
		Destroy (this);
	}

	void OnControllerColliderHit(ControllerColliderHit hit){
		if (hit.gameObject.tag == "Disc") {
			Disc disc = hit.gameObject.GetComponent<Disc>();
			HandleDiscCollision(disc);
		}
	}

	public void HandleDiscCollision(Disc disc) {
		if (disc.playerOwner == playerNumber) { 
			Destroy(disc.gameObject);
			ammunition += 1;
			isFacingWall = false;
		} else {
			enabled = false;
		}
	}

}

