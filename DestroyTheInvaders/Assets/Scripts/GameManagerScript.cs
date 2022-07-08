using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    public SpawnAreaManager spawnAreaManager;
    public GameObject spawnAreaBorders;
    public void Game_Start()
    {
        StartCoroutine(GameStartPRO());
    }
    IEnumerator GameStartPRO()
    {
        spawnAreaBorders.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        foreach (GameObject SpawnArea in spawnAreaManager.spawnAreas)
        {
            if (SpawnArea.GetComponent<SpawnAreaProperties>().isFull)
            {
                Destroy(SpawnArea.GetComponent<SpawnAreaProperties>().heroGameObject.GetComponent<MoveObjectScript>());
                SpawnArea.GetComponent<SpawnAreaProperties>().heroGameObject.AddComponent<Hero_AutoMove>();
                
            }
            Destroy(SpawnArea.GetComponent<SpawnAreaProperties>());
            Destroy(SpawnArea.GetComponent<SpawnAreaHeroMagnetScript>());
            SpawnArea.GetComponent<MeshRenderer>().enabled = false;
            SpawnArea.GetComponent<BoxCollider>().enabled = false;
        }
    }
    public void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    
}
