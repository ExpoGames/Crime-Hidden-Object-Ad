using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using TMPro;

public class ObjectCompleteEffect : MonoBehaviour
{
    public GameObject item, path, targetPos, tickmark;
    public float t1, t2, t3;
    public iq iq;

    public async void StartCompleteEffect()
    {
        if(item.GetComponent<Rigidbody>() != null)
        {
            item.GetComponent<Rigidbody>().isKinematic = true;
        }
        iq.ageIndex++;

        item.transform.SetParent(null);
        LeanTween.move(item, path.transform.position, t1);
        LeanTween.scale(item, path.transform.localScale, t1);
        LeanTween.rotate(item, path.transform.eulerAngles, t1);

        await UniTask.Delay(TimeSpan.FromSeconds(t1), false);

        LeanTween.move(item, targetPos.transform.position, t2);
        LeanTween.scale(item, targetPos.transform.localScale, t2);
        LeanTween.rotate(item, targetPos.transform.eulerAngles, t2);

        await UniTask.Delay(TimeSpan.FromSeconds(t2), false);
        tickmark.SetActive(true);
        LeanTween.scale(tickmark, Vector3.one, t3);
        LeanTween.scale(item, Vector3.zero, t3);

       await UniTask.Delay(TimeSpan.FromSeconds(t3), false);
       LeanTween.scale(tickmark, new Vector3(0.1f, 0.1f,0.1f), 0.5f).setEase(LeanTweenType.punch);
    }


}
