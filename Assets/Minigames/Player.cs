using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour {

	public float speed = 10f;
	public int ammunition = 3;
	public bool dualAnalogMovement;
	public bool isFacingWall = false;

	public OuyaPlayer playerNumber;

	void Start() {
		if (GameController.GameType != GameController.GameType.DiscArena) {
			transform.FindChild("DiscShooter").GetComponent<DiscShooter>().enabled = false;
		}
	}

	// Update is called once per frame
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
			if (inLeft != Vector2.zero) 
				transform.rotation = Quaternion.LookRotation(new Vector3(inLeft.x, 0, inLeft.y));
		}

		Move (inLeft.x, inLeft.y);
	}
	
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

