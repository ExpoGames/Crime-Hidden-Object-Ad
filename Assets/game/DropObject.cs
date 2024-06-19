using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObject : MonoBehaviour
{
    public List<Rigidbody> item = new List<Rigidbody>();
    public int index;


    public void EnableRigidBody()
    {
        if(index < item.Count)
        {
            item[index].gameObject.transform.SetParent(null);
            item[index].GetComponent<BoxCollider>().enabled = true;
            item[index].isKinematic = false;
            index++;
        }
      
    }
}
