using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	
	
	public float speed = 15;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		float hor = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		float vert = Input.GetAxis("Vertical") * speed * Time.deltaTime;
		
		//transform.Translate(new Vector3(hor, 0.0f, vert));
		
		rigidbody.AddForce(new Vector3(hor, 0.0f, vert), ForceMode.VelocityChange);
		
	}
}
