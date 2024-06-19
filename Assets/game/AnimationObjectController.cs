using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationObjectController : MonoBehaviour
{
    public Animator anim;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            anim.enabled = true;
        }
    }
}
