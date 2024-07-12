using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;
    public float bgmPitch;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    int channelIndex;
    AudioSource[] sfxPlayers;

    public enum Sfx {roomClear, teleport, roomEnter, attack, death, kill, soul, hit}  

    void Awake() {
        instance = this;   
        Init();
    }
    void Init() {
        GameObject bgmObject = new GameObject("bgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = true;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;
        bgmPlayer.pitch = 1;

        GameObject sfxObject = new GameObject("sfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for(int i=0;i<sfxPlayers.Length;i++) {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].volume = sfxVolume;
        }
    }

    public void PlayBgm(bool isPlaying) {
        if(isPlaying) {
            bgmPlayer.Play();
        }
        else {
            bgmPlayer.Stop();
        }
    }

    public void PlaySfx(Sfx sfx) {
        for(int i = 0;i < sfxPlayers.Length;i++) {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;
            if(sfxPlayers[loopIndex].isPlaying) continue;

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        instance.PlayBgm(true);
    }

    // Update is called once per frame
    void Update()
    {
        bgmPlayer.pitch = 0.8f + ((float)GameManager.instance.player_curHealth / (float)GameManager.instance.player_maxHealth) * 0.2f;
    }
}
