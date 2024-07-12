using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTree : MonoBehaviour
{
    float speed;
    int health;
    public Rigidbody2D target;
    bool isLive = true;
    bool activated = true;
    enemy Setting = new enemy(4);
    Rigidbody2D rigid;
    Collider2D col;
    SpriteRenderer spriter;
    Animator anim;
    WaitForFixedUpdate wait;
    int idleTick;
    int spawnCool;
    int tick;
    Vector3 pos;
    [SerializeField] GameObject wood;

    // Start is called before the first frame update
    void Awake()
    {
        tick = 0;
        spawnCool = GameManager.instance.tree_spawnCool;
        health = Setting.health;
        speed = Setting.speed;
        target = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
        col = GetComponent<Collider2D>();
        idleTick = 20;
    }
    void Start() {
        pos = gameObject.transform.position;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (idleTick > 0)
        {
            idleTick--;
            return;
        }
        
        if (!isLive) return;

        if (activated)
        {
            rigid.MovePosition(pos);
            if (tick <= 0) {
                Spawn();
                tick = spawnCool;
            } 
            else {
                tick--;
            }
        }
    }
    void Spawn() {
        Vector3 dirVec = target.position - rigid.position;
        Instantiate(wood, (transform.position + dirVec/10), Quaternion.identity);
    }
    private void LateUpdate()
    {
        if (idleTick > 0) return;
        if (!isLive) return;
        spriter.flipX = target.position.x > rigid.position.x;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (idleTick > 0) return;
        if (!collision.CompareTag("attackEff")) return;
        health -= GameManager.instance.player_damage;

        if (health > 0)
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            isLive = false;
            rigid.simulated = false;
            col.enabled = false;
            AudioManager.instance.PlaySfx(AudioManager.Sfx.kill);
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
        }
    }

    void Dead()
    {
        Destroy(gameObject);
    }
}
