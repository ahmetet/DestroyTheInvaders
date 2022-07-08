using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropManager : MonoBehaviour
{

    [SerializeField] SpawnAreaManager spawnAreaManager;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            GameObject heroObj;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                heroObj = hitInfo.collider.gameObject;
                if (heroObj.CompareTag("myHero"))
                {
                    heroObj.GetComponent<MoveObjectScript>().enabled = true;
                    heroObj.GetComponent<Outline>().enabled = true;
                }
            }

        }
    }
}
