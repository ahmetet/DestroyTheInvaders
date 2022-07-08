using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAreaHeroMagnetScript : MonoBehaviour
{
    private SpawnAreaProperties spawnAreaProperties;
    private Outline outline;

    public GameObject Orc2, Orc3,Orc4, Knight2, Knight3;

    private bool mergeTrigger = false;
    private void OnTriggerEnter(Collider other)
    {
       
        GameObject targetHero = other.gameObject;
        if (!spawnAreaProperties.isFull)
        {
            spawnAreaProperties.heroGameObject = targetHero;
            targetHero.transform.SetParent(this.transform);
            outline.OutlineColor = Color.green;
            outline.enabled = true;
        }
        else
        {
            if (targetHero!=spawnAreaProperties.heroGameObject&& targetHero.GetComponent<HeroScript>().heroType == spawnAreaProperties.heroGameObject.GetComponent<HeroScript>().heroType)
            {
                if(targetHero.GetComponent<HeroScript>().heroType==SpawnAreaProperties.Hero.Orc4 || targetHero.GetComponent<HeroScript>().heroType == SpawnAreaProperties.Hero.Knight4)
                {
                    //Final merge
                }
                else
                {
                    targetHero.transform.SetParent(this.transform);
                    mergeTrigger = true;
                    StartCoroutine(Merge(targetHero));
                }
            }
            else
            {
                outline.enabled = false;
            }
               

        } 
        
    }

    public IEnumerator Merge(GameObject _target)
    {
        SpawnAreaProperties.Hero _hero = _target.GetComponent<HeroScript>().heroType;
        yield return new WaitForSeconds(0.5f);
        mergeTrigger = false;
        Destroy(spawnAreaProperties.heroGameObject);
        Destroy(_target);
        if (_hero == SpawnAreaProperties.Hero.Orc1)
        {
            spawnAreaProperties.isFull = true;
            GameObject spawnedHero = GameObject.Instantiate(Orc2, this.transform);
            spawnedHero.transform.SetParent(this.transform); 
            spawnedHero.transform.localPosition = Vector3.zero;
            spawnAreaProperties.heroGameObject = spawnedHero;
        }
        else if (_hero == SpawnAreaProperties.Hero.Orc2)
        {
            spawnAreaProperties.isFull = true;
            GameObject spawnedHero = GameObject.Instantiate(Orc3, this.transform);
            spawnedHero.transform.SetParent(this.transform); 
            spawnedHero.transform.localPosition = Vector3.zero;
            spawnAreaProperties.heroGameObject = spawnedHero;

        }
        else if (_hero == SpawnAreaProperties.Hero.Orc3)
        {
            spawnAreaProperties.isFull = true;
            GameObject spawnedHero = GameObject.Instantiate(Orc4, this.transform);
            spawnedHero.transform.SetParent(this.transform);
            spawnedHero.transform.localPosition = Vector3.zero;
            spawnAreaProperties.heroGameObject = spawnedHero;

        }
        else if (_hero == SpawnAreaProperties.Hero.Knight1)
        {

        }
        else if (_hero == SpawnAreaProperties.Hero.Knight2)
        {

        }
         

        
       
        
    }
    public void TurnOffLights()
    {

        outline.enabled = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (!mergeTrigger)
        {
  
            Invoke("TurnOffLights", 0.5f);
        }
        
        
    }

    
    private void OnTriggerExit(Collider other)
    {
        if (!mergeTrigger)
        {
            GameObject targetHero = other.gameObject;
            Invoke("TurnOffLights", 0.5f);
            if (targetHero == spawnAreaProperties.heroGameObject)
            {
                spawnAreaProperties.heroGameObject = null;
            }
        } 
        

    }

    void Start()
    {
        outline = this.GetComponent<Outline>();
        spawnAreaProperties = this.GetComponent<SpawnAreaProperties>();       
    }

   
    
}
