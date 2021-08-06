using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]

public class Movement : MonoBehaviour
{

    private Animator animator;

    private new Rigidbody rigidbody;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private ControllerManager controllerManager;
    private Vector3 animationDirection;


    void Start()
    {
        controllerManager.OnMove += Move;
        controllerManager.OnAtack += Block;
        controllerManager.OnSpellThrow += Block;
        controllerManager.OnAtackOrThrowEnd += UnBlock;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }


    void Move(object sender, ControllerManager.OnMoveEventArgs e)
    {
        rigidbody.velocity = e.input * speed;
        
        animationDirection = transform.forward * e.input.z + transform.right * -e.input.x;
        animator.SetFloat(AnimationNames.playerForward, animationDirection.z);
        animator.SetFloat(AnimationNames.playerRight, animationDirection.x);
    }

    void Block(object sender, EventArgs e)
    {
        controllerManager.OnMove -= Move;
        rigidbody.velocity = Vector3.zero;
        animationDirection = Vector3.zero;
        animator.SetFloat(AnimationNames.playerForward, animationDirection.z);
        animator.SetFloat(AnimationNames.playerRight, animationDirection.x);
    }

    void UnBlock(object sender, EventArgs e)
    {
        controllerManager.OnMove += Move;
    }


}
