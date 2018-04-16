using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
  [SerializeField] float health = 10f;
  [SerializeField] float damage = 10f;
  [SerializeField] float damageColorTime = 0.2f;
  [SerializeField] GameObject deathVFX;

  SpriteRenderer sprite;
  AudioClip damageSFX;

  void Start()
  {
    sprite = GetComponent<SpriteRenderer>();
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      DealDamage(other.gameObject);
    }
    if (LayerMask.LayerToName(other.gameObject.layer) == "Player Attack")
    {

      PlayerAttack attack = other.gameObject.GetComponent<PlayerAttack>();
      AudioSource.PlayClipAtPoint(attack.damageSFX, transform.position);
			TakeDamage(attack.damage);
    }
  }

  void DealDamage(GameObject player)
  {
    player.GetComponent<PlayerHealth>().TakeDamage(damage);
  }

  public void TakeDamage(float amount)
  {
    health -= amount;
    sprite.color = Color.red;
    if (health <= 0f) { StartDeathSequence(); }
    Invoke("ResetSpriteColor", damageColorTime);
  }

  void ResetSpriteColor()
  {
    sprite.color = Color.white;
  }

  void StartDeathSequence()
  {
    Instantiate(deathVFX, transform.position, Quaternion.identity);
    Destroy(gameObject);
  }

}
