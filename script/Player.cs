using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rigid;
    Collider2D col;
    Vector2 inputVec;
    SpriteRenderer spriter;
    Animator anim;
    [SerializeField] Camera MainCamera;
    [SerializeField] GameObject healthUP;
    [SerializeField] GameObject damageUP;
    [SerializeField] GameObject speedUP;
    [SerializeField] GameObject immuneUP;
    float speed = 0;
    int hitCool = 0;
    int tick = 0;
    public static Node currentNode;
    Color defaultColor;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        defaultColor = spriter.color;
        
    }
    void Start() {
        speed = GameManager.instance.player_speed;
        hitCool = GameManager.instance.player_Hitcool;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.player_curHealth <= 0) return; 
        speed = GameManager.instance.player_speed;
        hitCool = GameManager.instance.player_Hitcool;

        currentNode = MapGenerator.currentNode;
        if(MainCamera.enabled == true && !ExitManager.isStop) {
            inputVec.x = Input.GetAxisRaw("Horizontal");
            inputVec.y = Input.GetAxisRaw("Vertical");
        }
        else {
            inputVec.x = 0;
            inputVec.y = 0;
        }
        if(Input.GetKeyDown(KeyCode.Space)) {
            currentNode.clear = true;
        }
        
    }
    void FixedUpdate()
    {
        if(tick >0) {
            tick--;
            spriter.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, spriter.color.a + 0.02f);
        }
        if (anim.GetBool("attack0") == false) {
            Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);
        }
        if (anim.GetBool("attack0") == true)
        {
            rigid.velocity = Vector2.zero;
        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject cols = collision.gameObject;
        if (cols.CompareTag("bullet"))
        {
            Destroy(cols);
        }
        if(cols.CompareTag("soul")) {
            Vector3 place = cols.transform.position;
            Destroy(cols);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.soul);
            int type = Random.Range(1, 5);
            if(type == 1) { // damage
                GameManager.instance.damageUp(Random.Range(1, 13));
                Instantiate(damageUP, place, Quaternion.identity);
            }
            else if (type == 2)
            { 
                GameManager.instance.maxHealthUp(Random.Range(1, 6) * 10);
                Instantiate(healthUP, place, Quaternion.identity);
            }
            else if (type == 3)
            {
                GameManager.instance.speedUp(Random.Range(1, 6));
                Instantiate(speedUP, place, Quaternion.identity);
            }
            else if (type == 4)
            {
                GameManager.instance.immuneTimeUp(Random.Range(1, 5) * 5);
                Instantiate(immuneUP, place, Quaternion.identity);
            }
        }
        if (tick > 0) return;
        if (!cols.CompareTag("goblin") && !cols.CompareTag("bat") && !cols.CompareTag("golem") && !cols.CompareTag("bullet") && !cols.CompareTag("tree") && !cols.CompareTag("wood")) return;
        if (cols.CompareTag("goblin"))
        {
            GameManager.instance.player_curHealth -= GameManager.instance.goblin_damage;
        }
        if (cols.CompareTag("bat"))
        {
            GameManager.instance.player_curHealth -= GameManager.instance.bat_damage;
        }
        if (cols.CompareTag("golem"))
        {
            GameManager.instance.player_curHealth -= GameManager.instance.golem_damage;
        }
        if (cols.CompareTag("bullet")) {
            GameManager.instance.player_curHealth -= GameManager.instance.golem_bullet_damage;
        }
        if (cols.CompareTag("tree"))
        {
            GameManager.instance.player_curHealth -= GameManager.instance.tree_damage;
        }
        if (cols.CompareTag("wood"))
        {
            GameManager.instance.player_curHealth -= GameManager.instance.wood_damage;
        }
        if (GameManager.instance.player_curHealth > 0)
        {
            //anim.SetTrigger("Hit");
            AudioManager.instance.PlaySfx(AudioManager.Sfx.hit);
            tick = hitCool;
            spriter.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 1-(float)GameManager.instance.player_Hitcool/50.0f);
        }
        else
        {
            rigid.simulated = false;
            col.enabled = false;
            anim.SetBool("dead", true); 
            AudioManager.instance.PlaySfx(AudioManager.Sfx.death);
            AudioManager.instance.PlayBgm(false);
        }
    }
    void LateUpdate()
    {
        anim.SetFloat("speed", inputVec.magnitude);
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
        if(!currentNode.clear) {
            
        }
        
    }
    void deadAnim() {
        anim.SetBool("dead", false);
    }
    void Death() {
        spriter.color = new Color(0, 0, 0, 0);
    }
}
