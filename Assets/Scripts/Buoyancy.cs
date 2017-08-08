using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyancy : MonoBehaviour {

	public float buoyancy = 20;
	public float viscosity = 0.9f;

	Rigidbody2D rb;
	bool isWater;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		// Check if GameObject is under water
		if (isWater) {
			rb.AddForce(Vector2.up * buoyancy);
			rb.AddForce(rb.velocity * -1 * viscosity);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.name == "Water") {
			isWater = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.name == "Water") {
			isWater = false;
		}
	}
}
