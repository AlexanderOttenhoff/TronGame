using UnityEngine;
using System.Collections;

public static class GameController {

	public enum GameType {
		None,
		DiscArena,
		Duelism,
		KingOfTheHill
	}

	public static GameType currentGame { 
		get { return _currentGame; } 
		private set { _currentGame = value; }
	}

	private static GameType _currentGame = GameType.None;

	public static int numPlayers {get; set;}

	public static void SetGame(GameType gameType) {
		switch (gameType) {
		case GameType.DiscArena:
			InitDiscArena();
			break;
		case GameType.Duelism:
			InitDuelism();
			break;
		case GameType.KingOfTheHill:
			InitKingOfTheHill();
			break;
		}
	}

	public static void InitDiscArena() {
	}

	public static void InitDuelism() {
	}

	public static void InitKingOfTheHill() {
	}

	public static void EndGame() {
		Debug.Log ("GAME OVER");
		//Bring up menu here, clear resources and eventually start next game
	}
}
