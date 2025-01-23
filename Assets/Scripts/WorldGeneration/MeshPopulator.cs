using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MeshPopulator : MonoBehaviour
{
    ChunkGeneration chunkGeneration;
    public int size;
    public int cellSize;
    public float stoneNoiseScale;
    public float stoneDensity;
    public float islandNoiseScale;
    public float islandDensity;
    public GameObject[] stones;
    public GameObject[] islands;

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        // chunkGeneration = transform.parent.gameObject.GetComponent<ChunkGeneration>();
        chunkGeneration = FindObjectOfType<ChunkGeneration>();

        size = chunkGeneration.chunkRes;
        cellSize = chunkGeneration.cellSize;
        stoneNoiseScale = chunkGeneration.stoneNoiseScale;
        stoneDensity = chunkGeneration.stoneDensity;
        islandNoiseScale = chunkGeneration.islandNoiseScale;
        islandDensity = chunkGeneration.islandDensity;
        // PopulateRocks();
        PopulateIsland();
    }

    void PopulateRocks()
    {
        float[,] noiseMap = new float[size, size];
        RaycastHit hit;

        float xOffset = Random.Range(-10000f, 10000f), zOffset = Random.Range(-10000f, 10000f);
        for (int z = 0; z < size; z++)
        {
            for (int x = 0; x < size; x++)
            {
                float noise = Mathf.PerlinNoise(x * xOffset * stoneNoiseScale, z * zOffset * stoneNoiseScale);
                noiseMap[x, z] = noise;
            }
        }

        for (int z = 0; z < size; z++)
        {
            for (int x = 0; x < size; x++)
            {
                if (Physics.Raycast(new Vector3(JigglePos(transform.localPosition.x + x), 5f, JigglePos(transform.localPosition.z + z)), -Vector3.up, out hit))
                {
                    if (hit.transform.gameObject.name.Contains("Terrain"))
                    {
                        float value = Random.Range(0, stoneDensity);
                        if (noiseMap[x, z] < value)
                        {
                            GameObject newObject = Instantiate(stones[Random.Range(0, stones.Length)], transform);
                            newObject.transform.position = new Vector3(JigglePos(transform.localPosition.x + x), Random.Range(0.3f, 0.5f), JigglePos(transform.localPosition.z + z));
                            newObject.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
                            newObject.transform.localScale = Vector3.one * Random.Range(.8f, 1.2f);
                        }
                    }
                }

            }
        }
    }

    void PopulateIsland()
    {
        float[,] noiseMap = new float[size, size];
        float xOffset = Random.Range(-10f, 10f), zOffset = Random.Range(-10f, 10f);
        for (int z = 0; z < size; z++)
        {
            for (int x = 0; x < size; x++)
            {
                float noise = Mathf.PerlinNoise(x * xOffset * islandNoiseScale, z * zOffset * islandNoiseScale);
                noiseMap[x, z] = noise;
            }
        }

        for (int z = 0; z < size; z++)
        {
            for (int x = 0; x < size; x++)
            {
                RaycastHit hit;
                if (Physics.Raycast(new Vector3(JigglePos(transform.localPosition.x + x), 5f, JigglePos(transform.localPosition.z + z)), -Vector3.up, out hit))
                {
                    if (hit.transform.gameObject.name.Contains("Terrain"))
                    {
                        Debug.Log("AQUI");
                        float value = Random.Range(0, islandDensity);
                        if (noiseMap[x, z] < value)
                        {
                            GameObject newObject = Instantiate(islands[Random.Range(0, islands.Length)], transform);
                            // newObject.transform.position = new Vector3(JigglePos(transform.localPosition.x + x), hit.point.y, JigglePos(transform.localPosition.z + z));
                            // newObject.transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal);
                            // newObject.transform.rotation = newObject.transform.rotation * Quaternion.Euler(0, Random.Range(0, 360f), 0);
                            // newObject.transform.localScale = Vector3.one * Random.Range(.8f, 1.2f);
                        }
                    }
                }
            }
        }
    }

    float JigglePos(float f)
    {
        f += Random.Range(-0.1f, 0.1f);
        return f;
    }
}