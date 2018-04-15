using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

  [SerializeField] float health = 100f;
  [SerializeField] float maxHealth = 100f;
  Animator anim;
  float dyingInterval = 1f;

  void Start()
  {
    anim = GetComponent<Animator>();
  }

  public void TakeDamage(float amount)
  {
    health -= amount;
    if (health > maxHealth) health = maxHealth;
    if (health <= 0f) StartDeathSequence();
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
