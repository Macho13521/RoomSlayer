using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpell : Spell
{
    [SerializeField]
    private float flamesTime = 2;

    [SerializeField]
    private float damageProcent = 0.1f;

    private void Awake()
    {
        //skillTree = GameObject.Find("Skills").GetComponent<SkillTree>();
        //skillTree.changeRed = SpellUpgrade;
    }

    private void Start()
    {
        rigidbody = transform.GetComponent<Rigidbody>();
        Destroy(gameObject, 5.0f);
    }

    void FixedUpdate()
    {
        rigidbody.velocity = transform.forward * speed;
    }

    protected override void SpellEffect(Enemy enemy)
    {
        base.SpellEffect(enemy);
        float flamesTimeSum = flamesTime + SkillTree.blueLvl;
        float damageProcentSum = damageProcent + 0.05f * SkillTree.redLvl;
        enemy.Flames(flamesTimeSum, damageProcentSum);
    }


}
