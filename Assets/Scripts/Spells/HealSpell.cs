using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpell : Spell
{
    [SerializeField]
    private float healProcent = 0.01f;



    Statistics statistics;

    private void Awake()
    {
        statistics = GameManager.Instance.player.GetComponent<Statistics>();
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
        enemy.GreenEffect(healProcent + 0.005f * SkillTree.blueLvl);
    }


}
