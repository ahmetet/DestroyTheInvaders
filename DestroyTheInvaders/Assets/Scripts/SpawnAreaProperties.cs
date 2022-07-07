using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAreaProperties : MonoBehaviour
{
    public bool isFull = false;
    private bool isFullPrev = false;
    public int heroType = 0;

    [SerializeField] SpawnAreaManager spawnAreaManager;
    void Start()
    {
        if (!spawnAreaManager.spawnAreas.Contains(this.gameObject))
        {
            spawnAreaManager.spawnAreas.Add(this.gameObject);
        }
        if (!spawnAreaManager.emptySpawnAreas.Contains(this.gameObject))
        {
            spawnAreaManager.emptySpawnAreas.Add(this.gameObject);
        }
    }

    public void Add_SpawnAreaFull()
    {
        if (!spawnAreaManager.fullSpawnAreas.Contains(this.gameObject))
        {
            if (spawnAreaManager.emptySpawnAreas.Contains(this.gameObject))
            {
                spawnAreaManager.emptySpawnAreas.Remove(this.gameObject);
            }
            spawnAreaManager.fullSpawnAreas.Add(this.gameObject);
        }
    }
    public void Add_SpawnAreaEmpty()
    {
        if (!spawnAreaManager.emptySpawnAreas.Contains(this.gameObject))
        {
            if (spawnAreaManager.fullSpawnAreas.Contains(this.gameObject))
            {
                spawnAreaManager.fullSpawnAreas.Remove(this.gameObject);
            }
            spawnAreaManager.emptySpawnAreas.Add(this.gameObject);
        }
    }
    void Update()
    {
        if (isFullPrev != isFull)
        {
            if (isFull)
            {
                Add_SpawnAreaFull();
            }
            else
            {
                Add_SpawnAreaEmpty();
            }
            isFullPrev = isFull;
        }
    }
}
