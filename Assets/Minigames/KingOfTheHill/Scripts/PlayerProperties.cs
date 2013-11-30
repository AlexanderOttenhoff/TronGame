using UnityEngine;
using System.Collections;

public class PlayerProperties : MonoBehaviour {

	public string playerHook;

	void Start () {

		playerHook = name;
		//Debug.Log(playerHook);
	}

	string getPlayerHook () {
		return this.playerHook;
	}
}
