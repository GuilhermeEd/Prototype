using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
  [SerializeField] float smoothTime = 0.3f;
  [SerializeField] float stepDistance = 2f;
  [SerializeField] float stepDelay = 1f;
  Vector3 velocity = Vector3.zero;

  void Start()
  {
    StartCoroutine("MoveCoroutine");
  }

  IEnumerator MoveCoroutine()
  {
    while (true)
    {
      Transform target = GameObject.FindGameObjectWithTag("Player").transform;
      Vector3 heading = target.position - transform.position;
      Vector3 destination = transform.position + (heading / heading.magnitude) * stepDistance;
      if (heading.x > 0) { Flip("left"); } else { Flip("right"); }
      yield return new WaitForSeconds(stepDelay);
      while (Vector3.Distance(transform.position, destination) > 0.1f)
      {
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, smoothTime);
        yield return new WaitForEndOfFrame();
      }
    }


  }

  void Flip(string side = null) {
    if (side == null) {
      Vector3 newScale = transform.localScale;
      newScale.x *= -1;
      transform.localScale = newScale;
    } else if (side == "right") {
      Vector3 newScale = transform.localScale;
      newScale.x = Mathf.Abs(newScale.x);
      transform.localScale = newScale;
    } else if (side == "left") {
      Vector3 newScale = transform.localScale;
      newScale.x = -Mathf.Abs(newScale.x);
      transform.localScale = newScale;
    }
  }

}
