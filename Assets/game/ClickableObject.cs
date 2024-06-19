using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Lean.Common;
using Lean.Touch;
using Cysharp.Threading.Tasks;
using System;

public class ClickableObject : MonoBehaviour 
{
    public bool isClicked, isclickable, isdragable, isanimation, isdraging, _isdragObjectAnimation;
    public GameObject ClickedPosition, initialPosition;
    public LeanConstrainToColliders leanConstrainToColliders;
    public LeanDragTranslate leanDragTranslate;
    public Animator anim, anim2;

    public void OnClick(Vector3 pos)
    {
        if (!isClicked)
        {
            if (isclickable)
            {
                isClicked = true;
                LeanTween.move(gameObject, ClickedPosition.transform.position, 0.3f);
                LeanTween.rotate(gameObject, ClickedPosition.transform.eulerAngles, 0.3f);
            }

            if(isanimation)
            {
                anim.SetTrigger("CLICK");
            }

            if(isdragable)
            {
                OnStartDrag(pos);
            }
        }
        else if (isClicked)
        {
            if (isclickable)
            {
                isClicked = false;
                LeanTween.move(gameObject, initialPosition.transform.position, 0.3f);
                LeanTween.rotate(gameObject, initialPosition.transform.eulerAngles, 0.3f);
            }
        }
    }

    public void OnStartDrag(Vector3 pos)
    {
        if(isdragable)
        {
            LeanTween.move(gameObject, pos, 0.1f).setOnComplete(() =>
            {
                 leanDragTranslate.enabled = true;
                 leanConstrainToColliders.enabled = true;
                 isdraging = true;
            });
        }
    }

    public async  void OnEndDrag()
    {
        if (isdragable)
        {
            leanDragTranslate.enabled = false;
            leanConstrainToColliders.enabled = false;

            if (_isdragObjectAnimation)
            {
                anim.enabled = true;
                anim2.enabled = true;
                await UniTask.Delay(TimeSpan.FromSeconds(2.0f), false);
                _isdragObjectAnimation = false;
                anim.enabled = false;
            }

            LeanTween.move(gameObject, initialPosition.transform.position, 0.3f);
            isdraging = false;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            if(isdraging)
            {
                _isdragObjectAnimation = true;
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            if (isdraging)
            {
                OnEndDrag();
            }
        }
    }


}
