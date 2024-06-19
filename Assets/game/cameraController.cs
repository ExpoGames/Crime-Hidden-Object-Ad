using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public GameObject cam1, cam2;
    public Canvas Canvas;
    public Animator anim;

    public void StartCam1()
    {
        cam1.SetActive(false);
        cam2.SetActive(true);
    }

    public void Camera()
    {
        Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
    }

    public void Switch()
    {
        anim.enabled = true;
    }

    public void StopCam1()
    {
        Canvas.renderMode = RenderMode.ScreenSpaceCamera;
      //  gameObject.SetActive(false);
    }
}
