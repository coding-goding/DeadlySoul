using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAudioManager : MonoBehaviour
{
    public static TitleAudioManager i;

    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;
    public float bgmPitch;
    // Start is called before the first frame update
    void Start()
    {
        i = this;
        Init();
        PlayBgm(true);
    }
    void Init()
    {
        GameObject bgmObject = new GameObject("bgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = true;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;
        bgmPlayer.pitch = 1;
    }

    public void PlayBgm(bool isPlaying)
    {
        if (isPlaying)
        {
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
