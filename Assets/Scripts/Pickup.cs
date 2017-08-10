using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	public float pickupRadius = 10.0f;
	public float attractFactor = 0.9f;

	Rigidbody2D rb;
	Collider2D pickupCol;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		pickupCol = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickupRadius, 1 << LayerMask.NameToLayer("Player"));

		if (colliders.Length == 0) return;
		Collider2D closestPlayer = colliders[0];

		// Find the nearest player within pickup radius
		foreach (Collider2D col in colliders) {
			if (col.Distance(pickupCol).distance > closestPlayer.Distance(pickupCol).distance) {
				closestPlayer = col;
			}
		}

		// Float pickup to nearest player
		Vector2 dist = closestPlayer.GetComponent<Rigidbody2D>().position - rb.position;
		rb.AddForce(dist * attractFactor);

		if (dist.magnitude < 1.0f) {
			PickupItem(closestPlayer.gameObject);
		}
	}

	public virtual void PickupItem(GameObject ply) {
		Destroy(gameObject);
	}
}
