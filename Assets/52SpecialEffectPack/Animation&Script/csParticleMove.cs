using UnityEngine;
using System.Collections;

public class csParticleMove : MonoBehaviour
{
    public float speed = 0.1f;
    public float size = 0.1f;
    public float spellRadius = 0.4f;
    public float damagePoints = 1f;
    public float knockbackForce = 200;

    Rigidbody rigidbody;

    private void Start()
    {
        for(int i=0; i< transform.childCount; i++)
        {
            transform.GetChild(i).localScale = new Vector3(size, size, size);
        }
        rigidbody = transform.GetComponent<Rigidbody>();
        Destroy(gameObject, 5.0f);
    }

    void FixedUpdate()
    {
        rigidbody.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, spellRadius);

        foreach (Collider hit in colliders)
        {
            Enemy enemy = hit.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Knockback(0.2f);
                enemy.Damage(damagePoints * SkillTree.playerDamageGain);
                enemy.GetRigibody().AddExplosionForce(knockbackForce, transform.position, 0.3f, -1);
            }
        }

        Destroy(gameObject, 2.3f);
    }
}
