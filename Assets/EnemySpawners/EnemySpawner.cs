using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

  [SerializeField] GameObject enemy;
  [SerializeField] float spawnInterval = 3f;
  [SerializeField] float speedToSpawnLocation = 0.2f;
  [SerializeField] Transform spawnLocation;

  Vector3 velocity;

  void Start()
  {
    StartCoroutine(SpawnCorroutine());
  }

  IEnumerator SpawnCorroutine()
  {
    while (true)
    {
      yield return new WaitForSeconds(spawnInterval);
      GameObject enemyInstance = Instantiate(enemy, transform.position, Quaternion.identity);
      enemyInstance.transform.parent = transform;
      while (Vector3.Distance(enemyInstance.transform.position, spawnLocation.position) > 0.1f)
      {
        enemyInstance.transform.position = Vector3.SmoothDamp(enemyInstance.transform.position, spawnLocation.position, ref velocity, speedToSpawnLocation);
        yield return new WaitForEndOfFrame();
      }
    }
  }

}
