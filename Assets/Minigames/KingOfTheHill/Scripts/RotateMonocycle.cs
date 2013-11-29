using UnityEngine;
using System.Collections;

public class RotateMonocycle : MonoBehaviour {


	public float rotationSpeed;
	//GameObject monocycle = this.transform.parent;

	void Start () {
		this.rotationSpeed = transform.parent.transform.parent.transform.parent.rigidbody.velocity.magnitude;
	}

	void Update () {
	
		this.rotateWheel(this.rotationSpeed);
		Debug.Log (this.rotationSpeed);
		//Debug.Log(rigidbody.velocity);

	}

	void rotateWheel (float speed) {

		transform.Rotate(Vector3.right * speed);

	} 
}
