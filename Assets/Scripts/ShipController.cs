using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour {

	public float speed = 3.0f;
	public bool isControlled;

	public GameObject[] shipUIElements;
	public GameObject ply;


	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		ply = GameObject.FindGameObjectWithTag("Player");

		if (Input.GetKeyDown(KeyCode.E)) {
			if (isControlled) {
				ReleaseControl();
			} else if (ply.transform.parent == transform) {
				TakeControl(ply);
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

		// Hide HUD
		foreach (GameObject obj in shipUIElements) {
			obj.SetActive(true);
		}
		GameObject.Find("Ship Health").GetComponent<Text>().text = GetComponent<Ship>().hull.ToString();
		GameObject.Find("Ship Name").GetComponent<Text>().text = transform.name;

		// Lock player movement
		ply.GetComponent<PlayerController>().enabled = false;
	}

	public void ReleaseControl() {
		isControlled = false;

		// Hide HUD
		foreach (GameObject obj in shipUIElements) {
			obj.SetActive(false);
		}

		// Unlock player movement
		ply.GetComponent<PlayerController>().enabled = true;
	}

	void OnDestroy() {
		ReleaseControl();
	}
}
