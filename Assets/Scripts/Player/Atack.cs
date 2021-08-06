using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atack : MonoBehaviour
{
    [SerializeField]
    private float slashMultiplier = 2.0f;
    private Animator animator;

    [SerializeField]
    private ControllerManager controllerManager;

    //[SerializeField]
    //private SpellThrow spellThrow;

    [SerializeField]
    private GameObject swordPlace;
    private Sword sword;

    [SerializeField]
    private Transform atackPositionHit;
    private float atackLag = 0.3f;

    [SerializeField]
    public Statistics statistics;


    [SerializeField]
    public SpellThrowItem spellThrowItem;


    [SerializeField]
    Transform spellThrowPos;

    private GameObject swordGameObject;


    internal float cooldown;
    internal float manaCost;

    void Start()
    {
        controllerManager.OnAtack += Atacking;
        animator = GetComponent<Animator>();
        controllerManager.OnSpellThrow += Spelling;

        swordGameObject = swordPlace.transform.GetChild(0).gameObject;
        ChangeSwordStatistics();
    }

    private void Atacking(object sender, EventArgs e)
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.PlayerAtack);
        animator.SetTrigger(AnimationNames.playerSlash);
        animator.SetFloat(AnimationNames.playerSlashSpeed, slashMultiplier);
        Invoke("SwordSlash", atackLag);
    }

    private void SwordSlash()
    {
        Vector3 atackPos = new Vector3(transform.position.x, 0, transform.position.z);
        sword.Slash(atackPositionHit.position, atackPos);
    }

    public void CreateSword(GameObject SwordPrefab)
    {
        if (swordPlace.transform.GetChild(0) != null)
            Destroy(swordPlace.transform.GetChild(0).gameObject);
        swordGameObject = Instantiate(SwordPrefab, swordPlace.transform);

        ChangeSwordStatistics();
    }

    private void ChangeSwordStatistics()
    {

        sword = swordGameObject.GetComponent<Sword>();
        spellThrowItem = swordGameObject.GetComponent<SpellThrowItem>();
        
        this.slashMultiplier = sword.slashMultiplier;
        this.atackLag = sword.atackLag;
    }

    private void Spelling(object sender, EventArgs e)
    {
        
        this.manaCost = spellThrowItem.manaCost;
        if(statistics.LoseMana(spellThrowItem.manaCost))
        {
            this.cooldown = spellThrowItem.cooldown;
            spellThrowItem.ThrowSpell(spellThrowPos, statistics, controllerManager);
            animator?.SetTrigger(AnimationNames.playerThrowSpell);
        }
        else
        {
            this.cooldown = 0;
        }
        

    }


}
