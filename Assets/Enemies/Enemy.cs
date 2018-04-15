using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
  [SerializeField] float health = 10f;
  [SerializeField] float damage = 10f;
	[SerializeField] GameObject deathVFX;
  Animator anim;
	float dyingInterval = 1f;

  void Start()
  {
    anim = GetComponent<Animator>();
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      DealDamage(other.gameObject);
    }

    if (LayerMask.LayerToName(other.gameObject.layer) == "Player Attack")
    {
      print("Take Damage Enemy");
    }
  }

  void DealDamage(GameObject player)
  {
    player.GetComponent<PlayerHealth>().TakeDamage(damage);
  }

  public void TakeDamage(float amount)
  {
		health -= amount;
		if (health <= 0f) { StartDeathSequence(); }
  }

  void StartDeathSequence()
  {
		Instantiate(deathVFX, transform.position, Quaternion.identity);
		Destroy(gameObject);
  }

}
