using UnityEngine;
using System.Collections;

public class GeneratorDetect : MonoBehaviour {
	
	public float captureProgress = 0;
	
	// Update is called once per frame
	void Update () {
		
		
		// === Generator
		// check if pillar changes status
		GameObject[] captured = GameObject.FindGameObjectsWithTag("Captured");
		
		switch (captured.Length) {
			case 0:
				break;
			case 1:
				this.captureProgress = this.captureProgress + 0.035f;	
				break;
			case 2: 
				this.captureProgress = this.captureProgress + 0.065f;
				break;
			case 3: 
				this.captureProgress = this.captureProgress + 0.1f;
				break;
		}
		
		//Debug.Log(captureProgress);
		
		if(this.captureProgress >= 100) {
			
			GameObject.Find("GameMaster").SendMessage("GameOver", null, SendMessageOptions.DontRequireReceiver);
		}
				
	}
}
