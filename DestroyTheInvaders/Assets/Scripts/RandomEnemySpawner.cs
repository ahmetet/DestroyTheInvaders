using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawner : MonoBehaviour
{

    public List<GameObject> Heroes;
    public List<GameObject> EnemySpawnAreas;
    public SpawnAreaManager spawnAreaManager;
  
    public int RandomUnitCount = 0;
    void Start()
    {
        spawnAreaManager.enemyHeros = new List<GameObject>();
        RandomUnitCount = Random.Range(1, 7);
        int x = 0;
        while (x < RandomUnitCount)
        {
            int c = Random.Range(0, EnemySpawnAreas.Count);
            GameObject selectedArea = EnemySpawnAreas[c];
            EnemySpawnAreas.Remove(selectedArea);
            GameObject spawnedHero = GameObject.Instantiate(Heroes[Random.Range(0,Heroes.Count)], selectedArea.transform);
            Destroy(spawnedHero.GetComponent<MoveObjectScript>());
            spawnedHero.GetComponent<Outline>().OutlineColor = Color.red;
            spawnAreaManager.enemyHeros.Add(spawnedHero);
            spawnedHero.GetComponent<Outline>().OutlineWidth = 2f;
            spawnedHero.GetComponent<Outline>().enabled = true;
            spawnedHero.tag = "Enemy";
            spawnedHero.transform.localRotation = this.gameObject.transform.localRotation;
            x = x + 1;
        }

    }

    
    void Update()
    {
        
    }
}
