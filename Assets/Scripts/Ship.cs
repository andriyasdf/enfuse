using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour {

	public int hull = 3000;

	// Use this for initialization
	void Start() {
		
	}
	
	// Update is called once per frame
	void Update() {
	}

	void HullDamage(int amount) {
		hull -= amount;
		GameObject.Find("Ship Health").GetComponent<Text>().text = hull.ToString();

		if (hull <= 0) {
			// Ship is dead
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.transform.tag == "Ground") {
			HullDamage(Mathf.FloorToInt(Random.Range(10, 22)));
		}
	}
}
