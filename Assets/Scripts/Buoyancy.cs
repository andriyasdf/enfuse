using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyancy : MonoBehaviour {

	float buoyancy = 15;
	float viscosity = 0.9f;
	public bool inWater = false;

	Rigidbody2D rb;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		// Check if GameObject is under water
		if (inWater) {
			rb.AddForce(Vector2.up * rb.mass * buoyancy);
			rb.AddForce(rb.velocity * -1 * viscosity);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.name == "Water") {
			inWater = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.name == "Water") {
			inWater = false;
		}
	}
}
