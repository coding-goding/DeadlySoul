using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int player_damage;
    public float player_speed;
    public int player_maxHealth;
    public int player_curHealth;
    public int player_Hitcool;

    public int goblin_damage;

    public int golem_bullet_speed;
    public int golem_range;
    public int golem_damage;
    public int golem_bullet_damage;
    public int golem_shoot_coolTime;

    public int bat_damage;
    
    public int tree_damage;
    public int wood_damage;
    public int tree_spawnCool;

    public int existEnemy;
    

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

    }
    void Start() {
        
        if(StageRemainder.instance.stage != 1) {
            instance.player_speed = StageRemainder.instance.speed;
            instance.player_maxHealth = StageRemainder.instance.maxHealth;
            instance.player_curHealth = StageRemainder.instance.curHealth;
            instance.player_damage = StageRemainder.instance.damage;
            instance.player_Hitcool = StageRemainder.instance.Hitcool;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void damageUp(int value) {
        player_damage += value;
    }
    public void speedUp(int value)
    {
        player_speed += value;
    }
    public void maxHealthUp(int value)
    {
        player_maxHealth += value;
        player_curHealth += value;
    }
    public void immuneTimeUp(int value)
    {
        player_Hitcool += value;
    
    }
}
