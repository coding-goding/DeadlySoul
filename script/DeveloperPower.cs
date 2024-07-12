using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperPower : MonoBehaviour
{
    [SerializeField] GameObject healthUP;
    [SerializeField] GameObject damageUP;
    [SerializeField] GameObject speedUP;
    [SerializeField] GameObject immuneUP;
    [SerializeField] GameObject player;

    char lastCode;
    int a, b, c, d, e, f;

    // Start is called before the first frame update
    void Start()
    {
        a = 0;
        b = 0;
        c = 0;
        d = 0;
        e = 0;
        f = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown) {
            if(Input.GetKeyDown(KeyCode.A) && a == 0) {
                a++;
                b = 0;
                c = 0;
                d = 0;
                e = 0;
                f = 0;
            }
            else if (Input.GetKeyDown(KeyCode.K) && a == 1)
            {
                a++;
            }
            else if (Input.GetKeyDown(KeyCode.S) && a == 2)
            {
                a++;
            }
            else if (Input.GetKeyDown(KeyCode.L) && a == 3)
            {
                a = 0;
                GameManager.instance.player_curHealth = GameManager.instance.player_maxHealth;
                AudioManager.instance.PlaySfx(AudioManager.Sfx.soul);
            }
            else if (Input.GetKeyDown(KeyCode.P) && b == 0)
            {
                b++;
                a = 0;
                c = 0;
                d = 0;
                e = 0;
                f = 0;
            }
            else if (Input.GetKeyDown(KeyCode.J) && b == 1)
            {
                b++;
            }
            else if (Input.GetKeyDown(KeyCode.S) && b == 2)
            {
                b++;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0) && b == 3)
            {
                b = 0;
                Instantiate(damageUP, player.transform.position, Quaternion.identity);
                GameManager.instance.damageUp(50);
                AudioManager.instance.PlaySfx(AudioManager.Sfx.soul);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9) && c == 0)
            {
                b = 0;
                a = 0;
                d = 0;
                e = 0;
                f = 0;
                c++;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1) && c == 1)
            {
                c++;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && c == 2)
            {
                c++;
            }
            else if (Input.GetKeyDown(KeyCode.S) && c == 3)
            {
                c = 0;
                Instantiate(healthUP, player.transform.position, Quaternion.identity);
                GameManager.instance.maxHealthUp(100);
                AudioManager.instance.PlaySfx(AudioManager.Sfx.soul);
            }
            else if (Input.GetKeyDown(KeyCode.F) && d == 0)
            {
                b = 0;
                c = 0;
                a = 0;
                e = 0;
                f = 0;
                d++;
            }
            else if (Input.GetKeyDown(KeyCode.U) && d == 1)
            {
                d++;
            }
            else if (Input.GetKeyDown(KeyCode.N) && d == 2)
            {
                d = 0;
                Instantiate(speedUP, player.transform.position, Quaternion.identity);
                GameManager.instance.speedUp(10);
                AudioManager.instance.PlaySfx(AudioManager.Sfx.soul);
            }
            else if (Input.GetKeyDown(KeyCode.S) && e == 0)
            {
                b = 0;
                c = 0;
                d = 0;
                a = 0;
                f = 0;
                e++;
            }
            else if (Input.GetKeyDown(KeyCode.U) && e == 1)
            {
                e++;
            }
            else if (Input.GetKeyDown(KeyCode.C) && e == 2)
            {
                e++;
            }
            else if (Input.GetKeyDown(KeyCode.K) && e == 3)
            {
                e = 0;
                Instantiate(immuneUP, player.transform.position, Quaternion.identity);
                GameManager.instance.immuneTimeUp(20);
                AudioManager.instance.PlaySfx(AudioManager.Sfx.soul);
            }
            else if (Input.GetKeyDown(KeyCode.KeypadDivide) && f == 0)
            {
                b = 0;
                c = 0;
                d = 0;
                e = 0;
                a = 0;
                f++;
            }
            else if (Input.GetKeyDown(KeyCode.KeypadMultiply) && f == 1)
            {
                f++;
            }
            else if (Input.GetKeyDown(KeyCode.KeypadMinus) && f == 2)
            {
                f++;
            }
            else if (Input.GetKeyDown(KeyCode.KeypadPlus) && f == 3)
            {
                f = 0;
                player.transform.position = MapGenerator.bossRoomVec;
                AudioManager.instance.PlaySfx(AudioManager.Sfx.soul);
            }
            else {
                a = 0;
                b = 0;
                c = 0;
                d = 0;
                e = 0;
                f = 0;
            }
        }
    }
}
