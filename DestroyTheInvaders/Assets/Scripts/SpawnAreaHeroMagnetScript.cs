using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAreaHeroMagnetScript : MonoBehaviour
{
    private SpawnAreaProperties spawnAreaProperties;
    private Outline outline;

    public GameObject Orc2, Orc3,Orc4, Knight2, Knight3,Knight4;

    private bool mergeTrigger = false;
    private GameObject _targetObj = null;
    private void OnTriggerEnter(Collider other)
    {
       
        GameObject targetHero = other.gameObject;
        if (!spawnAreaProperties.isFull)
        {
            Handheld.Vibrate();
            spawnAreaProperties.heroGameObject = targetHero;
            targetHero.transform.SetParent(this.transform);
            outline.OutlineColor = Color.green;
            outline.enabled = true;
        }
        else
        {
            try
            {
                if (targetHero != spawnAreaProperties.heroGameObject && targetHero.GetComponent<HeroScript>().heroType == spawnAreaProperties.heroGameObject.GetComponent<HeroScript>().heroType)
                {
                    if (targetHero.GetComponent<HeroScript>().heroType == SpawnAreaProperties.Hero.Orc4 || targetHero.GetComponent<HeroScript>().heroType == SpawnAreaProperties.Hero.Knight4)
                    {
                        //Final merge
                    }
                    else
                    {

                        mergeTrigger = true;
                        _targetObj = targetHero;
                        outline.enabled = true;

                    }
                }
                else
                {
                    outline.enabled = false;
                }
            }
            catch
            {

            }
               

        } 
        
    }

    public IEnumerator Merge(GameObject _target)
    {
        SpawnAreaProperties.Hero _hero = _target.GetComponent<HeroScript>().heroType;
        //yield return new WaitForSeconds(0.1f);
        _target.transform.SetParent(this.transform);
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
            spawnAreaProperties.isFull = true;
            GameObject spawnedHero = GameObject.Instantiate(Knight2, this.transform);
            spawnedHero.transform.SetParent(this.transform);
            spawnedHero.transform.localPosition = Vector3.zero;
            spawnAreaProperties.heroGameObject = spawnedHero;
        }
        else if (_hero == SpawnAreaProperties.Hero.Knight2)
        {
            spawnAreaProperties.isFull = true;
            GameObject spawnedHero = GameObject.Instantiate(Knight3, this.transform);
            spawnedHero.transform.SetParent(this.transform);
            spawnedHero.transform.localPosition = Vector3.zero;
            spawnAreaProperties.heroGameObject = spawnedHero;
        }
        else if (_hero == SpawnAreaProperties.Hero.Knight3)
        {
            spawnAreaProperties.isFull = true;
            GameObject spawnedHero = GameObject.Instantiate(Knight4, this.transform);
            spawnedHero.transform.SetParent(this.transform);
            spawnedHero.transform.localPosition = Vector3.zero;
            spawnAreaProperties.heroGameObject = spawnedHero;
        }
        Handheld.Vibrate();
        yield return null;




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
    private void Update()
    {
        if(mergeTrigger && Input.GetMouseButtonUp(0))
        {
            StartCoroutine(Merge(_targetObj));
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
        mergeTrigger = false;
        

    }

    void Start()
    {
        outline = this.GetComponent<Outline>();
        spawnAreaProperties = this.GetComponent<SpawnAreaProperties>();
        if (spawnAreaProperties.isEnemy)
        {
            Destroy(this);
        }
    }

   
    
}
