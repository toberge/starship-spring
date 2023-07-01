using System;
using System.Linq;
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
    public float threshold;
    public EnemyLayout enemy;
}

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private EnemySpawn[] spawns;

    [SerializeField]
    private AnimationCurve spawnDelayCurve;

    private int minSpawn = 0;
    private int maxSpawn = 1;

    private float lastSpawnTime = -100;
    private float startTime;

    [SerializeField]
    private float initialSpawnDelay = 10;

    [SerializeField]
    private float difficultyScaleTime = 60;

    private float spawnsPerTick = 1;

    [SerializeField]
    private Ship player;

    private bool gameOver = false;

    private void Start()
    {
        startTime = Time.time;
        player.OnDeath += OnDeath;

        // Ignore collisions between enclosure walls and enemies
        Physics2D.IgnoreLayerCollision(10, 11);
    }

    private void OnDeath()
    {
        gameOver = true;
    }

    private void Spawn()
    {
        var enemy = spawns.Skip(minSpawn).Take(maxSpawn).ElementAt(Random.Range(0, maxSpawn - minSpawn)).enemy;

        Vector3 position = Direction.ANY.ToVector();

        if (Mathf.Abs(position.y) > 0)
        {
            position *= Arena.EnemySpawnHalfHeight + (enemy.Size.y + (enemy.Size.y - 1) * 3) / 2;
            position += Vector3.right * Random.Range(-Arena.EnemyHalfWidth, Arena.EnemyHalfWidth);
        }
        else
        {
            position *= Arena.EnemySpawnHalfWidth + (enemy.Size.x + (enemy.Size.x - 1) * 3) / 2;
            position += Vector3.up * Random.Range(-Arena.EnemyHalfHeight, Arena.EnemyHalfHeight);
        }

        Instantiate(enemy, position, Quaternion.identity, transform);
    }

    private void Update()
    {
        if (gameOver)
        {
            return;
        }

        while (maxSpawn < spawns.Length - 1 && Time.time - startTime > spawns[maxSpawn].threshold)
        {
            maxSpawn++;
        }

        if (Time.time - lastSpawnTime > initialSpawnDelay * spawnDelayCurve.Evaluate((Time.time - startTime) / difficultyScaleTime))
        {
            for (int i = 0; i < spawnsPerTick; i++)
            {
                Spawn();
            }
            lastSpawnTime = Time.time;
        }
    }
}
