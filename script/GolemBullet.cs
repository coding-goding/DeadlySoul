using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBullet : MonoBehaviour
{
    Rigidbody2D target;

    Rigidbody2D rigid;
    
    Vector2 des;

    int tick = 0;
    int maxTick = 200;
    // Start is called before the first frame update
    private void Awake() {
        target = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        des = target.transform.position - transform.position;
        rigid.AddForce(des.normalized * GameManager.instance.golem_bullet_speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if(maxTick <= tick) {
            Destroy(gameObject);
        }
        else {
            if(!ExitManager.isStop) {
                tick++;
            }
            
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("attackEff")) return;
        Destroy(gameObject);
    }
}
