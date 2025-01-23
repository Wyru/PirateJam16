using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGeneration : MonoBehaviour
{
    public int numberOfChunks;
    public int chunkRes;
    public int cellSize = 1;
    public int chunkRenderRadius = 3;
    [Range(0, 1)]
    public float stoneNoiseScale = 0.5f;
    [Range(0, 1)]
    public float stoneDensity = 0.5f;
    [Range(0, 1)]
    public float islandNoiseScale = 0.5f;
    [Range(0, 1)]
    public float islandDensity = 0.5f;
    public GameObject terrainPrefab;
    public Transform playerTransform;
    public List<GameObject> terrainList;
    private Dictionary<int, bool> chunkVisibilityAt;

    private int lastPlayerChunkIndex = -1;
    private int currentPlayerChunkIndex;

    Vector3 randStart;
    Vector3 randEnd;
    Vector2 mousePosition;



    // Start is called before the first frame update
    void Start()
    {

        chunkVisibilityAt = new Dictionary<int, bool>();
        // playerTransform = FindObjectOfType<PlayerController>().transform;

        // playerTransform.position = new Vector3(playerTransform.position.x * Random.Range(1f, numberOfChunks) * ChunkWidth(), 5f, playerTransform.position.z * Random.Range(1f, numberOfChunks) * ChunkWidth());
        //playerTransform.position = new Vector3(10f, 10f, 10f);

        // pf = FindObjectOfType<newPfScript>();
        // cGrid = FindObjectOfType<newCellGrid>();

        StartCoroutine(CreateChunks());
    }

    void Update()
    {
        // mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPlayerChunkIndex = ChunkIndexFromPosition(playerTransform.position);
        // currentPlayerChunkIndex = ChunkIndexFromPosition(mousePosition);

        if (currentPlayerChunkIndex != lastPlayerChunkIndex)
        {
            HideShowChunks();
            lastPlayerChunkIndex = currentPlayerChunkIndex;
        }
        
    }

    public int ChunkIndexFromPosition(Vector3 pos)
    {
        int chunkXIndex = Mathf.FloorToInt(pos.x / ChunkWidth());
        int chunkZIndex = Mathf.FloorToInt(pos.z / ChunkWidth());

        return WorldUtils.LinearPosition(chunkXIndex, chunkZIndex, numberOfChunks);
    }


    public int ChunkWidth()
    {
        return chunkRes * cellSize;
    }

    public IEnumerator CreateChunks()
    {
        for (int x = 0; x < numberOfChunks; x++)
        {
            for (int z = 0; z < numberOfChunks; z++)
            {
                GameObject terrain = Instantiate(terrainPrefab, new Vector3(x * ChunkWidth(), 0f, z * ChunkWidth()), Quaternion.identity);
                terrain.name = "Terain" + new Vector2(x, z) * ChunkWidth();
                terrain.transform.parent = transform;
                terrainList.Add(terrain);
                UpdateChunkVisibilityTo(false, x, z);
            }
        }
        yield return null;
    }

    public void HideShowChunks()
    {

        int[] previousNeighbors = WorldUtils.TileNeighbors(lastPlayerChunkIndex, chunkRenderRadius, numberOfChunks);

        int[] currentNeighbors = WorldUtils.TileNeighbors(currentPlayerChunkIndex, chunkRenderRadius, numberOfChunks);

        foreach (int index in previousNeighbors.Except(currentNeighbors))
        {
            UpdateChunkVisibilityTo(false, index);
        }

        if (currentPlayerChunkIndex >= 0)
            UpdateChunkVisibilityTo(true, currentPlayerChunkIndex);

        foreach (int index in currentNeighbors)
        {
            UpdateChunkVisibilityTo(true, index);
        }
    }

    public bool isChunkVisible(int index)
    {
        return chunkVisibilityAt[index];
    }

    public void UpdateChunkVisibilityTo(bool active, int x, int z)
    {
        int chunkIndex = WorldUtils.LinearPosition(x, z, numberOfChunks);
        this.UpdateChunkVisibilityTo(active, chunkIndex);
    }

    public void UpdateChunkVisibilityTo(bool active, int index)
    {
        terrainList[index].SetActive(active);
        chunkVisibilityAt[index] = active;
    }
}
