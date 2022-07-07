using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyManager : MonoBehaviour
{
    public int playerCoin;
    public GameObject orcObject, knightObject;
    public int priceOrc = 1, priceKnight = 1;
    public Button buyOrcButton, buyKnightButton;
    public SpawnAreaManager spawnAreaManager;

    void Start()
    {
       
    }

    private void Update()
    {
        
        
        buyOrcButton.interactable = playerCoin >= priceOrc && spawnAreaManager.emptySpawnAreas.Count > 0;
        buyKnightButton.interactable = playerCoin >= priceKnight && spawnAreaManager.emptySpawnAreas.Count > 0;
        

    }

    public void Buy_Orc()
    {
        playerCoin -= 1;

        GameObject randomEmptySpawnArea = spawnAreaManager.emptySpawnAreas[Random.Range(0, spawnAreaManager.emptySpawnAreas.Count)];
        randomEmptySpawnArea.GetComponent<SpawnAreaProperties>().isFull = true;
        randomEmptySpawnArea.GetComponent<SpawnAreaProperties>().heroType = SpawnAreaProperties.Hero.Orc1;
        GameObject spawnedHero= GameObject.Instantiate(orcObject, randomEmptySpawnArea.transform);
        randomEmptySpawnArea.GetComponent<SpawnAreaProperties>().heroGameObject = spawnedHero;


    }
    public void Buy_Knight()
    {
        playerCoin -= 1;

        GameObject randomEmptySpawnArea = spawnAreaManager.emptySpawnAreas[Random.Range(0, spawnAreaManager.emptySpawnAreas.Count)];
        randomEmptySpawnArea.GetComponent<SpawnAreaProperties>().isFull = true;
        randomEmptySpawnArea.GetComponent<SpawnAreaProperties>().heroType = SpawnAreaProperties.Hero.Knight1;
        GameObject spawnedHero = GameObject.Instantiate(knightObject, randomEmptySpawnArea.transform);
        randomEmptySpawnArea.GetComponent<SpawnAreaProperties>().heroGameObject = spawnedHero;
    }
}
