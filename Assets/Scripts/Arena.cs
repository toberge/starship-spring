using UnityEngine;

public class Arena
{
    public const float Width = 48;
    public const float EnemyHalfWidth = Width / 2 - 2;
    public const float EnemySpawnHalfWidth = Width / 2 + 1;
    public const float Height = 27;
    public const float EnemyHalfHeight = Height / 2 - 2;
    public const float EnemySpawnHalfHeight = Height / 2 + 1;

    public static bool IsInFiringRange(Vector3 position)
    {
        return (Mathf.Abs(position.x) < Arena.EnemyHalfWidth && Mathf.Abs(position.y) < Arena.EnemyHalfHeight);
    }
}