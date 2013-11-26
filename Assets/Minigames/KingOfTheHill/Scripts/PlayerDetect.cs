using UnityEngine;
using System.Collections;

public class PlayerDetect : MonoBehaviour {
	
	public float playerProgress = 0.0f;
	public int ownCaptured = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		GameObject[] captured = GameObject.FindGameObjectsWithTag("Captured");
		
		this.ownCaptured = 0;
		
		foreach (GameObject own in captured) {
			if(own.renderer.sharedMaterial.name == this.renderer.sharedMaterial.name) {
				this.ownCaptured ++;
				//Debug.Log (this.ownCaptured);
			}
		}
		
		switch (this.ownCaptured) {
			case 0:
				break;
			case 1:
				this.playerProgress = this.playerProgress + 0.035f;
				break;
			case 2: 
				this.playerProgress = this.playerProgress + 0.065f;
				break;
			case 3: 
				this.playerProgress = this.playerProgress + 0.1f;
				break;
		}
		
		//Debug.Log(this.playerProgress);

	}
	void getPlayerProgress () {
		//return this.playerProgress;
		Debug.Log(this.name + "" + this.playerProgress);
	}
}
