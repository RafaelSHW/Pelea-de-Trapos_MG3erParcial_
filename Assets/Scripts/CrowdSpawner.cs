using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrowdSpawner : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    public Transform[] spawnPoints;
    public Transform player;
    public Transform enemy;

    public float minTime = 3f;
    public float maxTime = 6f;
    public float throwForce = 12f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            ThrowFromRandomPoint();
        }
    }

    void ThrowFromRandomPoint()
    {
        if (spawnPoints.Length == 0 || objectPrefabs.Length == 0) return;

        Transform selectedSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Transform target = Random.value > 0.5f ? player : enemy;

        if (target == null) return;

        Vector3 targetPos = target.position;

        int randomIndex = Random.Range(0, objectPrefabs.Length);
        GameObject obj = Instantiate(objectPrefabs[randomIndex], selectedSpawn.position, Quaternion.identity);

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = (targetPos - selectedSpawn.position).normalized;
            rb.AddForce(direction * throwForce, ForceMode.Impulse);
        }
    }
}