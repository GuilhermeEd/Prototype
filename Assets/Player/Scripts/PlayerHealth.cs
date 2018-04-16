using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

  [SerializeField] float health = 100f;
  [SerializeField] float maxHealth = 100f;
  [SerializeField] float damageColorTime = 0.3f;
  [SerializeField] float damagePushForce = 200f;
  [SerializeField] float invencibilityTime = 1.5f;

  float dyingInterval = 1f;
  bool isInencible = false;
  
  Animator anim;
  Rigidbody2D rb;
  SpriteRenderer sprite;

  void Start()
  {
    anim = GetComponent<Animator>();
    rb = GetComponent<Rigidbody2D>();
    sprite = GetComponent<SpriteRenderer>();
  }

  public void TakeDamage(float amount)
  {
    health -= amount;
    if (health <= 0f) StartDeathSequence();
    bool isFacingRight = GetComponent<PlayerMovement>().isFacingRight;
    if (isFacingRight) { rb.AddForce((Vector2.left + Vector2.up) * damagePushForce); }
    else { rb.AddForce((Vector2.right + Vector2.up) * damagePushForce); }
    StartCoroutine(DamageColorCoroutine());
  }

  IEnumerator DamageColorCoroutine() {
    sprite.color = Color.red;
    yield return new WaitForSeconds(damageColorTime);
    Color tmp = Color.white;
    tmp.a = 0.5f;
    sprite.color = tmp;
    yield return new WaitForSeconds(invencibilityTime - damageColorTime);
    sprite.color = Color.white;
    yield break;
  }

  public float GetHealth()
  {
    return health;
  }

  public void SetHealth(float value)
  {
    health = value;
    if (health > maxHealth) health = maxHealth;
    if (health <= 0f) StartDeathSequence();
  }

  public void IncreaseHealth(float amount)
  {
    health += amount;
    if (health > maxHealth) health = maxHealth;
  }

  public void ReduceHealth(float amount)
  {
    health -= amount;
    if (health <= 0f) StartDeathSequence();
  }

  void StartDeathSequence()
  {
    anim.SetBool("isDying", true);
		Invoke("Die", dyingInterval);
  }

	void Die()
	{
		Destroy(gameObject);
	}
}
