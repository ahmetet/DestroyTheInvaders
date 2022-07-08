using System;
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
    public GameManagerScript gameManagerScript;

    public bool isDead = false;
    private void Start()
    {
        gameManagerScript = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManagerScript>();
        if (this.gameObject.CompareTag("Enemy")){
            healthBar.color = Color.red;
        }
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
    private bool lockDeath = true;
    void Update()
    {

 

        healthBar.transform.LookAt(healthBar.transform.position + Camera.main.transform.rotation * Vector3.back, Camera.main.transform.rotation * Vector3.up);


        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, Health, Time.deltaTime*2f);
        if (healthBar.fillAmount <= 0f)
        {
            healthBar.fillAmount = 0f;
            isDead = true;
            this.gameObject.tag = "Dead"; 
        }

        if (isDead && lockDeath)
        {
            lockDeath = false;
            int _playerCoin = Convert.ToInt32(GameObject.FindGameObjectWithTag("Coin").GetComponent<Text>().text.Split(':')[1].Trim().ToString());

            if (!this.GetComponent<Hero_AutoMove>().goToEnemy)
            {
                gameManagerScript.totalEnemy -= 1;
                
                GameObject.FindGameObjectWithTag("Coin").GetComponent<Text>().text = "COIN: " + (_playerCoin + 1).ToString();
            }
            else
            {
                gameManagerScript.totalPlayer -= 1;
                GameObject.FindGameObjectWithTag("Coin").GetComponent<Text>().text = "COIN: " + (_playerCoin - 1).ToString();
            }
            gameManagerScript.CheckEnd();
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
