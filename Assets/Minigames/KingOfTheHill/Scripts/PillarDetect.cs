using UnityEngine;
using System.Collections;

public class PillarDetect : MonoBehaviour {
	
	public float captureProgess = 0.0f;
	public string capturedByPlayer = "";
	
	public int playerCount = 0;
	public string activePlayer = "";
	public string lastActivePlayer = "";
	
	
	void OnTriggerEnter (Collider other) {
		
		if(this.playerCount < 1) {
			this.activePlayer = other.name;
		} else {
			//Debug.Log("Another Player is active");
		}
		this.playerCount ++;
		
		if(this.playerCount > 1 || other.name != this.lastActivePlayer) {
			this.captureProgess = 0.0f;
			this.transform.parent.tag = "";
		} 
	}
		
	
	
	void OnTriggerStay (Collider other) {
		
		if(this.playerCount > 1) {
			//Debug.Log("More than one PLayer active, do nothing");
		} else {
			this.activePlayer = other.name;
			this.captureProgess = this.captureProgess + 0.5f;
			if(this.captureProgess >= 100) {
				this.capturedByPlayer = this.activePlayer;
				this.transform.parent.renderer.material = other.renderer.sharedMaterial;
				this.transform.parent.tag = "Captured";
				//Debug.Log(this.capturedByPlayer);
				//Debug.Log(other.renderer.material);
				//Debug.Log(this.transform.parent.name);
			
			}
		}
	}
	
	
	void OnTriggerExit (Collider other) {
		
		//this.lastActivePlayer = this.activePlayer;
		//this.activePlayer = "";
		this.playerCount --;
		if(this.playerCount == 0) {
			this.lastActivePlayer = other.name;
		}
		
	}
}
