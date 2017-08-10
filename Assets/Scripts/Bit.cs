using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bit : Pickup {

	public int value = 1;

	public override void PickupItem(GameObject ply) {
		base.PickupItem(ply);

		ply.GetComponent<Player>().AddBits(value);
	}
}
