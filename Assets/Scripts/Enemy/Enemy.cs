using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : MonoBehaviour
{
    protected GameObject player;
    [SerializeField]
    protected float damagePoints = 2;
    [SerializeField]
    public NavMeshAgent navMeshAgent;
    protected Rigidbody rigidbody;

    [SerializeField]
    protected float health = 20;

    [SerializeField]
    public List<Renderer> renders;

    [SerializeField]
    protected Animator animator;

    [SerializeField]
    protected float dying = 0.3f;
    protected bool isDying = false;

    [SerializeField]
    protected float expAmount = 1;

    protected bool frozen = false;

    protected bool greenEffect = false;

    private float greenEffectHealPoints = 0;

    [SerializeField]
    protected float manaRegProcent = 1f;

    [SerializeField]
    protected GameObject flames;

    protected void MoveToPlayer()
    {
        if(!frozen)
            if (navMeshAgent.enabled)
                if (!isDying)
                    navMeshAgent.destination = player.transform.position;
    }

    public void Knockback(float duration)
    {
        if (!isDying)
        {
            navMeshAgent.enabled = false;
            rigidbody.isKinematic = false;
            StartCoroutine(IKnockBack(duration));
        }
    }

    private IEnumerator IKnockBack(float duration)
    {
        yield return new WaitForSeconds(duration);
        rigidbody.isKinematic = true;
        navMeshAgent.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isDying)
        {
            Statistics playerStats = collision.gameObject.GetComponent<Statistics>();
            if (playerStats != null)
            {
                playerStats.Hit(damagePoints, true);
            }
        }
    }

    public virtual void Damage(float damagePoints)
    {
        health -= damagePoints;
        if (health <= 0)
        {
            Statistics playerStats = player.GetComponent<Statistics>();
            playerStats.Exp(expAmount);
            playerStats.ManaGain(manaRegProcent);
            if (greenEffect)
            {
                playerStats.HealbyGreenEffect(greenEffectHealPoints);
                greenEffect = false;
            }
            Dying();
        }
        if (!isDying)
        {
            foreach (Renderer r in renders)
            {
                r.material.SetColor("_BaseColor", Color.red);
            }

            Invoke("afterDamage", 0.2f);
        }
    }

    protected virtual void Dying()
    {
        isDying = true;
        Destroy(gameObject, dying);
    }

    public Rigidbody GetRigibody()
    {
        return rigidbody;
    }

    protected virtual void afterDamage()
    {
        
        foreach (Renderer r in renders)
        {
            if (!frozen)
                r.material.SetColor("_BaseColor", Color.white);
            else
                r.material.SetColor("_BaseColor", Color.cyan);

        }
    }

    public virtual void Freeze(bool freeze, float freezTime)
    {
        frozen = freeze;
        if(navMeshAgent.enabled)
            navMeshAgent.SetDestination(transform.position);
        foreach (Renderer r in renders)
        {
            r.material.SetColor("_BaseColor", Color.cyan);
        }
        Invoke("UnFreeze", freezTime);
    }

    protected virtual void UnFreeze()
    {
        frozen = false;
        foreach (Renderer r in renders)
        {
            r.material.SetColor("_BaseColor", Color.white);
        }
        
    }

    public virtual void GreenEffect(float healPoints)
    {
        greenEffect = true;
        foreach (Renderer r in renders)
        {
            r.material.SetColor("_BaseColor", Color.green);
        }
        greenEffectHealPoints = healPoints;
    }

    public void Flames(float time, float damageProcent)
    {
        flames.SetActive(true);
        StartCoroutine(FlamesOff(time, damageProcent));
    }

    private IEnumerator FlamesOff(float time, float damageProcent)
    {
        for (int i = 0; i < time; i++)
        {
            Damage(health * damageProcent);
            yield return new WaitForSeconds(i);
        }
        flames.SetActive(false);
    }


}
