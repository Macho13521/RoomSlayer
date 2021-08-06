using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    #region Singelton
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            Poziom = 0;
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(player);
            DontDestroyOnLoad(canvas);
            DontDestroyOnLoad(camera);
            DontDestroyOnLoad(postProcesing);
        }
    }
    #endregion


    public int Poziom { get; private set; }

    private float flyHpGain = 1.40f;
    private float flyDmgGain = 1.30f;

    private float slimeHpGain = 1.30f;
    private float slimeDmgGain = 1.40f;

    public float GetFlyHpGain() {return Mathf.Pow(flyHpGain, Poziom);}
    public float GetFlyDmgGain() { return Mathf.Pow(flyDmgGain, Poziom); }
    public float GetSlimeHpGain() { return Mathf.Pow(slimeHpGain, Poziom); }
    public float GetSlimeDmgGain() { return Mathf.Pow(slimeDmgGain, Poziom); }


    public GameObject canvas;
    public GameObject player;
    public GameObject camera;
    public GameObject postProcesing;
    public TextMeshProUGUI textPiêtra;

    public void changeFloorLevel()
    {
        Poziom++;
        SceneManager.LoadScene("Gra" + (Poziom + 1).ToString());
        player.transform.position = new Vector3(0, player.transform.position.y, 0);
        textPiêtra.text = "Piêtro " + (Poziom + 1).ToString();
        if(Poziom == 4)
        {
            Destroy(player);
            
            Destroy(canvas);
            
            Destroy(SoundManager.Instance.gameObject);
            Destroy(camera, 0.1f);
            Invoke("goToMenu", 10.0f);
            Destroy(gameObject, 10.1f);
        }
    }

    private void goToMenu()
    {
        SceneManager.LoadScene("ScenaPierwsza");
    }

    internal void gameOver()
    {
        Destroy(player);
        Destroy(camera);
        Destroy(canvas);
        SceneManager.LoadScene("Koniec");
        Destroy(gameObject);
        Destroy(SoundManager.Instance.gameObject);
    }


}
