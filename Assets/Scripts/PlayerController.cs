using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody2D rb;
    public float speed = 3.0f;
	public float jumpHeight = 2.0f;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate() {
        float moveX = Input.GetAxis("Horizontal");

		rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

		if (Input.GetButtonDown("Jump")) {
			rb.velocity = Vector2.up * jumpHeight;
		}

		
    }
}
