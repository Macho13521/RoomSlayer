using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackOut : MonoBehaviour
{
    private Image image;
    [SerializeField]
    private float startTime = 1.0f; //in secounds
    private float time = 0;
    private bool timerRunning = false;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (timerRunning)
        {
            time -= Time.deltaTime;
            /*if (time >= (startTime / 2.0f))
            {
                float normalize = (time - (time / 2.0f)) / (startTime/2);
                image.color = new Color(0, 0, 0, 1-normalize);
            }
            else*/ if(time >= 0 )//&& time < (startTime / 2.0f))
            {
                float normalize = time  / (startTime / 2);
                image.color = new Color(0, 0, 0, normalize);
            }
            else
            {
                timerRunning = false;
            }
            
        }
            
        
    }

    public void FadeInFadeOut()
    {
        timerRunning = true;
        time = startTime;
    }



}
