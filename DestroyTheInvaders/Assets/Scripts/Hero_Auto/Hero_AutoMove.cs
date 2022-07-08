using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_AutoMove : MonoBehaviour
{

    private bool targetBoundsInteracted = false;
    public bool goToEnemy = false;
    private void Start()
    {
        InvokeRepeating("Attack", 2.0f, 1f);
    }

    public Transform FindTarget()
    {
        GameObject[] candidates;
        if (goToEnemy)
        {
            candidates = GameObject.FindGameObjectsWithTag("Enemy");
        }
        else
        {
            candidates = GameObject.FindGameObjectsWithTag("myHero");
        }
         
        float minDistance = Mathf.Infinity;
        Transform closest;

        if (candidates.Length == 0)
            return null;

        closest = candidates[0].transform;
        for (int i = 1; i < candidates.Length; ++i)
        {
            float distance = (candidates[i].transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                closest = candidates[i].transform;
                minDistance = distance;
            }
        }
        return closest;
    }

    private int HeroCount = 0;
    private void OnTriggerEnter(Collider other)
    {
        
        if (goToEnemy)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
               
                targetBoundsInteracted = true;
            }
        }
        else
        {
            if (other.gameObject.CompareTag("myHero"))
            {
                
                targetBoundsInteracted = true;
            }
        }
        
        
    }
    private void OnTriggerExit(Collider other)
    {
        
        if (goToEnemy)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
               
                targetBoundsInteracted = false;
            }
        }
        else
        {
            if (other.gameObject.CompareTag("myHero"))
            {
                
                targetBoundsInteracted = false;
            }
        }

    }


    Transform closestTransform;
    private bool isAttackFinished = false;
    void Update()
    {
        closestTransform = FindTarget();

        if (closestTransform != null)
        {
            _other = closestTransform.gameObject;
            if (!targetBoundsInteracted)
            {

                this.GetComponent<Animator>().SetBool("isWalking", true);
                this.GetComponent<Animator>().SetBool("isIdle", false);
                isAttackFinished = true;
                this.transform.LookAt(closestTransform, new Vector3(0f, 1f, 0f));
                this.transform.localRotation = Quaternion.Euler(0f, this.transform.localRotation.eulerAngles.y, this.transform.localRotation.eulerAngles.z);
                this.transform.position = Vector3.MoveTowards(this.transform.position, closestTransform.position + new Vector3(Random.Range(0.05f,0.3f), 0f, Random.Range(0.05f,0.1f)), 0.3f * Time.deltaTime);
            }
            else
            {
                this.GetComponent<Animator>().SetBool("isWalking", false);
                this.GetComponent<Animator>().SetBool("isAttacking", true);
                isAttackFinished = false;
                
            }
        }
        else
        {
            this.GetComponent<Animator>().SetBool("isWalking", false);
            this.GetComponent<Animator>().SetBool("isAttacking", false);
            this.GetComponent<Animator>().SetBool("isIdle", true);
            isAttackFinished = true;

        }

        
    }
    GameObject _other = null;

    public void Attack()
    {
        if (!isAttackFinished)
        {
            Handheld.Vibrate();
            _other.GetComponent<HeroScript>().DecreaseHealth(GetComponent<HeroScript>().myForce);
        }
        
    }
     

     
    
}
