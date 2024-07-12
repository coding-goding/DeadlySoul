using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { health, exist, gameoverBG, gameoverT}
    public InfoType type;
    SpriteRenderer sp;
    Text Text;
    Slider healthBar;
    Image Image;
    private void Awake() {
        Text = GetComponent<Text>();
        healthBar = GetComponent<Slider>();
        Image = GetComponent<Image>();
        sp = GameObject.Find("Player").GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        switch (type) {
            case InfoType.health:
                float curHealth = GameManager.instance.player_curHealth;
                float maxHealth = GameManager.instance.player_maxHealth;
                healthBar.value = curHealth / maxHealth;
                break;
            case InfoType.exist:
                int exist = GameManager.instance.existEnemy;
                if (GameManager.instance.existEnemy > 0)
                {
                    Text.color = new Color(1, 0, 0);
                }
                else {
                    Text.color = new Color(1, 1, 1);
                }
                Text.text = exist + " left";
                break;
            case InfoType.gameoverBG:
                if(GameManager.instance.player_curHealth <= 0) {
                    if(Image.color.a < 0.3f) {
                        Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, Image.color.a + 0.002f);
                    }
                    
                }
                break;
            case InfoType.gameoverT:
                if (GameManager.instance.player_curHealth <= 0 && sp.color.a == 0)
                {
                    Text.text = Text.text + "\n" + StageRemainder.instance.stage + " stage";
                    if (Text.color.a < 1)
                    {
                        Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, Text.color.a + 0.1f);
                    }
                    
                }
                
                break;

        }
    }
}
