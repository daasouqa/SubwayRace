using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] prefabs;
    private float spawnOffset = 0; // Z position of the spawned prefab
    private float prefabLength = 29.5f;
    private int initialNumberOfPrefabs = 3;
    private int numberOfPrefabs = 19; // (numberOfPrefabs + 2) must be multiple of 3
    public Transform playerTransform;
    private List<GameObject> activePrefabs = new List<GameObject>();

    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfPrefabs; ++i)
        {
            if (i == 0)
            {
                SpawnPrefab(0);
            }
            else
            {
                SpawnPrefab(Random.Range(1, prefabs.Length - 1));
            }
        }

        SpawnPrefab(prefabs.Length - 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - 35 > spawnOffset - numberOfPrefabs * prefabLength)
        {
            SpawnPrefab(0);
            DeleteTile();
        }
    }

    public void SpawnPrefab(int prefabIndex)
    {
        GameObject go = Instantiate(prefabs[prefabIndex], transform.forward * spawnOffset, transform.rotation);
        activePrefabs.Add(go);
        spawnOffset += prefabLength;
    }

    public void DeleteTile()
    {
        Destroy(activePrefabs[0]);
        activePrefabs.RemoveAt(0);
    }
}
