using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpell : Spell
{
    [SerializeField]
    private float freezeTime = 2;

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
        float freezeTimeSum = freezeTime + 0.5f * SkillTree.redLvl;
        enemy.Freeze(true, freezeTimeSum);
    }


}
