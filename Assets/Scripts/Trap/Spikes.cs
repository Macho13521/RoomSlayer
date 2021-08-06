using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{

    private Vector3 downPos, upPos;
    [SerializeField]
    private float spikesStayDownDuration = 2.0f;
    [SerializeField]
    private float spikesStayUpDuration = 1.0f;
    [SerializeField]
    private float spikesMoveDuration = 0.2f;
    private float startTime;
    private bool canMove = true, canStay = false;
    private int goUpOrDown = 1;
    [SerializeField]
    private int hitDamage = 2;
    [SerializeField]
    private int stayDamage = 2;

    private static float enterTriggerTime;
    [SerializeField]
    private float stayDamageCooldown = 1;

    void Start()
    {
        upPos = transform.position;
        downPos = upPos - new Vector3(0, 0.5f, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SpikeUpDown(spikesMoveDuration, goUpOrDown);
    }

    void SpikeUpDown(float duration, int direction)//direction up = 1, down = -1;
    {
        if (canMove)
        {
            startTime = Time.time;
            canMove = false;
            canStay = true;
        }
        float normalizedDuration = (Time.time - startTime) / duration;
        if (normalizedDuration < 1.0f)
        {
            if (direction == -1)
                transform.position = Vector3.Lerp(upPos, downPos, normalizedDuration);
            else if (direction == 1)
                transform.position = Vector3.Lerp(downPos, upPos, normalizedDuration);
        }
        else
        {
            if (canStay)
            {
                canStay = false;
                if (direction == -1)
                    Invoke("Stay", spikesStayDownDuration);
                else if (direction == 1)
                    Invoke("Stay", spikesStayUpDuration);
            }
        }


    }

    private void Stay()
    {
        goUpOrDown *= -1;
        canMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Statistics playerStats = other.gameObject.GetComponent<Statistics>();
        if (playerStats != null)
        {
            playerStats.Hit(hitDamage, false);
            enterTriggerTime = Time.time;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Statistics playerStats = other.gameObject.GetComponent<Statistics>();
        if (playerStats != null)
        {
            if (Time.time - enterTriggerTime > stayDamageCooldown)
            {
                playerStats.Hit(stayDamage, false);
                enterTriggerTime = Time.time;
            }
        }
    }

}
