using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance => instance;

    [SerializeField] private AudioSource[] listSFX;
    [SerializeField] private Transform sfxParent;

    [SerializeField] private AudioSource[] listBGM;
    [SerializeField] private Transform bgmParent;

    public bool isPlayBGM;
    private int bgmIndex;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void OnValidate() => LoadAudio();

    private void LoadAudio()
    {
        if (sfxParent != null)
            listSFX = sfxParent.GetComponentsInChildren<AudioSource>();

        if (bgmParent != null)
            listBGM = bgmParent.GetComponentsInChildren<AudioSource>();
    }

    private void Update()
    {
        if (!isPlayBGM)
            StopAllBGM();
        else
        {
            if (!listBGM[bgmIndex].isPlaying)
                PlayBGM(bgmIndex);
        }
    }

    public void PlaySFX(int _sfxIndex)
    {
        if (_sfxIndex < listSFX.Length)
        {
            listSFX[_sfxIndex].pitch = Random.Range(0.85f, 1.1f);
            listSFX[_sfxIndex].Play();
        }
    }

    public void StopSFX(int _sfxIndex) => listSFX[_sfxIndex].Stop();

    public void PlayRandomBGM()
    {
        bgmIndex = Random.Range(0, listBGM.Length);
        PlayBGM(bgmIndex);
    }

    public void PlayBGM(int _bgmIndex)
    {
        bgmIndex = _bgmIndex;

        StopAllBGM();
        listBGM[bgmIndex].Play();
    }

    private void StopAllBGM()
    {
        for (int i = 0; i < listBGM.Length; i++)
        {
            listBGM[i].Stop();
        }
    }
}
