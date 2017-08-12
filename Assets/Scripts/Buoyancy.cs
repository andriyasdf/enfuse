using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyancy : MonoBehaviour {

	public float buoyancy = 15;
	public float viscosity = 0.9f;

	internal bool inWater = false;

	Rigidbody2D rb;

	void Start() {
		if (!GetComponent<Rigidbody2D>()) {
			gameObject.AddComponent<Rigidbody2D>();
		}

		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		if (!inWater) return;

		Vector2[] vertices;
		Vector2 centroid;

		// Find vertices based on collider type
		if (GetComponent<PolygonCollider2D>()) {
			centroid = GetBuoyancy(GetComponent<PolygonCollider2D>().points);
		} else if (GetComponent<BoxCollider2D>()) {
			vertices = new Vector2[4];
			BoxCollider2D col = GetComponent<BoxCollider2D>();

			vertices[0] = new Vector2(col.offset.x - (col.size / 2.0f).x, col.offset.y - (col.size / 2.0f).y); //bottom left
			vertices[1] = new Vector2(col.offset.x + (col.size / 2.0f).x, col.offset.y + (col.size / 2.0f).y); //top right
			vertices[2] = new Vector2(col.offset.x + (col.size / 2.0f).x, col.offset.y - (col.size / 2.0f).y); //bottom right
			vertices[3] = new Vector2(col.offset.x - (col.size / 2.0f).x, col.offset.y + (col.size / 2.0f).y); //top left
			
			centroid = GetBuoyancy(vertices);
		} else {
			Debug.LogWarning("Buoyancy game object has no compatible colliders.");

			centroid = rb.position;
		}

		rb.AddForceAtPosition(Vector2.up * rb.mass * buoyancy, centroid);
		rb.AddForce(rb.velocity * -1 * viscosity);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Water") {
			inWater = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Water") {
			inWater = false;
		}
	}

	/// <summary>
	/// Returns the center of buoyancy in world space.
	/// </summary>
	Vector2 GetBuoyancy(Vector2[] vertices) {
		Vector2 centroid = Vector2.zero;
		int uwVerts = 0;

		foreach (Vector2 vertex in vertices) {
			// Sum up underwater vertices
			if (transform.TransformPoint(vertex).y <= 0) {
				centroid += vertex;
				uwVerts++;
			}

			/*if (Physics2D.OverlapPoint(transform.TransformPoint(vertex), LayerMask.NameToLayer("Water")).gameObject.tag == "Water") {
				centroid += vertex;
				uwVerts++;
			}*/
		}
		// Divide by number of underwater verts (or number of verts if zero)
		centroid /= uwVerts == 0 ? vertices.Length : uwVerts;

		// Convert to world space
		centroid = transform.TransformPoint(centroid);

		return centroid;
	}
}
