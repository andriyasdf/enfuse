using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour {

	public float speed = 3.0f;
	public GameObject[] shipUIElements;

	Rigidbody2D rb;
	bool isControlled;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		GameObject ply = GameObject.FindGameObjectWithTag("Player");

		if (Input.GetKeyDown(KeyCode.E) &&
			ply.transform.parent == transform) {

			if (isControlled) {
				isControlled = false;

				// Unlock player movement
				ply.GetComponent<PlayerController>().enabled = true;

				// Hide ship HUD
				foreach (GameObject obj in shipUIElements) {
					obj.SetActive(false);
				}
			} else {
				isControlled = true;

				// Lock player movement
				ply.GetComponent<PlayerController>().enabled = false;

				// Show ship HUD
				foreach (GameObject obj in shipUIElements) {
					obj.SetActive(true);
				}
				GameObject.Find("Ship Name").GetComponent<Text>().text = transform.name;
			}
			
		}

		if (isControlled) {
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
