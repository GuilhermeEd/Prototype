using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearWallCheck : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col) {
		if (col.collider.gameObject.layer == LayerMask.NameToLayer("Ground")){
			SendMessageUpwards("OnNearWallEnter");
		}
	}

	void OnCollisionExit2D(Collision2D col) {
		if (col.collider.gameObject.layer == LayerMask.NameToLayer("Ground")){
			SendMessageUpwards("OnNearWallExit");
		}
	}
}
