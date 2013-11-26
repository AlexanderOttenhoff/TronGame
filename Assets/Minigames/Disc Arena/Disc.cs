using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Disc : MonoBehaviour {
	
	public float speed = 20f;
	public OuyaPlayer playerOwner;

	// Use this for initialization
	void Start () {
		//rigidbody.AddExplosionForce(transform.forward * speed, ForceMode.VelocityChange, 1);
	}

	public void SetOwner(OuyaPlayer owner) {
		playerOwner = owner;
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody.velocity = speed * rigidbody.velocity.normalized * Time.deltaTime;
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
