using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public float minXCoordinate = -3.0f;
    public float maxXCoordinate = 3.0f;
    public float yCoordinate = 10.0f;
    public float initialSpawnInterval = 2.0f;
    public float minSpawnInterval = 0.5f;
    public float intervalDecreaseRate = 0.05f;
    public float obstacleLifespan = 10.0f;
    public float initialSpawnDelay = 3.0f; 

    private float lastSpawnTime = 0.0f;
    private float currentSpawnInterval;

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        StartCoroutine(StartSpawningWithDelay()); 
    }

    IEnumerator StartSpawningWithDelay()
    {
        yield return new WaitForSeconds(initialSpawnDelay);
        
        
        while (true)
        {
            SpawnObstacle();
            yield return new WaitForSeconds(currentSpawnInterval);
        }
    }

    void SpawnObstacle()
    {
        int randomObstacleIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject obstaclePrefab = obstaclePrefabs[randomObstacleIndex];
        float randomXCoordinate = Random.Range(minXCoordinate, maxXCoordinate);
        Vector3 spawnPosition = new Vector3(randomXCoordinate, yCoordinate, 0f);
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        lastSpawnTime = Time.time;

        if (currentSpawnInterval > minSpawnInterval)
        {
            currentSpawnInterval -= intervalDecreaseRate;
        }

        Destroy(obstacle, obstacleLifespan);
    }
}
