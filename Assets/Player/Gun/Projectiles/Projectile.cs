using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour {

	[SerializeField] float speed = 5f;
	bool shotToRight;
	Rigidbody2D rb;


	void Start () {
		float xScale = GameObject.FindGameObjectWithTag("Player").transform.localScale.x;
		if (xScale > 0) {
			shotToRight = true;
		} else {
			shotToRight = false;
		}
		rb = GetComponent<Rigidbody2D>();

	}

	void Update () {
		if (shotToRight) {
			rb.velocity = new Vector2(speed, 0f);
		} else {
			rb.velocity = new Vector2(-speed, 0f);
		}
	}

	void OnBecameInvisible() {
		Destroy(gameObject);
	}
	
}
