using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Disc : MonoBehaviour {
	
	public float speed = 20f;
	public OuyaPlayer playerOwner;

	private Vector3 lastPosition;

	void Start () {
		lastPosition = transform.position;
	}

	public void SetOwner(OuyaPlayer owner) {
		playerOwner = owner;
	}

	void FixedUpdate() {
		//rigidbody.velocity = speed * rigidbody.velocity.normalized;

		Vector3 direction = new Vector3(transform.position - lastPosition);
		Ray ray = new Ray(lastPosition, direction);
		RaycastHit hit;
		if (Physics.Raycast(ray, hit, direction.magnitude)) {
			// Do something if hit
		}

		this.lastPosition = transform.position;
	}

	
	void OnCollisionEnter(Collision collision) {
//		Debug.Log(this.ToString() + " collided with " + collision.gameObject.ToString() + " " + collision.relativeVelocity);
		if (collision.gameObject.tag == "Player") {
			collision.gameObject.SendMessage("HandleDiscCollision", this);
		}
	}
	
//	void OnCollisionExit(Collision collision) {
//		if (collision.gameObject.tag == "Player") {
//			hasLeftPlayer = true;
//			Debug.Log(this.ToString() + " has left "  + collision.gameObject.ToString());
//		}
//	}
}
