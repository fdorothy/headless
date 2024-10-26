using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : MonoBehaviour
{
    Vector3 target;
    Vector3 startPosition;
    float speed = 8f;
    public int trackIndex = 0;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        startPosition = transform.position;
        target = startPosition + Vector3.left * 10f;
        yield return new WaitForSeconds(2f);
        Game.singleton.SpawnBullet(transform.position + Vector3.left * 1.5f);
        yield return new WaitForSeconds(.5f);
        target = startPosition;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
    }
}
