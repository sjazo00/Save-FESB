using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    float randX;
    Vector2 whereToSpawn;
    public float spawnRate = 2f;
    float nextSpawn = 0.0f;
    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if ((Vector2.Distance(transform.position, target.position) < 8))
        {
            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnRate;
                randX = Random.Range(28.75f, 46.19f);
                whereToSpawn = new Vector2(randX, transform.position.y);
                Instantiate(enemy, whereToSpawn, Quaternion.identity);
            }
        }
    }
}
