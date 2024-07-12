using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    float alpha = 1;
    bool emissioning = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit(); // 어플리케이션 종료
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            ExitGame();
        }
        else if (Input.anyKey)
        {
            SceneManager.LoadScene("Maingame");
            
        }
        if (!emissioning) {
            if(alpha >= 0.1) {
                alpha -= 0.01f;    
            }
            else {
                emissioning = true;
            }
        }
        else {
            if(alpha < 1) {
                alpha += 0.01f;
            }
            else {
                emissioning = false;
            }
        }
        gameObject.GetComponent<Text>().color = new Color(1, 1, 1, alpha);
    }
}
