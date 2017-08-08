using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyancy : MonoBehaviour {

	Rigidbody2D rb;
	public float buoyancyFactor = 0.9f;

	bool isWater;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		// Check if GameObject is under water
		if (isWater) {
			rb.AddForce(new Vector2(0, rb.gravityScale * 10 * buoyancyFactor));
		}
		Debug.Log(rb.gravityScale);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.name == "Water") {
			isWater = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Water") {
			isWater = false;
		}
	}
}
