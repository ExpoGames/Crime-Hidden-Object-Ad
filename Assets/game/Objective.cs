using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public ObjectCompleteEffect objectCompleteEffect;

    public void click()
    {
        objectCompleteEffect.StartCompleteEffect();
    }
}
