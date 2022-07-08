using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroScript : MonoBehaviour
{
    public float Health = 1f;
    public Image healthBar;
    public SpawnAreaProperties.Hero heroType;
    public float myForce = 0f;
    public float myDefense = 0f;

    public bool isDead = false;
    private void Start()
    {
        if (heroType == SpawnAreaProperties.Hero.Knight1)
        {
            myForce = 0.07f;
            myDefense = 0f;
        }
        else if (heroType == SpawnAreaProperties.Hero.Knight2)
        {
            myForce = 0.18f;
            myDefense = 0.05f;
        }
        else if (heroType == SpawnAreaProperties.Hero.Knight3)
        {
            myForce = 0.27f;
            myDefense = 0.1f;
        }
        else if (heroType == SpawnAreaProperties.Hero.Knight4)
        {
            myForce = 0.36f;
            myDefense = 0.15f;
        }
        else if(heroType == SpawnAreaProperties.Hero.Orc1)
        {
            myForce = 0.07f;
            myDefense = 0f;
        }
        else if (heroType == SpawnAreaProperties.Hero.Orc2)
        {
            myForce = 0.18f;
            myDefense = 0.05f;
        }
        else if (heroType == SpawnAreaProperties.Hero.Orc3)
        {
            myForce = 0.27f;
            myDefense = 0.1f;
        }
        else if (heroType == SpawnAreaProperties.Hero.Orc4)
        {
            myForce = 0.36f;
            myDefense = 0.15f;
        }
    }
    void Update()
    {

        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, Health, Time.deltaTime*2f);
        if (healthBar.fillAmount <= 0f)
        {
            healthBar.fillAmount = 0f;
            isDead = true;
            this.gameObject.tag = "Dead"; 
        }

        if (isDead)
        {
            this.gameObject.GetComponent<Animator>().SetBool("isWalking", false);
            this.gameObject.GetComponent<Animator>().SetBool("isAttacking", false);
            this.gameObject.GetComponent<Animator>().SetBool("isIdle", false);
            this.gameObject.GetComponent<Animator>().SetBool("isDead", true);

            Destroy(this.gameObject.GetComponent<MoveObjectScript>());
            Destroy(this.gameObject.GetComponent<Outline>());
            Destroy(this.gameObject.GetComponent<Hero_AutoMove>());
        }
    }
    public void DecreaseHealth(float val)
    {
        Health = Health - ( val + myDefense);
        if (Health <= 0f)
        {
            Health = 0f;
            isDead = true;
            this.gameObject.tag = "Dead";
        }
    }
}
