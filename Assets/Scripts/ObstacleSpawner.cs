using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnRate = 1.2f;
    public float obstacleSpeed = 40f;
    public Vector2 spawnRangeX = new Vector2(-25, 25);
    public Vector2 spawnRangeY = new Vector2(-10, 10);

    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), 1f, spawnRate);
    }

    void SpawnObstacle()
    {
        if (obstaclePrefab == null) return;

        Vector3 spawnPos = new Vector3(
            transform.position.x + Random.Range(spawnRangeX.x, spawnRangeX.y),
            transform.position.y + Random.Range(spawnRangeY.x, spawnRangeY.y),
            transform.position.z
        );

        GameObject newObstacle = Instantiate(obstaclePrefab, spawnPos, Random.rotation);

        Obstacle script = newObstacle.GetComponent<Obstacle>();
        if (script != null)
        {
            script.moveSpeed = obstacleSpeed;
        }
    }
}