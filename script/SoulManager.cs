using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulManager : MonoBehaviour
{
    Color col;
    // Start is called before the first frame update
    void Awake()
    {
        col = gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<SpriteRenderer>().color.a > 0) {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.025f, 0);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(col.r, col.g, col.b, gameObject.GetComponent<SpriteRenderer>().color.a-0.015f);
        }
        else {
            Destroy(gameObject);
        }
    }
}
