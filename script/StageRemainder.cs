using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageRemainder : MonoBehaviour
{

    public static StageRemainder instance = null;
    public int stage = 1;
    public int damage;
    public int maxHealth;
    public int curHealth;
    public float speed;
    public int Hitcool;

    void Start() {
        damage = GameManager.instance.player_damage;
        maxHealth = GameManager.instance.player_maxHealth;
        curHealth = GameManager.instance.player_curHealth;
        speed = GameManager.instance.player_speed;
        Hitcool = GameManager.instance.player_Hitcool;
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance == null) {
            Destroy(gameObject);
        }
    }

}
