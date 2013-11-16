using UnityEngine;
using System.Collections;

public class Disc : MonoBehaviour {
	
	public float speed;
	
	// Use this for initialization
	void Start () {
			//rigidbody.AddExplosionForce(transform.forward * speed, ForceMode.VelocityChange, 1);
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody.velocity = speed * rigidbody.velocity.normalized;
	}
}
