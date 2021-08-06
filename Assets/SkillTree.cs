using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    public Action<int> changeRed, changeBlue;
    public Action<int, float> changeGreen;

    [SerializeField]
    static public int redLvl = 0;
    [SerializeField]
    static public int greenLvl = 0;
    [SerializeField]
    static public int blueLvl = 0;

    public Action<float> gainHp;

    public static float playerHpGain = 1.22f;
    public static float playerDamageGain = 1f;
    public static float playerManaGain = 1f;

    static public float playerMaxHp = 100f;

    [SerializeField]
    private Button btnRed, btnBlue, btnGreen;

    [SerializeField]
    PlayerHud playerHud;

    private void Awake()
    {
        playerHud.skillBtnInteraction = skillsClickable;
    }

    public void OnRedClick()
    {
        redLvl++;
        playerDamageGain *= 1.35f;
        changeRed?.Invoke(redLvl);
        onClickClose();
    }

    public void OnGreenClick()
    {
        greenLvl++;
        changeGreen?.Invoke(greenLvl, playerHpGain);
        playerMaxHp *= playerHpGain;
        gainHp?.Invoke(playerHpGain);
        onClickClose();


    }

    public void OnBlueClick()
    {
        blueLvl++;
        playerManaGain *= 1.01f;
        changeBlue?.Invoke(blueLvl);
        onClickClose();
    }

    void onClickClose()
    {
        //gameObject.SetActive(false);
    }

    void skillsClickable(bool can)
    {
        btnRed.interactable = can;
        btnBlue.interactable = can;
        btnGreen.interactable = can;
    }    
}
