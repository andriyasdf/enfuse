using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyancy : MonoBehaviour {

	float buoyancy = 12;
	float viscosity = 1.2f;
	internal bool inWater = false;

	Rigidbody2D rb;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		// Check if GameObject is under water
		if (inWater) {
			rb.AddForce(Vector2.up * rb.mass * buoyancy);
			rb.AddForce(rb.velocity * -1 * viscosity);

			// Get center of buoyancy
			if (GetComponent<PolygonCollider2D>() != null) {
				Vector2[] points = GetComponent<PolygonCollider2D>().points;
				Vector2[] uwPoints;

				/*foreach (Vector2 point in points) {
					if (point.y <= 0) {
						uwPoints = 
					}
				}*/
			} else if (GetComponent<BoxCollider2D>() != null) {
				//Vector2[] points = GetComponent<BoxCollider2D>().bo;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Water") {
			inWater = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Water") {
			inWater = false;
		}
	}
}
