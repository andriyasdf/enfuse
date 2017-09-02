using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	public float attractRadius = 8.0f;
	public float attractFactor = 0.8f;

	Rigidbody2D rb;
	Collider2D pickupCol;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		pickupCol = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, attractRadius, 1 << LayerMask.NameToLayer("Player"));

		if (players.Length == 0) return;
		Collider2D closestPlayer = players[0];

		// Find the nearest player within pickup radius
		foreach (Collider2D col in players) {
			if (col.Distance(pickupCol).distance > closestPlayer.Distance(pickupCol).distance) {
				closestPlayer = col;
			}
		}

		// Float pickup towards nearest player
		Vector2 dist = closestPlayer.GetComponent<Rigidbody2D>().position - rb.position;
		rb.AddForce(dist * attractFactor);
	}

	public virtual void PickupItem(GameObject ply) {
		Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Player") {
			Physics2D.IgnoreCollision(col.collider, pickupCol);
			PickupItem(col.gameObject);
		}
	}
}
