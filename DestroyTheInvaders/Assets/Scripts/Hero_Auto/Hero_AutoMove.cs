using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_AutoMove : MonoBehaviour
{
    GameObject TEST;
    private bool targetBoundsInteracted = false;
 
    void Start()
    {
        TEST = GameObject.Find("TEST");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            targetBoundsInteracted = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            targetBoundsInteracted = false;
        }
        
    }

    void Update()
    {
        
        if (!targetBoundsInteracted)
        {
            this.GetComponent<Animator>().SetBool("isWalking", true);
            this.GetComponent<Animator>().SetBool("isIdle", false);
            this.transform.LookAt(TEST.transform, new Vector3(0f, 1f, 0f));
            this.transform.localRotation = Quaternion.Euler(0f, this.transform.localRotation.eulerAngles.y, this.transform.localRotation.eulerAngles.z);
            this.transform.position = Vector3.MoveTowards(this.transform.position, TEST.transform.position, Time.deltaTime*5);
        }

        else
        {
            this.GetComponent<Animator>().SetBool("isWalking", false);
            this.GetComponent<Animator>().SetBool("isIdle", true);
        }
    }
}
