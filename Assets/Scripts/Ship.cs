﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour {

	public int hull = 1000;

	void TakeDamage(int amount) {
		hull -= amount;

		if (GetComponent<ShipController>().isControlled) {
			GameObject.Find("Ship Health").GetComponent<Text>().text = hull.ToString();
		}

		if (hull <= 0) {
			// Ship is dead
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.transform.tag == "Ground") {
			TakeDamage(Mathf.FloorToInt(Random.Range(10, 22)));
		}
	}
}
