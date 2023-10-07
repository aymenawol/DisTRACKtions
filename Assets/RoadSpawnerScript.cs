using UnityEngine;

public class RoadSpawnerScript : MonoBehaviour
{
    public GameObject RoadPrefab;
    public float spawnRate = 2;

    private float timer = 0;
    public float roadSpeed = 5; 

    void Start()
    {
        spawnRoad();
    }

    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnRoad();
            timer = 0;
        }
    }

    void spawnRoad()
    {
        GameObject newRoad = Instantiate(RoadPrefab, transform.position, transform.rotation);
        RoadCode roadCode = newRoad.GetComponent<RoadCode>();
        if (roadCode != null)
        {
            roadCode.moveSpeed = roadSpeed;
        }
    }
}
