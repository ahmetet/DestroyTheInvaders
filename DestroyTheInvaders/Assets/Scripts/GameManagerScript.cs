using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{

    public SpawnAreaManager spawnAreaManager;
    public GameObject spawnAreaBorders;

    public Text resultText;
    public CanvasGroup canvasGroupResult;

    public int totalEnemy = 0;
    public int totalPlayer = 0;

    private bool isGameStarted = false;
    public void Game_Start()
    {
        StartCoroutine(GameStartPRO());
    }
    IEnumerator GameStartPRO()
    {
     
        spawnAreaBorders.SetActive(false);
        isGameStarted = true;
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject SpawnArea in spawnAreaManager.spawnAreas)
        {
            if (SpawnArea.GetComponent<SpawnAreaProperties>().isFull)
            {
                Destroy(SpawnArea.GetComponent<SpawnAreaProperties>().heroGameObject.GetComponent<MoveObjectScript>());
                SpawnArea.GetComponent<SpawnAreaProperties>().heroGameObject.AddComponent<Hero_AutoMove>().goToEnemy=true;
                SpawnArea.GetComponent<SpawnAreaProperties>().heroGameObject.GetComponent<Outline>().OutlineWidth = 2f;
                SpawnArea.GetComponent<SpawnAreaProperties>().heroGameObject.GetComponent<Outline>().enabled = true;
                totalPlayer += 1;
            }
            Destroy(SpawnArea.GetComponent<SpawnAreaProperties>());
            Destroy(SpawnArea.GetComponent<SpawnAreaHeroMagnetScript>());
            SpawnArea.GetComponent<MeshRenderer>().enabled = false;
            SpawnArea.GetComponent<BoxCollider>().enabled = false;
        }

        foreach (GameObject EnemyHero in spawnAreaManager.enemyHeros)
        {
            EnemyHero.AddComponent<Hero_AutoMove>().goToEnemy = false;
            totalEnemy += 1;

        }



    }
    public void ReLoad()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    private void Update()
    {
        if (isGameStarted)
        {


            if (totalEnemy <= 0 && totalPlayer > 0)
            {
                //Win
                resultText.text = "WIN";
                if (canvasGroupResult.alpha <= 0.9)
                {

                    canvasGroupResult.blocksRaycasts = true;
                    canvasGroupResult.interactable = true;
                    canvasGroupResult.alpha = Mathf.MoveTowards(canvasGroupResult.alpha, 0.9f, Time.deltaTime / 10f);
                }
            }
            else if (totalEnemy > 0 && totalPlayer <= 0)
            {
                //Lost
                resultText.text = "LOSE";
                if (canvasGroupResult.alpha <= 0.9)
                {

                    canvasGroupResult.blocksRaycasts = true;
                    canvasGroupResult.interactable = true;
                    canvasGroupResult.alpha = Mathf.MoveTowards(canvasGroupResult.alpha, 0.9f, Time.deltaTime / 10f);
                }
            }
            else if (totalPlayer <= 0 && totalEnemy <= 0)
            {
                resultText.text = "DRAWN";
                if (canvasGroupResult.alpha <= 0.9)
                {
                    canvasGroupResult.blocksRaycasts = true;
                    canvasGroupResult.interactable = true;
                    canvasGroupResult.alpha = Mathf.MoveTowards(canvasGroupResult.alpha, 0.9f, Time.deltaTime / 10f);
                }
            }

        }

    }
    public void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    
}
