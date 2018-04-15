using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearWallCheck : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.layer == LayerMask.NameToLayer("Ground")){
			SendMessageUpwards("OnNearWallEnter");
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.layer == LayerMask.NameToLayer("Ground")){
			SendMessageUpwards("OnNearWallExit");
		}
	}
}
