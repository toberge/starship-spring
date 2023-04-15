using System;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Direction
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
}

[Serializable]
public struct EnemySpawn
{
    public Enemy enemy;
    public Direction side;
}

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private EnemySpawn[] spawns;

    [SerializeField]
    private float spawnRate = 5;
    private float lastSpawnTime;

    private void Spawn()
    {
        var spawn = spawns[Random.Range(0, spawns.Length)];

        Vector3 position;
        if (spawn.side == Direction.UP || spawn.side == Direction.DOWN)
        {
            position = Vector3.up * Arena.EnemySpawnHalfHeight * (spawn.side == Direction.UP ? 1 : -1);
            position += Vector3.right * Random.Range(-Arena.EnemyHalfWidth, Arena.EnemyHalfWidth);
        }
        else
        {
            position = Vector3.right * Arena.EnemySpawnHalfWidth * (spawn.side == Direction.RIGHT ? 1 : -1);
            position += Vector3.up * Random.Range(-Arena.EnemyHalfWidth, Arena.EnemyHalfHeight);
        }

        Instantiate(spawn.enemy, position, Quaternion.identity, transform);
    }

    void Update()
    {
        if (Time.time - lastSpawnTime > spawnRate)
        {
            Spawn();
            lastSpawnTime = Time.time;
        }
    }
}
