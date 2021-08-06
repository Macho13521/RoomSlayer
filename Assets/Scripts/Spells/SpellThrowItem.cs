using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellThrowItem : MonoBehaviour
{
    public Spell spellProjectile;

    [SerializeField]
    public float cooldown = 2;
    private float nextSpell = 2;

    [SerializeField]
    public float manaCost = 25;


    public void ThrowSpell( Transform spellThrowPos, Statistics statistics, ControllerManager controllerManager)
    {
   
        Instantiate(spellProjectile.gameObject, spellThrowPos.position, spellThrowPos.rotation);
        //nextSpell = Time.time + cooldown;

        SoundManager.Instance.PlaySound(SoundManager.Sound.PlayerThrowSpell);
        
    }
}
