using UnityEngine;
using System.Collections;


[RequireComponent(typeof(CharacterController))]
public class NotPlayer : MonoBehaviour {
	
	public float speed = 3.0f;
	public float rotateSpeed = 3.0f;
	
	public float turnSmoothing = 15f;   // A smoothing value for turning the player.
    public float speedDampTime = 0.1f;  // The damping for the speed parameter
	
	//public Disc projectile;
	public float dashSpeed = 200;
	private bool dash = false;
	public float dashLength = 1;
	private float dashStart = 0;
	public float dashCD = 1;
	private Vector3 movement;
	
	public GameObject hand;
	public GameObject sword;
	private float swingStart = 0;
	private bool attack = false;
	public float swingSpeed = 1000;
	public float swingLength = 0.1f;
	
	// Use this for initialization
	void Start () {
		dashStart = 0;
		movement = new Vector3(0,0,0);
		//hand = GameObject.Find("Hand");
		//sword = GameObject.Find("Hand/Sabre");
		sword.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
if(this.gameObject.name == "Duelist 1"){
		CharacterController controller = GetComponent<CharacterController>();
		//transform.Rotate (0, Input.GetAxis("Horizontal") * rotateSpeed, 0);	//Replace with ouya controller
		//float curSpeed = speed * Input.GetAxis("Vertical");
		
		float h = Input.GetAxis("Horizontal");
		//float h = SuperInputMapper.GetAxis(OuyaSDK.KeyEnum.AXIS_LSTICK_X);
		float m = Input.GetAxis("Vertical");
		//float m = SuperInputMapper.GetAxis(OuyaSDK.KeyEnum.AXIS_LSTICK_Y);
		//float rightX = SuperInputMapper.GetAxis(OuyaSDK.KeyEnum.AXIS_RSTICK_X);
		//float rightY = SuperInputMapper.GetAxis(OuyaSDK.KeyEnum.AXIS_RSTICK_Y);
		float rx = Input.GetAxis ("Joy1 Axis 3");
		float ry = Input.GetAxis ("Joy1 Axis 4");
		float now = Time.time;

		
 		if (Input.GetButtonDown("Jump")) {
			Vector3 swing;
			if(rx != 0f || ry != 0f) {
				swing = new Vector3(rx, 0.0f, ry);
				hand.transform.rotation = Quaternion.LookRotation(swing, Vector3.up);
            		
			} else {
				swing = new Vector3(0.0f, 0.0f, 0.0f);
				hand.transform.rotation = transform.localRotation;
			}
			swingStart = now;
			sword.SetActive(true);
        }
		if(sword.activeSelf) {
			if(now - swingStart > swingLength) {
				sword.SetActive (false);
			} else {
				hand.transform.Rotate(Vector3.up * Time.deltaTime * swingSpeed);
			}
		}
		
		if (Input.GetButtonDown("Fire1")) {
			if(dash == false && now - dashStart > dashCD) {
				if(rx != 0f || ry != 0f) {
					movement = new Vector3(rx, 0.0f, ry);
            		transform.rotation = Quaternion.LookRotation(movement, Vector3.up);

					dash = true;
					dashStart = now;
				}
			}
		} 
		if(now - dashStart > dashLength)
		{
			dash = false;
		}
		if(dash) {
			controller.Move(movement * dashSpeed * Time.deltaTime);
		} else {
			movement = new Vector3(h, 0.0f, m);
			controller.Move(movement * speed * Time.deltaTime);
			if(h != 0f || m != 0f)
				rotating(h, m);
        }
	}
}	
	
	void OnCollisionEnter(Collision collision) {
		Debug.Log(this.ToString() + " collided with " + collision.gameObject.ToString() + " " + collision.relativeVelocity);
		Debug.Log(collision.gameObject.name);
		//if (collision.gameObject.tag == "Sword") {
			//Destroy(collision.gameObject);
			sword.SetActive(true);
		//}
	}

//void OnTriggerEnter(Collider other) {
//       Destroy(other.gameObject);
//    }
		
	
	    
    void rotating (float horizontal, float vertical)
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

}

