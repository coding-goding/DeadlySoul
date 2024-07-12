using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartManager : MonoBehaviour
{

    SpriteRenderer sp;
    // Start is called before the first frame update
    void Start()
    {
        sp = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        AudioManager.instance.PlayBgm(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.player_curHealth <= 0 && sp.color.a == 0) {
            if(Input.GetKey(KeyCode.Escape)) {
                StageRemainder.instance.stage = 1;
                SceneManager.LoadScene("TitleScene");
            }
            else if (Input.anyKey)
            {
                StageRemainder.instance.stage = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                AudioManager.instance.PlayBgm(true);
            }
        }
    }
}
