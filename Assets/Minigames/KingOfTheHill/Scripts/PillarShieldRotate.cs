using UnityEngine;
using System.Collections;

public class PillarShieldRotate : MonoBehaviour {
	

	// Update is called once per frame
	void Update () {
	
		transform.Find("geo_shild_1").transform.Rotate(Vector3.down * 5);
		transform.Find("geo_shild_2").transform.Rotate(Vector3.up * 5);

	}
}
