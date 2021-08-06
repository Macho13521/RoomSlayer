using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{

    public float speed = 2.0f;
    public float spellRadius = 1.35f;
    //public float damagePoints = 1f;
    public float knockbackForce = 200;

    protected Rigidbody rigidbody;


    

    private void OnCollisionEnter(Collision collision)
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, spellRadius);

        foreach (Collider hit in colliders)
        {
            Enemy enemy = hit.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                SpellEffect(enemy);
                //enemy.Knockback(0.2f);
                //enemy.Damage(damagePoints * SkillTree.playerDamageGain);
                
            }
        }

        Destroy(gameObject, 5.75f);
    }

    protected virtual void SpellEffect(Enemy enemy)
    {
        enemy.GetRigibody().AddExplosionForce(knockbackForce, transform.position, 0.4f, -1);

    }

    private void OnTriggerEnter(Collider other)
    {


        /*Collider[] colliders = Physics.OverlapSphere(transform.position, spellRadius);

        foreach (Collider hit in colliders)
        {*/
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            SpellEffect(enemy);
            //enemy.Knockback(0.2f);
            //enemy.Damage(damagePoints * SkillTree.playerDamageGain);
            
        }
        else if(other.GetComponent<Statistics>() == null && 
            other.GetComponent<SlimeProjectile>() == null &&
            other.GetComponent<Spikes>() == null)
        {
            Destroy(gameObject, 0.1f);
        }
        //}

        Destroy(gameObject, 5.75f);
    }


}
