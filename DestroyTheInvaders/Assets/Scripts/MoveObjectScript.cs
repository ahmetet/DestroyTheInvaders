using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectScript : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 targetPos;
    public Vector3 PlayerPos;
    public bool isMoving;
    const int MOUSE = 0;
    public bool isPermitted = true;
    private GameObject spawnArea;
 
  
    void Start()
    {

        targetPos = transform.position;
        isMoving = false;
    }

 
    void Update()
    {
         

        if (!isPermitted)
        {
            this.gameObject.transform.localPosition = Vector3.zero;
        }
        else
        {
            if (Input.GetMouseButton(MOUSE))
            {
                SetTarggetPosition();
            }
            if (isMoving)
            {
                MoveObject();
            }
            if (Input.GetMouseButtonUp(0) && !isMoving)
            {
                Invoke("ExecutePosition", 0.1f);
            }
        }
    }
    void SetTarggetPosition()
    {
        Plane plane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float point = 0f;

        if (plane.Raycast(ray, out point))
        {
            targetPos = ray.GetPoint(point);
            PlayerPos = this.transform.position;
        }

        if (targetPos.x <= PlayerPos.x + 0.5f && targetPos.x >= PlayerPos.x - 0.5f)
        {
            isMoving = true && isPermitted;
        }
        else
        {
            isMoving = false;
        }


    }
    void MoveObject()
    {
       // transform.LookAt(targetPos);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (transform.position == targetPos)
        {
            isMoving = false;
                  
        }
            
      

    }
    public void ExecutePosition()
    {
        spawnArea = this.transform.parent.gameObject;
        this.transform.localPosition = Vector3.zero;
        this.enabled = false;
        this.GetComponent<Outline>().enabled = false;
    }
    
   
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("SpawnAreaBorders"))
        {
            isPermitted = false;
            isMoving = false;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Invoke("givePermission", 1f);
    }
    public void givePermission()
    {
        isPermitted = true;
    }


}
