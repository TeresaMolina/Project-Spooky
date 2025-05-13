using UnityEngine;
using System.Collections;

public class ShadowSpawner : MonoBehaviour
{
    public GameObject shadowPrefab;
    public Transform player;
    public int maxShadows = 3;
    public float spawnInterval = 10f;
    public float spawnRadius = 7f;

    private bool shouldSpawn = false;
    private int currentShadows = 0;

    public void StartSpawning()
    {
        shouldSpawn = true;
        StartCoroutine(SpawnShadows());
    }

    public void StopSpawning()
    {
        shouldSpawn = false;
    }

    IEnumerator SpawnShadows()
    {
        while (shouldSpawn)
        {
            if (currentShadows < maxShadows)
            {
                Vector2 spawnPos = (Vector2)player.position + Random.insideUnitCircle.normalized * spawnRadius;
                Instantiate(shadowPrefab, spawnPos, Quaternion.identity);
                currentShadows++;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void ShadowDestroyed()
    {
        currentShadows--;
    }
}
