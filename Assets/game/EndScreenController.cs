using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EndScreenController : MonoBehaviour
{
    public List<GameObject> deactiveObject, activeObject;
    public Animator anim;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            EnableEndScreen();
        }

    }

    void EnableEndScreen()
    {
        for(int i =0; i< deactiveObject.Count;i++ )
        {
            deactiveObject[i].SetActive(false);
        }

        for (int i = 0; i < activeObject.Count; i++)
        {
            activeObject[i].SetActive(true);
        }

        anim.enabled = true;
    }
}
