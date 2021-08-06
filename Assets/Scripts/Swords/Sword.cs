using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    [SerializeField]
    private float atackRadius = 0.8f;
    
    [SerializeField]
    private float knockbackRadius = 2f;
    [SerializeField]
    private float knockbackForce = 300;
    [SerializeField]
    private float knockbackDuration = 0.5f;
    [SerializeField]
    private float damagePoints = 5;


    public float slashMultiplier = 2.0f;
    public float atackLag = 0.3f;

    public void Slash(Vector3 atackPositionHit, Vector3 atackPos)
    {
        Knockback(atackPositionHit, atackPos);
    }

    void Knockback(Vector3 atackPositionHit, Vector3 atackPos)
    {

        Collider[] colliders = Physics.OverlapSphere(atackPositionHit, atackRadius);

        foreach (Collider hit in colliders)
        {
            Enemy enemy = hit.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (!hit.gameObject.GetComponent<FlyQueen>())
                    enemy.Knockback(knockbackDuration);
                enemy.Damage(damagePoints *  SkillTree.playerDamageGain);
                enemy.GetRigibody().AddExplosionForce(knockbackForce, atackPos, knockbackRadius, -1);
            }
        }
    }
}
