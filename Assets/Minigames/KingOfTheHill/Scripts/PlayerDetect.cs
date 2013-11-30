using UnityEngine;
using System.Collections;

public class PlayerDetect : MonoBehaviour {
	
	public float playerProgress = 0.0f;
	public int ownCaptured = 0;
	
	
	void Update () {
		
		GameObject[] captured = GameObject.FindGameObjectsWithTag("Captured");
		
		this.ownCaptured = 0;
		
		foreach (GameObject own in captured) {
			if(own.renderer.sharedMaterial == this.renderer.sharedMaterial) {
				this.ownCaptured ++;
				Debug.Log (this.ownCaptured);
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
		
	}
	void getPlayerProgress (string playername) {
		
		Debug.Log (playername + " : " + this.playerProgress);
		
		//return this.playerProgress;
	}
	
	
}
