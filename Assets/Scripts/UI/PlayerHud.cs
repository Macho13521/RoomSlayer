using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{
    
    

    [SerializeField]
    private Statistics stats;

    [SerializeField]
    private SkillTree skillTree;

    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private Text healthText;

    [SerializeField]
    private Slider manaSlider;
    [SerializeField]
    private Text manaText;

    [SerializeField]
    private Slider expSlider;
    [SerializeField]
    private Text expText;

    [SerializeField]
    private Text lvlText;


    [SerializeField]
    private Text redText;
    [SerializeField]
    private Text greenText;
    [SerializeField]
    private Text blueText;


    [SerializeField]
    private TextMeshProUGUI getLvlText;

    public Action<bool> skillBtnInteraction;

    private int skillPoints = 0;

    void Start()
    {
        stats.changeHealth = UpdateHealth;
        stats.changeExp = UpdateExp;
        stats.changeMana = UpdateMana;
        stats.changeLvl = UpdateLvl;
        skillTree.changeGreen = UpdateGreen;
        skillTree.changeRed = UpdateRed;
        skillTree.changeBlue = UpdateBlue;
        skillBtnInteraction?.Invoke(false);
    }



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            Tutorial.FirstUClick();
            if (!skillTree.gameObject.activeSelf)
            {
                skillTree.gameObject.SetActive(true);
                if (skillPoints > 0)
                    skillBtnInteraction?.Invoke(true);
                else
                    skillBtnInteraction?.Invoke(false);
            }
            else
            {
                skillTree.gameObject.SetActive(false);
            }
        }
    }

    void UpdateHealth(float health)
    {
        healthSlider.value = health;
        healthText.text = ((int)health).ToString() + "/" + ((int)healthSlider.maxValue).ToString();
    }

    void UpdateMana(float mana)
    {
        manaSlider.value = mana;
        manaText.text = ((int)mana).ToString() + "/" + ((int)manaSlider.maxValue).ToString();
    }

    void UpdateExp(float exp)
    {
        expSlider.value = exp;
        expText.text = ((int)exp).ToString() + "/" + ((int)expSlider.maxValue).ToString();
    }

    void UpdateLvl(int lblbefor, int lvl)
    {
        getLvlText.enabled = true;
        skillPoints += lvl - lblbefor;
        lvlText.text = "LVL " + lvl.ToString();
    }

    void UpdateRed(int points)
    {
        redText.text = points.ToString();
        UpdateSkillpoints();
    }

    void UpdateGreen(int points, float maxHealth)
    {

        healthSlider.maxValue *= maxHealth;
        greenText.text =  points.ToString();
        UpdateSkillpoints();
    }

    void UpdateBlue(int points)
    {
        blueText.text =  points.ToString();
        UpdateSkillpoints();
    }

    void UpdateSkillpoints()
    {
        getLvlText.enabled = false;
        skillPoints--;
        if (skillPoints > 0)
            skillBtnInteraction?.Invoke(true);
        else
            skillBtnInteraction?.Invoke(false);
    }
}
