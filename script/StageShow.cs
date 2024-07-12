using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageShow : MonoBehaviour
{

    bool isShowed;
    int showTick = 200;
    int tick = 0;
    float alpha = 0;
    void Start()
    {
        isShowed = false;
        //gameObject.GetComponent<Text>().text = "stage " + StageRemainder.instance.stage; 
    }

    // Update is called once per frame
    void Update()
    {
        if(!isShowed) {
            gameObject.GetComponent<Text>().text = "stage " + StageRemainder.instance.stage;
            if (gameObject.GetComponent<Text>().color.a < 1) {
                gameObject.GetComponent<Text>().color = new Color(1, 1, 1, alpha);
                alpha += 0.02f;
            }
            else {
                isShowed = true;
            }
        }
        else {
            if(tick < showTick) {
                tick++;
            }
            else {
                if(gameObject.GetComponent<Text>().color.a > 0) {
                    gameObject.GetComponent<Text>().color = new Color(1, 1, 1, alpha);
                    alpha -= 0.02f;
                }
            }
        }
    }
}
