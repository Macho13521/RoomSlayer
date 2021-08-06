using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private static int hint = 0;

    private static string[] hints =
    {
        "Poruszaj siê na WASD",
        "Uderzaj u¿ywaj¹c LPM",
        "Unikaj u¿ywaj¹c Spacji",
    };

    private static string getSword = "U¿ywaj magii na PPM";

    private static string getLvl = "Wciœnij U do Ulepszeñ";

    static bool getFirstLvl = false;

    static bool getFirstSwordLvl = false;

    
    private static TextMeshProUGUI text;


    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        text.enabled = true;
    }


    public static void FirstMove()
    {
        if (hint == 0)
        {
            hint++;
            text.text = hints[hint];
        }
    }

    public static void FirstLPM()
    {
        if (hint == 1)
        {
            hint++;
            text.text = hints[hint];
        }
    }

    public static void FirstDash()
    {
        if (hint == 2)
            text.enabled = false;
    }


    public static void FirstLvl()
    {
        if (!getFirstLvl)
        {
            text.enabled = true;
            text.text = getLvl;

        }
    }

    public static void FirstUClick()
    {
        if (!getFirstLvl)
        {
            getFirstLvl = true;
            text.enabled = false;
        }
    }

    public static void FirstPPMClick()
    {
        if (!getFirstSwordLvl)
        {
            getFirstSwordLvl = true;
            text.enabled = false;
        }
    }

    public static void FirstSword()
    {
        if (!getFirstSwordLvl)
        {
            text.enabled = true;
            text.text = getSword;

        }
    }

}
