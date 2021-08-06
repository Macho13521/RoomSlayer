using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singelton
    private static SoundManager instance;
    public static SoundManager Instance
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
            DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion

    public enum Sound
    {
        PlayerAtack, PlayerThrowSpell, PlayerHit,
        SlimeThrow, SlimeDamage,
        FlyMove, FlyDamage
    }

    public SoundAudioClip[] soundAudioClips;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }

    public void PlaySound(Sound sound)
    {
        GameObject soundGO = new GameObject("Sound");
        soundGO.transform.parent = transform;
        AudioSource audioSource = soundGO.AddComponent<AudioSource>();
        for(int i=0; i<soundAudioClips.Length; i++)
        {
            if (soundAudioClips[i].sound == sound)
            {
                audioSource.PlayOneShot(soundAudioClips[i].audioClip);
                Destroy(soundGO, soundAudioClips[i].audioClip.length + 0.1f);
            }
        }
        Destroy(soundGO, 2.0f);
    }
}
