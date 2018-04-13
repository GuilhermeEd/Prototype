using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Gun : MonoBehaviour {

	[SerializeField] GameObject smallProjectile;
	[SerializeField] GameObject mediumProjectile;
	[SerializeField] GameObject bigProjectile;
	[SerializeField] float chargeSpeed = 3f;
	[SerializeField] float chargeTime = 0f;

	void Update () {
		if (CrossPlatformInputManager.GetButtonDown("Fire1")) {
			Shot();
		}
		if (CrossPlatformInputManager.GetButton("Fire1")) {
			ChargeShot();
		}
		if (CrossPlatformInputManager.GetButtonUp("Fire1")) {
			ReleaseShot();
		}
	}

	void Shot() {
		Instantiate(smallProjectile, transform.position, Quaternion.identity);
		chargeTime = 0f;
	}

	void ChargeShot() {
		chargeTime += Time.deltaTime * chargeSpeed;
	}

	void ReleaseShot() {
		if (chargeTime >= 3f) {
			Instantiate(bigProjectile, transform.position, Quaternion.identity);
		} else if (chargeTime >= 1.5f) {
			Instantiate(mediumProjectile, transform.position, Quaternion.identity);
		}
		chargeTime = 0f;
	}
}
