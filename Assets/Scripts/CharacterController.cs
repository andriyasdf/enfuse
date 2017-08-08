using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    Rigidbody2D rb;
    public float speed = 3.0f;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate() {
        float moveX = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

		if (Input.GetButtonDown("Jump")) {
			rb.AddForce(Vector3.up * 5.0f);
			rb.gravityScale *= -1;
		}
    }
}
