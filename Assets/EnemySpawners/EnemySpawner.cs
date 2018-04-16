using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

  [SerializeField] GameObject enemy;
  [SerializeField] float spawnInterval = 3f;

  Vector3 velocity;

  void Start()
  {
    StartCoroutine(SpawnCorroutine());
  }

  IEnumerator SpawnCorroutine()
  {
    while (true)
    {
      GameObject enemyInstance = Instantiate(enemy, transform.position, Quaternion.identity);
      enemyInstance.transform.parent = transform;
    	yield return new WaitForSeconds(spawnInterval);
    }
  }

}
