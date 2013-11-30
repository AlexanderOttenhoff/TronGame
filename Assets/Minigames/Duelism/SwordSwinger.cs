using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(Collider))]
public class SwordSwinger : MonoBehaviour {

	//public Player player;

	public GameObject sword;
	public GameObject hand;
	private float swingStart = 0;
	private bool attack = false;
	public float swingSpeed = 1000;
	public float swingLength = 0.1f;
	public OuyaPlayer playerNumber;


	void Start() {
		Transform t = transform.parent;
		//player = (Player) t.GetComponent<Player>();
		sword.SetActive(false);
	}

	void Update () {
		float now = Time.time;
		if(OuyaInput.GetButtonDown(OuyaButton.RB, playerNumber)) {
			//Vector2 inRight = new Vector2(OuyaInput.GetAxis(OuyaAxis.RX, playerNumber), OuyaInput.GetAxis(OuyaAxis.RY, playerNumber));
			Swing(new Vector2(OuyaInput.GetAxis(OuyaAxis.RX, playerNumber), OuyaInput.GetAxis(OuyaAxis.RY, playerNumber)), now);
		}
		if(sword.activeSelf) {
			if(now - swingStart > swingLength) {
				sword.SetActive (false);
				hand.transform.rotation = transform.localRotation;
			} else {
				hand.transform.Rotate(Vector3.up * Time.deltaTime * swingSpeed);
			}
		}
	}

	private void Swing(Vector2 swingDir, float now) {
		if(swingDir != Vector2.zero) {
			Vector3 swing = new Vector3(swingDir.x, 0.0f, swingDir.y);
			hand.transform.rotation = Quaternion.LookRotation(swing, Vector3.up);
			
		} else {
			Vector3 swing = new Vector3(0.0f, 0.0f, 0.0f);
			hand.transform.rotation = transform.localRotation;
		}
		swingStart = now;
		sword.SetActive(true);

	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player") {
			other.gameObject.SendMessage("HandleSwordHit", this);
			Debug.Log("sword hit");
		}
	}

	void OnCollisionEnter(Collision other) {
		if(other.gameObject.tag == "Player") {
			other.gameObject.SendMessage("HandleSwordHit", this);
			Debug.Log("sword hit");
		}
	}

}
