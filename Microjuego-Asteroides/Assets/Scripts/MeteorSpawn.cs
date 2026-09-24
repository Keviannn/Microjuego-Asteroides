using UnityEngine;

public class MeteorSpawn : MonoBehaviour
{
    public GameObject meteorPrefab;
    public float spawnRatePerMinute = 30f;
    public float spawnRateIncrement = 1f;
    public float xLimit;
    private float maxTimeLife = 4f;
    private float spawnNext = 0;
    //Update is called once per frame

    void Update()
    {
        if(Time.time > spawnNext)
        {
            spawnNext = Time.time + 60/spawnRatePerMinute;

            spawnRatePerMinute += spawnRateIncrement;

            float rand = Random.Range(-xLimit, xLimit);
            Vector3 spawnPosition = new Vector3(rand , 8f, -1f);

            GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);

            Destroy(meteor, maxTimeLife);
        }    
    }
}
