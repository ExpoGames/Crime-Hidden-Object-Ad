using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Cysharp.Threading.Tasks;
using System;

public class videoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject canvas;
    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.Pause();
       
    }

    async void DeacyiveCanva()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(delay), false);
        canvas.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            DeacyiveCanva();
            videoPlayer.Play();
        }
    }
}
