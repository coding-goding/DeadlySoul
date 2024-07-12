using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGoblin : MonoBehaviour
{
    float speed;
    int health;
    public Rigidbody2D target;
    bool isLive = true;
    bool activated= true;
    enemy Setting = new enemy(1);
    Rigidbody2D rigid;
    Collider2D col;
    SpriteRenderer spriter;
    Animator anim;
    WaitForFixedUpdate wait;
    int idleTick;

    // Start is called before the first frame update
    void Awake()
    {
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

    // Update is called once per frame
    void FixedUpdate()
    {
        if(idleTick > 0) {
            idleTick--;
            return;
        }

        if(!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        if(activated) {
            Vector2 dirVec = target.position - rigid.position;
            Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);
            rigid.velocity = Vector2.zero;
        }
    }
    private void LateUpdate() {
        if(idleTick > 0) return;
        if(!isLive) return;
        spriter.flipX = target.position.x < rigid.position.x;
    }
    void OnTriggerEnter2D(Collider2D collision) {
        if(idleTick > 0 ) return;
        if(!collision.CompareTag("attackEff")) return;
        health -= GameManager.instance.player_damage;
        StartCoroutine(KnockBack());

        if(health > 0) {
            anim.SetTrigger("Hit");
        }
        else {
            isLive = false;
            rigid.simulated = false;
            col.enabled = false;
            AudioManager.instance.PlaySfx(AudioManager.Sfx.kill);
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
        }
    }
    IEnumerator KnockBack() {
        yield return wait;
        Vector3 playerPos = target.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }
    void Dead() {
        Destroy(gameObject);
    }
}
