using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public LayerMask _layerMask, _dragLayerMask, _clickableObjectMask;
    public Vector3 hitPos;
    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _dragLayerMask))
            {
                hitPos = hit.point;
            }
        }


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
            {
                Debug.Log(hit.collider.gameObject.name);
                hit.collider.gameObject.GetComponent<ClickableObject>().OnClick(hitPos);
                hitPos = Vector3.zero;
                Debug.DrawLine(ray.origin, hit.point);
            }
            else
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, _clickableObjectMask))
                {
                    Debug.Log(hit.collider.gameObject.name);
                    hit.collider.gameObject.GetComponent<Objective>().click();
                    Debug.DrawLine(ray.origin, hit.point);
                }
            }
        }

      
    }
  
}
