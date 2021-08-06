using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private ControllerManager controllerManager;
    private GameObject player;
    private Vector3 startPos;
    private RaycastHit hit;

    private bool block = false;

    void Start()
    {
        
        startPos = transform.position;
        player = controllerManager.gameObject;
        controllerManager.OnAtack += Block;
        controllerManager.OnSpellThrow += Block;
        controllerManager.OnAtackOrThrowEnd += UnBlock;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!block)
            PlayerLookAt();
    }

    void Update()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        transform.position = startPos + player.transform.position;
    }

    void PlayerLookAt()
    {
        if (!controllerManager.isDying)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
            {

                Vector3 pos = new Vector3(hit.point.x, player.transform.position.y, hit.point.z);
                if (Vector3.Distance(pos, player.transform.position) > 1.0f)
                    player.transform.LookAt(pos);
            }
        }
    }

    void Block(object sender, EventArgs e)
    {
        block = true;
    }

    void UnBlock(object sender, EventArgs e)
    {
        block = false;
    }
}
