using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

	
	void GameOver () {
		Debug.Log ("GAME OVER");
		
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		
		foreach (GameObject player in players) {
			player.SendMessage("getPlayerProgress", null, SendMessageOptions.DontRequireReceiver);
		}
	}
}
