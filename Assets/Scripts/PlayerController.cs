using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 3.0f;
	public float jumpHeight = 2.0f;

	Rigidbody2D rb;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate() {
        float move = Input.GetAxis("Horizontal");
		// TODO: use rigidbody force vectors

		// Horizontal movement
		if (rb.velocity.x < speed) {
			rb.velocity = new Vector2(move * speed, rb.velocity.y);
		} else {
			rb.velocity = new Vector2(speed, rb.velocity.y);
		}

		// Jumping
		if (Input.GetButtonDown("Jump") && IsGrounded()) {
			rb.velocity = Vector2.up * jumpHeight;
		}

    }

	bool IsGrounded() {
		float rayDist = 1.0f;
		Vector2 origin = rb.position;
		Vector2 dir = Vector2.down;

		// Cast a ray to check if the player is on ground
		RaycastHit2D hit = Physics2D.Raycast(origin, dir, rayDist);

		return hit;
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.transform.tag == "Ship") {
			transform.parent = col.transform;
		}
	}

	void OnCollisionExit2D(Collision2D col) {
		if (col.transform.tag == "Ship") {
			transform.parent = null;
		}
	}
}
