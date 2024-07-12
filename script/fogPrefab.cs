using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fogPrefab : MonoBehaviour
{
    int fading = 0;
    Color color;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name == "Player") {
            fading = 1;
        }
         
    }
    // Start is called before the first frame update
    void Start()
    {
       color = gameObject.GetComponent<SpriteRenderer>().color;
    }
    void FadeOut() {
        
        
    }
    // Update is called once per frame
    void Update()
    {
        if(fading == 1) {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, gameObject.GetComponent<SpriteRenderer>().color.a - 0.01f);
            if(gameObject.GetComponent<SpriteRenderer>().color.a <= 0) {
                Destroy(gameObject);
            }
            
        }   
    }
}
