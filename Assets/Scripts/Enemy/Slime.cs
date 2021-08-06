using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slime : Enemy
{
    [SerializeField]
    private float shootDistance = 3f;
    [SerializeField]
    private GameObject shootPrefab;
    [SerializeField]
    private float shootRate = 2;
    private float nextShoot = 2;
    [SerializeField]
    private Transform projectilePos;

    void Start()
    {
        player = GameManager.Instance.player;

        rigidbody = gameObject.GetComponent<Rigidbody>();

        health *= GameManager.Instance.GetSlimeHpGain();

        damagePoints *= GameManager.Instance.GetSlimeDmgGain();

    }
    void Update()
    {
        if (!frozen)
        {
            if (!isDying)
            {
                if (Vector3.Distance(transform.position, player.transform.position) < shootDistance)
                {
                    ShootToPlayer(player.transform.position);
                    transform.LookAt(new Vector3(player.transform.position.x,
                        transform.position.y, player.transform.position.z));
                }
                else
                    MoveToPlayer();
            }
        }
    }

    private void ShootToPlayer(Vector3 shootPos)
    {
        
        if (Time.time > nextShoot)
        {
            Instantiate(shootPrefab, projectilePos.position, transform.rotation);
            SoundManager.Instance.PlaySound(SoundManager.Sound.SlimeThrow);
            nextShoot = Time.time + shootRate;
            
        }

    }

    protected override void Dying()
    {
        base.Dying();
        animator?.SetTrigger(AnimationNames.slimeDying);
    }

    public override void Freeze(bool freeze, float freezTime)
    {
        base.Freeze(freeze, freezTime);
        animator?.SetBool(AnimationNames.slimeFreeze, true);
    }

    protected override void UnFreeze()
    {
        frozen = false;
        foreach (Renderer r in renders)
        {
            r.material.SetColor("_BaseColor", new Color(0, 0.394f, 0.1757f));
        }
        animator?.SetBool(AnimationNames.slimeFreeze, false);
    }

    protected override void afterDamage()
    {
        //base.afterDamage();

        foreach (Renderer r in renders)
        {
            if (!frozen)
                r.material.SetColor("_BaseColor", new Color(0, 0.394f, 0.1757f));
            else
                r.material.SetColor("_BaseColor", Color.cyan);

        }
    }

}
