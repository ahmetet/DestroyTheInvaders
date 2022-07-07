using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAreaHeroMagnetScript : MonoBehaviour
{
    private SpawnAreaProperties spawnAreaProperties;
    private Outline outline;

    private void OnTriggerEnter(Collider other)
    {
       
        GameObject targetHero = other.gameObject;
        if (!spawnAreaProperties.isFull)
        {
            spawnAreaProperties.heroGameObject = targetHero;
            targetHero.transform.SetParent(this.transform); 
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        } 
        
    }

    public void TurnOffLights()
    {
        outline.enabled = false;
    }
    private void OnTriggerStay(Collider other)
    {
        Invoke("TurnOffLights", 0.5f);
    }

    private void OnTriggerExit(Collider other)
    {
     
        GameObject targetHero = other.gameObject; 
        outline.enabled = false;
        if (targetHero == spawnAreaProperties.heroGameObject)
        {
            spawnAreaProperties.heroGameObject = null;
        }

    }

    void Start()
    {
        outline = this.GetComponent<Outline>();
        spawnAreaProperties = this.GetComponent<SpawnAreaProperties>();       
    }

   
    
}
