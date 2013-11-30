using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]
public class DuelismPlayer : MonoBehaviour {

	public float speed = 10f;
	public float rotateSpeed = 3.0f;
	
	public float turnSmoothing = 15f;   // A smoothing value for turning the player.
	public float speedDampTime = 0.1f;  // The damping for the speed parameter

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
	public bool isAlive = true;
	public GameObject flag;
	private int captureCount = 0;
	public GameObject spawnPoint;
	public Quaternion originalRotationValue;
	public GUIText winText;

	void Start() {
		Reset();
		dashStartTime = Time.time;
		controller = GetComponent<CharacterController>();
		flag.SetActive(false);
		originalRotationValue = transform.rotation;
		winText.text = "";
	}

	public void Reset() {
		if (GameController.currentGame != GameController.GameType.DiscArena) {
//			transform.FindChild("DiscShooter").GetComponent<DiscShooter>().enabled = false;
		}
	}

	void Update () {
		if(!isAlive)
		{
			Die ();
		} if(captureCount > 2) { 
			winText.text = gameObject.transform.name;
			winText.text +=  " wins";
		}else {
			float now = Time.time;
//		bool inShoot = OuyaInput.GetButtonDown(OuyaButton.LB, playerNumber) || OuyaInput.GetButtonDown(OuyaButton.RB, playerNumber);
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
		controller.Move(speed * new Vector3(mvX, 0, mvY) * Time.deltaTime);

	}

	private void Die() {
		Vector3 newPos = transform.position;
		if(transform.position.y < 10)
			newPos.y += 2;
		if(transform.position.y < 15)
			newPos.y += 1;
		if(transform.position.x < spawnPoint.transform.position.x + 10)
			newPos.x += 9;
		else if(transform.position.x > spawnPoint.transform.position.x - 10)
			newPos.x -= 9;
		if(transform.position.z < spawnPoint.transform.position.z + 10)
			newPos.z += 9;
		else if(transform.position.z > spawnPoint.transform.position.z - 10)
			newPos.z -= 9;
		transform.position = newPos;
		transform.rotation = Random.rotation;
	//	transform.Translate(spawnPoint.transform.position * Time.deltaTime);

	}
	
	
	void OnTriggerEnter(Collider other) {
		if(other.tag == "Flag" && !flag.activeSelf) {
			other.gameObject.SetActive(false);
			flag.SetActive(true);
		}
		if(flag.activeSelf) { 
			if(other.tag == "FlagRepo") {
				flag.SetActive(false);
				captureCount += 1;
			}
		}
		if(other.tag == "Sword")
		{
			isAlive = false;
			Debug.Log("ouch ;'p");
		}
		if(!isAlive && other.tag == "Spawn") {
			isAlive = true;
			Vector3 spawnLoc = new Vector3(spawnPoint.transform.position.x, 0, spawnPoint.transform.position.z);
			transform.position = spawnLoc;
			//transform.position.y = 0;
			transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, 50);		}
	}
	

	public void HandleSwordHit(GameObject badSword) {
		isAlive = false;
		Debug.Log("hit");
	}

}

