using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GameObject player1;
	public GameObject player2;
	public GameObject player3;
	public GameObject player4;
	private Vector3 offset;
	public float zoomMult = 1.5f;

	// Use this for initialization
	void Start () {
		offset = transform.position;	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 zoom = player1.transform.position;
		if(player2.transform.position.magnitude > zoom.magnitude)
			zoom = player2.transform.position;
		if(player3.transform.position.magnitude > zoom.magnitude)
			zoom = player3.transform.position;
		if(player4.transform.position.magnitude > zoom.magnitude)
			zoom = player4.transform.position;
		Vector3 toFollow = (player1.transform.position + player2.transform.position + player3.transform.position + player4.transform.position)/4;
		//toFollow += offset;
		transform.position = new Vector3(toFollow.x, zoom.magnitude * zoomMult, toFollow.z - zoom.magnitude);
	}

}