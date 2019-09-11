using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject spawn;
    public float SpawnRadius;

    public int enemyAmount;

    void Start()
    {
        if (enemyAmount < 0) enemyAmount = 0;

        if (enemyAmount > 1)
        {
            for (int i = 0; i < enemyAmount; i++)
            {
                Instantiate(spawn, RandomRadius(transform.position, SpawnRadius), transform.rotation);
            }
        }
    }

    private Vector3 RandomRadius(Vector3 pos, float radius)
    {
        float angle = Random.value * 360;
        Vector3 position;
        position.x = pos.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        position.y = pos.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        position.z = pos.z;
        return position;
    }
}
