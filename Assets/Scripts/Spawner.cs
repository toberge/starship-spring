using System;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Direction
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    HORIZONTAL,
    VERTICAL,
    ANY,
}

public static class DirectionExtension
{
    private static readonly Direction[] AnyDirection = { Direction.UP, Direction.DOWN, Direction.LEFT, Direction.RIGHT };

    public static Vector3 ToVector(this Direction direction)
    {
        switch (direction)
        {
            case Direction.UP:
            case Direction.DOWN:
                return Vector3.up * (direction == Direction.UP ? 1 : -1);
            case Direction.VERTICAL:
                return Random.Range(0, 1) == 1 ? Vector3.up : Vector3.down;
            case Direction.LEFT:
            case Direction.RIGHT:
                return Vector3.right * (direction == Direction.RIGHT ? 1 : -1);
            case Direction.HORIZONTAL:
                return Random.Range(0, 1) == 1 ? Vector3.left : Vector3.right;
            case Direction.ANY:
                return AnyDirection[Random.Range(0, 3)].ToVector();
            default:
                return Vector3.up;
        }
    }
}

[Serializable]
public struct EnemySpawn
{
    public GameObject enemy;
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

        Vector3 position = spawn.side.ToVector();

        if (Mathf.Abs(position.y) > 0)
        {
            position *= Arena.EnemySpawnHalfHeight;
            position += Vector3.right * Random.Range(-Arena.EnemyHalfWidth, Arena.EnemyHalfWidth);
        }
        else
        {
            position *= Arena.EnemySpawnHalfWidth;
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
