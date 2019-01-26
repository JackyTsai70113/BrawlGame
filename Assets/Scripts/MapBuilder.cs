using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour {

    [SerializeField] GameObject start;
    [SerializeField] GameObject end;
    [SerializeField] GameObject wallPrefab, wall;
    private bool ifCreate = false; 

    // Use this for initialization
    void Start () {
        wall = null;



    }

    // Update is called once per frame
    void Update() {
        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            start.transform.position =GetWorldPos();
            ifCreate = true;
            wall = (GameObject)Instantiate(wallPrefab,
                start.transform.position,
                Quaternion.identity);
            /*
            Instantiate(bullet, 
                        Camera.main.ViewportToWorldPoint(Input.mousePosition),
                        Quaternion.identity);*/
        }
        else if (Input.GetMouseButtonUp(0))
        {
            end.transform.position = GetWorldPos();
            ifCreate = false;
        }
        else
            if (ifCreate)
            {
                end.transform.position = GetWorldPos();
                adjustWall();
                
            }
    }

    private void adjustWall()
    {
        start.transform.LookAt(end.transform);
        end.transform.LookAt(start.transform);
        float distance = Vector3.Distance(start.transform.position,
                    end.transform.position);
        wall.transform.position = start.transform.position + 
            distance / 2 * start.transform.forward;
        wall.transform.rotation = start.transform.rotation;
        wall.transform.localScale = new Vector3(wall.transform.localScale.x,
            wall.transform.localScale.y,
            distance);
    }

    private Vector3 GetWorldPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return new Vector3(0, 0, 0);
    }
}
