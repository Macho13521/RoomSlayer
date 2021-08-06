using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Dashing : MonoBehaviour
{
    public Action IsDashing;

    private new Rigidbody rigidbody;

    [SerializeField]
    private float dashForce = 1000f;
    [SerializeField]
    private float dashRate = 1.0f;//in secounds
    [SerializeField]
    private float dashLength = 0.3f;//in secounds

    private Vector3 dashDir;
    private float nextDash;
    public GameObject particlesPrefab;

    [SerializeField]
    private ControllerManager controllerManager;

    [SerializeField]
    private Statistics playerStats;

    private GameObject player;

    [SerializeField]
    private GameObject playerMesh;

    [SerializeField]
    private GameObject swordPlace;

    private void Start()
    {
        controllerManager.OnSpacePressed += Dash;
        controllerManager.OnAtack += Block;
        controllerManager.OnSpellThrow += Block;
        controllerManager.OnAtackOrThrowEnd += UnBlock;
        player = controllerManager.gameObject;

        rigidbody = GetComponent<Rigidbody>();
    }

    private void Dash(object sender, ControllerManager.OnSpacePressedEventArgs e)
    {
        if (playerStats.mana >= 10)
        {
            if (Time.time > nextDash)
            {
                dashDir = e.input;
                if (dashDir != Vector3.zero)
                {
                    StartCoroutine(DashIE());
                    EmitParticles();
                    IsDashing?.Invoke();
                    nextDash = Time.time + dashRate;
                }
            }
        }
    }

    IEnumerator DashIE()
    {
        playerMesh.SetActive(false);
        swordPlace.SetActive(false);
        gameObject.layer = 7;
        transform.GetChild(0).gameObject.layer = 7;
        for (int i = 0; i < dashLength * 10; i++)
        {
            yield return new WaitForSeconds(0.02f);
            rigidbody.AddForce(dashDir * dashForce);
        }
        playerMesh.SetActive(true);
        swordPlace.SetActive(true);
        gameObject.layer = 8;
        transform.GetChild(0).gameObject.layer = 8;
    }

    private void EmitParticles()
    {
        GameObject particle = Instantiate(particlesPrefab, transform.position, transform.rotation);
    }

    private void Block(object sender, EventArgs e)
    {
        controllerManager.OnSpacePressed -= Dash;
       
    }

    private void UnBlock(object sender, EventArgs e)
    {
        controllerManager.OnSpacePressed += Dash;
        
    }
}
