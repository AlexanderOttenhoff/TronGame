using UnityEngine;
using System.Collections.Generic;
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

	public Object ExplosionPrefab;

	CharacterController controller;
	public OuyaPlayer playerNumber;

	void Start() {
		Reset();
		dashStartTime = Time.time;
	}

	public void Reset() {
		if (GameController.currentGame != GameController.GameType.DiscArena) {
<<<<<<< HEAD
			//transform.FindChild("DiscShooter").GetComponent<DiscShooter>().enabled = false;
=======
//			transform.FindChild("DiscShooter").GetComponent<DiscShooter>().enabled = false;
>>>>>>> ef5dd86000d8211e61f07decafeb98d47badd438
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
				rigidbody.velocity = dashDirection * dashSpeed;
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
		rigidbody.MovePosition(new Vector3(rigidbody.position.x, 0, rigidbody.position.z));
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

	private void Move(float mvX, float mvY) {
		rigidbody.velocity = speed * new Vector3(mvX, 0, mvY);
	}

	private void Die() {
		//Initiate death animation
        Debug.Log("I'm Dead :`(");
		enabled = false;
		Instantiate(ExplosionPrefab, transform.position, transform.rotation);
		Destroy (this.gameObject);
	}

	void OnControllerColliderHit(ControllerColliderHit hit){
		if (GameController.currentGame == GameController.GameType.DiscArena) {
			if (hit.gameObject.tag == "Disc") {
				Disc disc = hit.gameObject.GetComponent<Disc>();
				HandleDiscCollision(disc);
			}
		}
	}

	public void HandleDiscCollision(Disc disc) {
		if (disc.playerOwner == playerNumber) { 
			Destroy(disc.gameObject);
			ammunition += 1;
			isFacingWall = false;
		} else {
            Die();
		}
	}

}

