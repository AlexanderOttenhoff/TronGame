using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class SwordSwinger : MonoBehaviour {

	public Player player;

	public Rigidbody projectile;

	public GameObject sword;
	private float swingStart = 0;
	private bool attack = false;
	public float swingSpeed = 1000;
	public float swingLength = 0.1f;

	void Start() {
		Transform t = transform.parent;
		player = (Player) t.GetComponent<Player>();
		sword.SetActive(false);
	}

	void Update () {
		float now = Time.time;
		if(OuyaInput.GetButtonDown(OuyaButton.RB, player.playerNumber)) {
			//Vector2 inRight = new Vector2(OuyaInput.GetAxis(OuyaAxis.RX, playerNumber), OuyaInput.GetAxis(OuyaAxis.RY, playerNumber));
			Swing(new Vector2(OuyaInput.GetAxis(OuyaAxis.RX, player.playerNumber), OuyaInput.GetAxis(OuyaAxis.RY, player.playerNumber)));
		}
		if(sword.activeSelf) {
			if(now - swingStart > swingLength) {
				sword.SetActive (false);
			} else {
				hand.transform.Rotate(Vector3.up * Time.deltaTime * swingSpeed);
			}
		}
	}

	private void Swing(Vector2 swingDir, float now) {
		if(swingDir != Vector2.zero) {
			Vector3 swing = new Vector3(swingDir.x, 0.0f, swingDir.y);
			transform.rotation = Quaternion.LookRotation(swing, Vector3.up);
			
		} else {
			swing = new Vector3(0.0f, 0.0f, 0.0f);
			transform.rotation = player.transform.localRotation;
		}
		swingStart = now;
		sword.SetActive(true);

	}

	void OnTriggerEnter() {
		player.isFacingWall = true;
	}

	void OnTriggerExit() {
		player.isFacingWall = false;
	}
}
