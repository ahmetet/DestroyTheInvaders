using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{

    public SpawnAreaManager spawnAreaManager;
    public GameObject spawnAreaBorders;

    public BuyManager buyManager;
    public Text resultText;
    public CanvasGroup canvasGroupResult;

    public int totalEnemy = 0;
    public int totalPlayer = 0;

    private bool isGameStarted = false;
    
    public void Game_Start()
    {
        if (buyManager.playerCoin != 12)
        {
            StartCoroutine(GameStartPRO());
        }
        
        
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
           

        }



    }
    public void ReLoad()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void CheckEnd()
    {
        Invoke("_checkend", 2f);
    }
    public void _checkend()
    {
        if (isGameStarted)
        {




            if (totalEnemy <= 0 && totalPlayer > 0)
            {
                //Win

                canvasGroupResult.gameObject.SetActive(true);
                resultText.text = "WIN";


                canvasGroupResult.blocksRaycasts = true;
                canvasGroupResult.interactable = true;
                canvasGroupResult.alpha = 0.9f;

            }
            else if (totalEnemy > 0 && totalPlayer <= 0)
            {
                //Lost
                canvasGroupResult.gameObject.SetActive(true);
                resultText.text = "LOSE";


                canvasGroupResult.blocksRaycasts = true;
                canvasGroupResult.interactable = true;
                canvasGroupResult.alpha = 0.9f;

            }
            else if (totalPlayer <= 0 && totalEnemy <= 0)
            {
                canvasGroupResult.gameObject.SetActive(true);
                resultText.text = "DRAWN";

                canvasGroupResult.blocksRaycasts = true;
                canvasGroupResult.interactable = true;
                canvasGroupResult.alpha = 0.9f;

            }
        }
    }

    private void Update()
    {
        
        

    }
    public void HidePanel(GameObject panel)
    {
        if (buyManager.playerCoin != 12)
        {
            panel.SetActive(false);
        }
    }

    private void Start()
    {
        canvasGroupResult.blocksRaycasts = false;
        canvasGroupResult.interactable = false;
        canvasGroupResult.alpha = 0f;
        canvasGroupResult.gameObject.SetActive(false);
    }

}
