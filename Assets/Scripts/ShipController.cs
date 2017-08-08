using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

	public float speed = 3.0f;

	Rigidbody2D rb;
	bool isControlled;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		GameObject controller = GameObject.Find("Character");

		if (Input.GetKeyDown(KeyCode.E) &&
			controller.transform.parent == transform) {

			if (isControlled) {
				// Unlock player movement
				controller.GetComponent<PlayerController>().enabled = true;


				isControlled = false;
			} else {
				// Lock player movement
				controller.GetComponent<PlayerController>().enabled = false;

				isControlled = true;
			}
			
		}

		if (isControlled) {
			// Prevent player from moving in local space
			Vector3 localVelocity = transform.InverseTransformDirection(controller.GetComponent<Rigidbody2D>().velocity);
			localVelocity.x = 0;
			localVelocity.z = 0;

			controller.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(localVelocity);

			float move = Input.GetAxis("Horizontal");

			// Horizontal movement
			if (GetComponent<Buoyancy>().inWater) {
				if (rb.velocity.x < speed) {
					rb.velocity = new Vector2(move * speed, rb.velocity.y);
				} else {
					rb.velocity = new Vector2(speed, rb.velocity.y);
				}
			}
		}	
	}
}
