﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject LinesObj;
    private bool lineStarted = false;
    private List<GameObject> lineRenderers = new List<GameObject>();
    private int lastRenderer=0;
    private Material material;
    void Start()
    {
        material=new Material(Shader.Find("Sprites/Default"));
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(ray,out hit))
            {
                if (hit.collider.gameObject.name == "Plane"&&lineStarted==false)
                {
                    StartLine(hit);
                    lineStarted = true;
                }
                else if(hit.collider.gameObject.name == "Plane"&&lineStarted == true)
                {
                    lineRenderers[lastRenderer].GetComponent<LineRenderer>().SetPosition(1, hit.point);
                    lineStarted = false;
                    lastRenderer++;
                }
            }
        }
        if (lineStarted)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name == "Plane")
                {
                    lineRenderers[lastRenderer].GetComponent<LineRenderer>().SetPosition(1, hit.point);
                }
            }
            else
            {
                RemoveLine();
            }
        }
    }
    public void RemoveLine()
    {
        if (lineRenderers.Count>0)
        {
            int index = lineRenderers.Count - 1;
            GameObject temp = lineRenderers[index];
            lineRenderers.RemoveAt(index);
            Destroy(temp);
            lineStarted = false;
            lastRenderer--;
        }
    }
    private void StartLine(RaycastHit hit)
    {
        GameObject obj = new GameObject();
        obj.name = "Line" + (lastRenderer + 1).ToString();
        obj.transform.SetParent(LinesObj.transform);
        lineRenderers.Add(obj);
        lineRenderers[lastRenderer].AddComponent<LineRenderer>();
        LineRenderer lineRenderer = lineRenderers[lastRenderer].GetComponent<LineRenderer>();
        lineRenderer.material = material;
        lineRenderer.widthMultiplier = 0.3f;
        lineRenderer.positionCount = 2;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.SetPosition(0, hit.point);
    }
}
