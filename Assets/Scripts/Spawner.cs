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

[Serializable]
public struct DifficultyLevel
{
    public float time;
    public int spawnDelay;
    public int spawns;
    public int spawnsPerSide;
}


public class Spawner : MonoBehaviour
{
    [SerializeField]
    private EnemySpawn[] spawns;

    [SerializeField]
    private DifficultyLevel[] levels;
    private DifficultyLevel level;
    private int nextLevelIndex = 1;

    private float lastSpawnTime = -100;
    private float startTime;

    private void Start()
    {
        level = levels[0];
        startTime = Time.time;

        // Ignore collisions between enclosure walls and enemies
        Physics2D.IgnoreLayerCollision(10, 11);
    }

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
            position += Vector3.up * Random.Range(-Arena.EnemyHalfHeight, Arena.EnemyHalfHeight);
        }

        Instantiate(spawn.enemy, position, Quaternion.identity, transform);
    }

    private void Update()
    {
        if (nextLevelIndex < levels.Length - 1 && Time.time - startTime > levels[nextLevelIndex].time)
        {
            level = levels[nextLevelIndex];
            nextLevelIndex++;
        }

        if (Time.time - lastSpawnTime > level.spawnDelay)
        {
            for (int i = 0; i < level.spawns; i++)
            {
                Spawn();
            }
            lastSpawnTime = Time.time;
        }
    }
}
