using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyancy : MonoBehaviour {

	float buoyancy = 15;
	float viscosity = 0.9f;
	internal bool inWater = false;

	Rigidbody2D rb;
	Vector2[] vertices;
	Vector2 centroid;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		if (!inWater) return;

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
			Debug.LogWarning("Buoyancy game object has no attached/compatible colliders.");

			centroid = rb.position;
		}

		// Upward force at center of buoyancy
		rb.AddForceAtPosition(Vector2.up * rb.mass * buoyancy, centroid);
		// Water resistance
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
			if (transform.TransformPoint(vertex).y <= -1.25f) {
				centroid += vertex;
				uwVerts++;
			}
		}
		// Divide by number of underwater verts (or number of verts if zero)
		centroid /= uwVerts == 0 ? vertices.Length : uwVerts;

		// Convert to world space
		centroid = transform.TransformPoint(centroid);

		return centroid;
	}

	void OnDrawGizmosSelected() {
		if (!inWater) return;

		Gizmos.color = Color.red;
		Gizmos.DrawLine(centroid, centroid + (Vector2.up * rb.mass * buoyancy));
		Gizmos.DrawLine(rb.position, rb.position + (rb.velocity * -1 * viscosity));

		Gizmos.color = Color.yellow;
		Gizmos.DrawIcon(centroid, "Center of Buoyancy");

		Gizmos.color = Color.cyan;

		foreach (Vector2 v in vertices) {
			Vector2 vertex = transform.TransformPoint(v);

			Gizmos.DrawIcon(vertex, "Vertex");
		}
	}
}
