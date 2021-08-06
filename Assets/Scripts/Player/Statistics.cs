using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Statistics : MonoBehaviour
{
    public Action<float> changeHealth;
    public Action<float> changeMana;
    public Action<float> changeExp;
    public Action<int, int> changeLvl;

    public Action<int> changeMaxHealth;

    [SerializeField]
    private ControllerManager controllerManager;

    [SerializeField]
    private Dashing dashing;

    [SerializeField]
    public float health = 100;

    new Rigidbody rigidbody;

    [SerializeField]
    private Renderer armour;
    [SerializeField]
    private Renderer helmet;

    [SerializeField]
    private Transform atackPositionHit;

    [SerializeField]
    private new CapsuleCollider collider;
    private float startColliderRadius;
    private Vector3 startColliderPos;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    SkillTree skillTree;

    [SerializeField]
    float healByLvlProcent = 0.3f;
    [SerializeField]
    float manaRegenerationProcent = 0.03f;

    private float expPoints = 0;
    private int lvl = 0;

    public float mana { get; private set; } = 100;

    private void Start()
    {
        rigidbody = transform.GetComponent<Rigidbody>();
        controllerManager.OnAtack += Block;
        controllerManager.OnSpellThrow += Block;
        controllerManager.OnAtackOrThrowEnd += UnBlock;
        startColliderRadius = collider.radius;
        startColliderPos = collider.center;
        dashing.IsDashing = Dash;

        skillTree.gainHp = GainHp;
    }

    void GainHp(float extrahp)
    {
        health *= extrahp;
        changeHealth?.Invoke(health);
    }

    bool invincible = false;
    internal void Hit(float damagePoints, bool knockback)
    {
        Invoke("Invincible", 0.2f);
        SoundManager.Instance.PlaySound(SoundManager.Sound.PlayerHit);
        if (!invincible)
        {
            Color red = Color.red;
            armour.material.SetColor("_BaseColor", red);
            helmet.material.SetColor("_BaseColor", red);

            Damage(damagePoints);
            invincible = true;
        }
        if (knockback)
            KnockbackEnemys(transform.position);
    }

    internal void HealbyGreenEffect(float healProcent)
    {
        health += (SkillTree.playerMaxHp - health) * (healProcent);
        changeHealth?.Invoke(health);
    }

    void Dash()
    {
        if (!invincible)
        {
            mana -= 10;
            if (mana < 0)
                mana = 0;
            changeMana?.Invoke(mana);
        }

        invincible = true;
        Invoke("Invincible", 0.2f);
        
    }

    void Invincible()
    {
        invincible = false;
        Color white = Color.white;
        armour.material.SetColor("_BaseColor", white);
        helmet.material.SetColor("_BaseColor", white);
    }

    void Damage(float damagePoints)
    {
        health -= damagePoints;
        if (!controllerManager.isDying)
        {
            animator.SetTrigger(AnimationNames.playerImpact);
            changeHealth?.Invoke(health);
            if (health <= 0)
            {
                changeHealth?.Invoke(0);
                controllerManager.isDying = true;
                animator.SetTrigger(AnimationNames.playerDying);
                Invoke("Dying", 5f);
            }
        }
    }

    private void Dying()
    {
        GameManager.Instance.gameOver();
    }

    public void Exp(float exp)
    {
        expPoints += exp;
        if (expPoints >= 100)
        {
            int lvlbefor = lvl;
            int tmp = (int)expPoints / 100;
            lvl += tmp;
            for (int i = 0; i < lvl - lvlbefor; i++)
            {
                health += (SkillTree.playerMaxHp - health) * healByLvlProcent;
                changeHealth?.Invoke(health);
            }
            expPoints -= tmp * 100;
            changeLvl?.Invoke(lvlbefor, lvl);
            Tutorial.FirstLvl();
        }
        changeExp?.Invoke(expPoints);
    }

    public void ManaGain(float procent)
    {
        mana += (100 - mana)* ((1+ manaRegenerationProcent) * SkillTree.playerManaGain -1) * procent;
        changeMana?.Invoke(mana);

        
    }

    private void Block(object sender, EventArgs e)
    {
        collider.radius = 0.09f;
        collider.center = new Vector3(startColliderPos.x, startColliderPos.y, -0.25f);
    }

    private void UnBlock(object sender, EventArgs e)
    {
        collider.radius = startColliderRadius;
        collider.center = startColliderPos;
    }

    void KnockbackEnemys(Vector3 atackPos)
    {

        Collider[] colliders = Physics.OverlapSphere(atackPositionHit.position, 2f);

        foreach (Collider hit in colliders)
        {
            Enemy enemy = hit.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {

                enemy.Knockback(0.5f);
                enemy.GetRigibody().AddExplosionForce(80f, atackPos, 2f, -1);
            }
        }
    }

    public bool LoseMana(float points)
    {
        if (mana >= points)
        {
            mana -= points;
            changeMana?.Invoke(mana);

            return true;
        }
        return false;
    }

}
