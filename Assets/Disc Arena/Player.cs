using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour {

	public float speed = 3.0f;
	public float rotateSpeed = 3.0f;
	
	public Disc projectile;
	public float discSpeed = 20;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		CharacterController controller = GetComponent<CharacterController>();
		transform.Rotate (0, Input.GetAxis("Horizontal") * rotateSpeed, 0);	//Replace with ouya controller
		float curSpeed = speed * Input.GetAxis("Vertical");
		
		controller.Move(transform.forward * curSpeed * Time.deltaTime);
		
		if (Input.GetButtonDown("Fire1")) {
			Transform barrelPos = transform.FindChild("Barrel");
			Disc bullet = (Disc) Instantiate(projectile, barrelPos.position, barrelPos.rotation);
			bullet.rigidbody.AddExplosionForce(discSpeed, transform.localPosition, 2);
			bullet.speed = discSpeed;
		}
	}
	
	
	void OnCollisionEnter(Collision collision) {
		Debug.Log(this.ToString() + " collided with " + collision.gameObject.ToString() + " " + collision.relativeVelocity);
		Debug.Log(collision.gameObject.name);
		if (collision.gameObject.tag == "Disc") {
			Destroy(collision.gameObject);
		}
	}

}

