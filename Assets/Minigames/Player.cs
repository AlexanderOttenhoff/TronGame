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

	public OuyaPlayer playerNumber;

	void Start() {
		Reset();
	}

	public void Reset() {
		if (GameController.currentGame != GameController.GameType.DiscArena) {
			transform.FindChild("DiscShooter").GetComponent<DiscShooter>().enabled = false;
		}
	}

	void Update () {
		bool inShoot = OuyaInput.GetButtonDown(OuyaButton.LB, playerNumber) || OuyaInput.GetButtonDown(OuyaButton.RB, playerNumber);
		Vector2 inLeft = new Vector2(OuyaInput.GetAxis(OuyaAxis.LX, playerNumber), OuyaInput.GetAxis(OuyaAxis.LY, playerNumber));
		Vector2 inRight = new Vector2(OuyaInput.GetAxis(OuyaAxis.RX, playerNumber), OuyaInput.GetAxis(OuyaAxis.RY, playerNumber));
		
		if (dualAnalogMovement) {
			float rotation = 0f;
			if (inRight.magnitude > 0.25f) {
				transform.rotation = Quaternion.LookRotation(new Vector3(inRight.x, 0, inRight.y));
			}
		} else {
			if (inLeft != Vector2.zero) {
				transform.rotation = Quaternion.LookRotation(new Vector3(inLeft.x, 0, inLeft.y));
			}
		}
		
		Move (inLeft.x, inLeft.y);
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
		CharacterController controller = GetComponent<CharacterController>();
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

