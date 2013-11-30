using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GameObject player1;
	public GameObject player2;
	public GameObject player3;
	public GameObject player4;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position;	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 toFollow = (player1.transform.position + player2.transform.position + player3.transform.position + player4.transform.position)/4;
		transform.position = toFollow + offset;
	}

}