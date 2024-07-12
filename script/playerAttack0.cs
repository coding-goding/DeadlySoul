using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack0 : MonoBehaviour
{
    Color color;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        color = gameObject.GetComponent<SpriteRenderer>().color;
        anim = GetComponent<Animator>();
    }
    

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("AttackEffect0") == false)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, gameObject.GetComponent<SpriteRenderer>().color.a - 0.05f);
            if (gameObject.GetComponent<SpriteRenderer>().color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
