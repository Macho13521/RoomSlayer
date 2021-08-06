using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class ControllerManager : MonoBehaviour
{

    public event EventHandler<OnSpacePressedEventArgs> OnSpacePressed;
    public class OnSpacePressedEventArgs : EventArgs
    {
        public Vector3 input;
    }

    public event EventHandler<OnMoveEventArgs> OnMove;
    public class OnMoveEventArgs : EventArgs
    {
        public Vector3 input;
    }

    public event EventHandler OnAtack;
    public event EventHandler OnAtackOrThrowEnd;

    public event EventHandler OnSpellThrow;

    public Action<float> cooldownCircleChanged;

    private Vector3 input;
    [SerializeField]
    private float movementAtackBlockLength = 0.8f;//in secounds
    [SerializeField]
    private float movementSpellBlockLength = 0.8f;//in secounds
    bool block = false;




    [SerializeField]
    private Atack atack;


    private float nextSpell = 1;

    public bool isDying = false;
    void FixedUpdate()
    {
        if (!isDying)
        {
            Dash();
            Move();
        }
    }

    private void Update()
    {
        if (!isDying)
        {
            Atack();
        }
    }

    void Dash()
    {
        if (Input.GetKey("space"))
        {
            if (OnSpacePressed != null)
            {
                OnSpacePressed(this, new OnSpacePressedEventArgs { input = input });
                Tutorial.FirstDash();
            }
        }
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if(input != new Vector3(x, 0, z))
        {
            Tutorial.FirstMove();
        }

        input = new Vector3(x, 0, z);
 
        if (OnMove != null)
        {
            OnMove(this, new OnMoveEventArgs { input = input });

        }

    }

    

    void Atack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (OnAtack != null && !block)
            {
                OnAtack(this, EventArgs.Empty);
                block = true;
                Invoke("UnBlockMoving", movementAtackBlockLength);
                Tutorial.FirstLPM();
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (atack.spellThrowItem != null)
            {
                if (Time.time > nextSpell)
                {

                        if (OnSpellThrow != null && !block)
                        {
                            OnSpellThrow(this, EventArgs.Empty);
                            block = true;
                            Invoke("UnBlockMoving", movementSpellBlockLength);
                            Tutorial.FirstPPMClick();
                        }

                        nextSpell = Time.time + atack.cooldown;
                        cooldownCircleChanged?.Invoke(atack.cooldown);
                }
                
            }
        }
    }



    void UnBlockMoving()
    {
        if (OnAtackOrThrowEnd != null)
            OnAtackOrThrowEnd(this, EventArgs.Empty);
        block = false;
    }

    
}
