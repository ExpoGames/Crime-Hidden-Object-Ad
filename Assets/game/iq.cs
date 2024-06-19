using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using System;
using System;
using DG.Tweening;
using UnityEngine.Video;

public class iq : MonoBehaviour
{
    public List<int> age;
    public int currentAge, ageIndex, prevageIndex;
    public TextMeshProUGUI agetext;
    bool completed = false;
    public GameObject video;
    bool increment;
    private async void UpdateAgeText()
    {

        while (!completed)
        {
            if (prevageIndex != ageIndex)
            {
                bool complate = false;
                int target = age[ageIndex] + currentAge;

                DOTween.To(() => currentAge, x => currentAge = x, target, 1.5f).OnUpdate(() =>
                {
                    agetext.text = " IQ : " + currentAge;
                }).
                OnStepComplete
                (() =>
                {
                    prevageIndex = ageIndex;
                    complate = true;
                });

                await UniTask.WaitUntil(() => complate == true);
            }
            else
            {
                if(increment)
                {
                    currentAge++;
                }
                else
                {
                    currentAge--;
                }
               
                if(currentAge < 30 )
                {
                    increment = true;
                }
                else if (currentAge > 40)
                {
                    increment = false;
                }
                agetext.text = " IQ : " + currentAge;
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f), true);
            }


        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            video.SetActive(true);
            UpdateAgeText();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            ageIndex++;
        }

    }
}
