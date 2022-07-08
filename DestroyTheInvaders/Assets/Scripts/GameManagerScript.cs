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
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject SpawnArea in spawnAreaManager.spawnAreas)
        {
            if (SpawnArea.GetComponent<SpawnAreaProperties>().isFull)
            {
                Destroy(SpawnArea.GetComponent<SpawnAreaProperties>().heroGameObject.GetComponent<MoveObjectScript>());
                SpawnArea.GetComponent<SpawnAreaProperties>().heroGameObject.AddComponent<Hero_AutoMove>().goToEnemy=true;
                SpawnArea.GetComponent<SpawnAreaProperties>().heroGameObject.GetComponent<Outline>().OutlineWidth = 2f;
                SpawnArea.GetComponent<SpawnAreaProperties>().heroGameObject.GetComponent<Outline>().enabled = true;
            }
            Destroy(SpawnArea.GetComponent<SpawnAreaProperties>());
            Destroy(SpawnArea.GetComponent<SpawnAreaHeroMagnetScript>());
            SpawnArea.GetComponent<MeshRenderer>().enabled = false;
            SpawnArea.GetComponent<BoxCollider>().enabled = false;
        }

        foreach (GameObject EnemyHero in spawnAreaManager.enemyHeros)
        {
            EnemyHero.AddComponent<Hero_AutoMove>().goToEnemy = false;

        }



    }
    public void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    
}
