using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitManager : MonoBehaviour
{
    static public bool isStop;
    // Start is called before the first frame update
    void Start()
    {
        isStop = false;
        gameObject.GetComponent<Text>().color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.player_curHealth > 0) {
            if(Input.GetKeyDown(KeyCode.Escape)) {
                if (isStop) {
                    Time.timeScale = 1;
                    isStop = false;
                    gameObject.GetComponent<Text>().color = new Color(1, 1, 1, 0);
                    return;
                }
                else {
                   Time.timeScale = 0;
                   isStop = true;
                    gameObject.GetComponent<Text>().color = new Color(1, 1, 1, 1);
                   
                    return; 
                }
            }
            if(isStop) {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    StageRemainder.instance.stage = 1;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    AudioManager.instance.PlayBgm(true);
                    isStop = false;
                    gameObject.GetComponent<Text>().color = new Color(1, 1, 1, 0);
                    Time.timeScale = 1;
                }
                if (Input.GetKeyDown(KeyCode.B))
                {
                    StageRemainder.instance.stage = 1;
                    isStop = false;
                    Time.timeScale = 1;
                    gameObject.GetComponent<Text>().color = new Color(1, 1, 1, 0);
                    SceneManager.LoadScene("TitleScene");
                }
            }
            
        }
    }
}
