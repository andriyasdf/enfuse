﻿using System;
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

	// Use this for initialization
	void Start() {
		healthCount = GameObject.Find("Player Health");
		bitsCount = GameObject.Find("Player Bits");
	}
	
	// Update is called once per frame
	void Update() {
		
	}

	public void TakeDamage(int amount) {
		if (!isServer) return;

		health -= amount;

		if (health <= 0) {
			health = 0;
			Destroy(gameObject);
		}
	}

	public void AddBits(int amount) {
		bits += amount;
	}

	void OnHealthUpdate(int health) {
		healthCount.GetComponent<Text>().text = health.ToString();
	}

	void OnBitsUpdate(int bits) {
		bitsCount.GetComponent<Text>().text = bits.ToString();
	}
}
