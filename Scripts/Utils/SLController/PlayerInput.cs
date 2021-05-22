using UnityEngine;
using System.Collections;
using Game.Core;

[RequireComponent(typeof(Health))]
[RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour {

	Player player;
	Health health;

	void Start () {
		player = GetComponent<Player> ();
		health = GetComponent<Health>();
	}

	void Update () {
		if (health.IsDead()) return;

		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		player.SetDirectionalInput (directionalInput);

		if (Input.GetKeyDown (KeyCode.Space)) {
			player.OnJumpInputDown ();
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			player.OnJumpInputUp ();
		}
	}
}
