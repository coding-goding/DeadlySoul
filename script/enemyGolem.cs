using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGolem : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    float speed;
    int health;
    public Rigidbody2D target;
    bool isLive = true;
    bool activated = true;
    enemy Setting = new enemy(3);
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    Collider2D col;
    WaitForFixedUpdate wait;
    int idleTick;
    int tick;
    float distance;

    // Start is called before the first frame update
    void Awake()
    {
        tick = 0;
        health = Setting.health;
        speed = Setting.speed;
        target = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        wait = new WaitForFixedUpdate();
        idleTick = 20;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distance = (target.transform.position - transform.position).magnitude;

        if(tick > 0) {
            tick--;
        }

        if (idleTick > 0)
        {
            idleTick--;
            return;
        }

        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        

        if (activated && distance > GameManager.instance.golem_range)
        {
            Vector2 dirVec = target.position - rigid.position;
            Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);
            rigid.velocity = Vector2.zero;
        }
        if (activated && distance <= GameManager.instance.golem_range)
        {
            if(tick == 0) {
                tick = GameManager.instance.golem_shoot_coolTime;
                Instantiate(bullet, transform.position, Quaternion.identity);
            }
        }
    }
    private void LateUpdate()
    {
        if (idleTick > 0) return;
        if (!isLive) return;
        spriter.flipX = target.position.x < rigid.position.x;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (idleTick > 0) return;
        if (!collision.CompareTag("attackEff")) return;
        health -= GameManager.instance.player_damage;
        StartCoroutine(KnockBack());

        if (health > 0)
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            isLive = false;
            rigid.simulated = false;
            AudioManager.instance.PlaySfx(AudioManager.Sfx.kill);
            col.enabled = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
        }
    }
    IEnumerator KnockBack()
    {
        yield return wait;
        Vector3 playerPos = target.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }
    void Dead()
    {
        Destroy(gameObject);
    }
}
