using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Disc : MonoBehaviour {
	
	public float speed = 20f;
	public OuyaPlayer playerOwner;
	
	public void SetOwner(OuyaPlayer owner) {
		playerOwner = owner;
	}
	
	void Start() {
		rigidbody.velocity = speed * transform.TransformDirection(Vector3.forward);
	}
	
	void Update() {
		//rigidbody.velocity = speed * transform.TransformDirection(Vector3.forward) * Time.deltaTime;
		Debug.DrawRay(transform.position, rigidbody.velocity, Color.red);
		
		RaycastHit hit;
		Ray ray = new Ray(transform.position, transform.forward);
		if (Physics.Raycast(ray, out hit, speed * Time.deltaTime)) {
			Debug.DrawLine(transform.position, hit.point, Color.red);
			//Debug.DrawRay(hit.point, reflectVec, Color.green);
			
			if (hit.distance < speed * Time.deltaTime) {
				print(hit.distance.ToString() + "/" + (speed * Time.deltaTime).ToString());
			}
			
		}
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
