using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour {

	public const int maxHealth = 100;

	[SyncVar(hook = "OnHealthUpdate")]
	public int health = maxHealth;

	[SyncVar(hook = "OnBitsUpdate")]
	public int bits = 0;

	GameObject healthCount;
	GameObject bitsCount;

	void Start() {
		healthCount = GameObject.Find("Player Health");
		bitsCount = GameObject.Find("Player Bits");

		// Set initial UI values
		OnHealthUpdate(health);
		OnBitsUpdate(bits);
	}

	void OnHealthUpdate(int health) {
		healthCount.GetComponent<Text>().text = health.ToString();
	}

	void OnBitsUpdate(int bits) {
		bitsCount.GetComponent<Text>().text = bits.ToString();
	}

	public void AddBits(int amount) {
		bits += amount;
	}

	public void TakeDamage(int amount) {
		if (!isServer) return;

		health -= amount;

		if (health <= 0) {
			Respawn();
		}
	}

	public void SetHealth(int amount) {
		if (!isServer)
			return;

		health = amount;

		if (health <= 0) {
			Respawn();
		}
	}

	[ClientRpc]
	void Respawn() {
		if (!isLocalPlayer) return;

		// Reset health
		health = maxHealth;

		transform.position = Vector3.zero;
	}
}
