using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorGenerator : MonoBehaviour
{

    public GameObject prefabRight;
    public GameObject prefabLeft;
    private float spawnOffset = 0; // Z position of the spawned prefab
    private float prefabLength = 88.5f;
    private int numberOfPrefabs = 7;
    public Transform playerTransform;
    private List<GameObject> activePrefabs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfPrefabs; ++i)
        {
            SpawnPrefab();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - 95 > spawnOffset - numberOfPrefabs * prefabLength)
        {
            SpawnPrefab();
            DeleteTile();
        }
    }

    public void SpawnPrefab()
    {
        GameObject go = Instantiate(prefabRight, transform.forward * spawnOffset, transform.rotation);
        GameObject go2 = Instantiate(prefabLeft, transform.forward * spawnOffset, transform.rotation);
        activePrefabs.Add(go);
        activePrefabs.Add(go2);
        spawnOffset += prefabLength;
    }

    public void DeleteTile()
    {
        Destroy(activePrefabs[0]);
        Destroy(activePrefabs[1]);
        activePrefabs.RemoveAt(0);
        activePrefabs.RemoveAt(1);
    }
}
