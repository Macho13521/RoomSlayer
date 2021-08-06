using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeProjectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 10.0f;
    private GameObject player;
    [SerializeField]
    private float damage = 2;

    [SerializeField]
    private new Rigidbody rigidbody;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        Destroy(gameObject, 3f);
        damage *= GameManager.Instance.GetSlimeDmgGain();
        //transform.LookAt(player.transform);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Statistics playerStats = other.gameObject.GetComponent<Statistics>();
        if(playerStats != null)
        {
            playerStats.Hit(damage, false);
            Destroy(gameObject, 0.1f);
        }
        else
        {
            if (other.gameObject.GetComponent<Slime>() == null)
                Destroy(gameObject);
        }

        
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Statistics playerStats = collision.gameObject.GetComponent<Statistics>();
        if (playerStats != null)
        {
            playerStats.Hit(damage, false);
            Destroy(gameObject);
        }
        else
        {
            if (collision.gameObject.GetComponent<Slime>() == null)
                Destroy(gameObject);
        }



    }


}
