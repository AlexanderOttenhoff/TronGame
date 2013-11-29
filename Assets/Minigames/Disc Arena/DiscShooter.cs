using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class DiscShooter : MonoBehaviour {

	public Player player;

	public Rigidbody projectile;

	void Start() {
		Transform t = transform.parent;
		player = (Player) t.GetComponent<Player>();
	}

	void Update () {
		bool inShoot = OuyaInput.GetButtonDown(OuyaButton.RB, player.playerNumber);

		if (inShoot && player.ammunition>0 && !player.isFacingWall) {
			Fire();
		}
	}

	private void Fire() {
		Rigidbody disc = (Rigidbody) Instantiate(projectile, transform.position, transform.rotation);
		disc.rigidbody.AddForce(transform.forward.normalized);
		disc.SendMessage("SetOwner", player.playerNumber);
		player.ammunition -= 1;
	}

	void OnTriggerEnter() {
		player.isFacingWall = true;
	}

	void OnTriggerExit() {
		player.isFacingWall = false;
	}
}
