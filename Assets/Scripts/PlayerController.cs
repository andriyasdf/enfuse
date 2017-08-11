using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public float speed = 3.0f;
	public float jumpHeight = 2.0f;
	public LayerMask groundLayer;

	Rigidbody2D rb;

	void Start() {
        rb = GetComponent<Rigidbody2D>();

		if (isLocalPlayer) {
			transform.GetChild(0).gameObject.SetActive(true);
			GameObject.Find("Lobby Camera").SetActive(false);
		}
	}

	void Update() {
		if (!isLocalPlayer) return;

		if (Input.GetKeyDown(KeyCode.Backspace)) {
				GetComponent<Player>().TakeDamage(10);
		}

		if (Input.GetButtonDown("Jump") && IsGrounded()) {
			rb.velocity = Vector2.up * jumpHeight;
		}

		// Model flipping
		if (Input.GetKeyDown(KeyCode.A)) {
			GetComponent<SpriteRenderer>().flipX = true;
		} else if (Input.GetKeyDown(KeyCode.D)) {
			GetComponent<SpriteRenderer>().flipX = false;
		}
	}

    void FixedUpdate() {
		if (!isLocalPlayer) return;

        float move = Input.GetAxis("Horizontal");

		if (rb.velocity.x < speed) {
			rb.velocity = new Vector2(move * speed, rb.velocity.y);
		} else {
			rb.velocity = new Vector2(speed, rb.velocity.y);
		}

		if (rb.position.y < -100) {
			rb.position = GameObject.Find("Spawnpoint").transform.position;
		}
    }

	bool IsGrounded() {
		Collider2D col = GetComponent<BoxCollider2D>();

		return Physics2D.OverlapArea(col.bounds.min, col.bounds.max, groundLayer);
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
