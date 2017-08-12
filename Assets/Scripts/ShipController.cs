using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ShipController : NetworkBehaviour {

	public float speed = 3.0f;
	[SyncVar]
	public bool isControlled;

	public GameObject controller;
	public GameObject[] shipUIElements;

	Rigidbody2D rb;

	// Use this for initialization
	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		controller = GameObject.FindGameObjectWithTag("Player");

		if (Input.GetKeyDown(KeyCode.E)) {
			if (isControlled) {
				ReleaseControl();
			} else if (controller.transform.parent == transform) {
				TakeControl(controller);
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

	public void TakeControl(GameObject ply) {
		isControlled = true;

		// Show HUD
		foreach (GameObject obj in shipUIElements) {
			obj.SetActive(true);
		}
		GameObject.Find("Ship Health").GetComponent<Text>().text = GetComponent<Ship>().hull.ToString();
		GameObject.Find("Ship Name").GetComponent<Text>().text = transform.name;

		// Lock player movement
		ply.GetComponent<PlayerController>().enabled = false;
		//ply.GetComponent<Rigidbody2D>().isKinematic = true;
	}

	public void ReleaseControl() {
		isControlled = false;

		// Hide HUD
		foreach (GameObject obj in shipUIElements) {
			obj.SetActive(false);
		}

		// Unlock player movement
		controller.GetComponent<PlayerController>().enabled = true;
		//controller.GetComponent<Rigidbody2D>().isKinematic = false;

	}

	void OnDestroy() {
		ReleaseControl();
	}
}
