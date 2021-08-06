using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownCircle : MonoBehaviour
{
    [SerializeField]
    private Image circleBackground;

    [SerializeField]
    private Image circle;

    [SerializeField]
    ControllerManager controller;


    void Start()
    {
        circle.fillAmount = 1;
        controller.cooldownCircleChanged = StartFill;
    }


    private void StartFill(float cooldown)
    {
        StartCoroutine(FillCircle(cooldown));
    }

    private IEnumerator FillCircle(float time)
    {
        circleBackground.enabled = true;
        circle.enabled = true;
        for (float i = 0; i < time; i+=0.05f)
        {
            yield return new WaitForSeconds(0.05f);
            circle.fillAmount = (i/time);
        }
        circle.fillAmount = 1;

        circleBackground.enabled = false;
        circle.enabled = false;
    }
}
