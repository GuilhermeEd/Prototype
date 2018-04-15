using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
	[SerializeField] float damage = 10f;

  void OnTriggerEnter2D(Collider2D other)
  {
		if (other.tag == "Player") {
			other.GetComponent<PlayerHealth>().TakeDamage(damage);
		}
  }

}
