using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fly : Enemy
{
    private void Start()
    {
        player = GameManager.Instance.player;

        rigidbody = gameObject.GetComponent<Rigidbody>();

        health *= GameManager.Instance.GetFlyHpGain();

        damagePoints *= GameManager.Instance.GetFlyDmgGain();
    }

    void Update()
    {
        if(!frozen)
            MoveToPlayer();
    }

    protected override void Dying()
    {
        base.Dying();
        animator?.SetTrigger(AnimationNames.flyDying);
    }

    public override void Freeze(bool freeze, float freezTime)
    {
        base.Freeze(freeze, freezTime);
        animator?.SetBool(AnimationNames.flyFreeze, true);
    }

    protected override void UnFreeze()
    {
        base.UnFreeze();
        animator?.SetBool(AnimationNames.flyFreeze, false);
    }

    public override void Damage(float damagePoints)
    {
        base.Damage(damagePoints);
        SoundManager.Instance.PlaySound(SoundManager.Sound.FlyDamage);
    }


}
