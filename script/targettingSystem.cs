using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class targettingSystem : MonoBehaviour
{
    //[SerializeField] GameObject cursor;
    [SerializeField] GameObject player;
    [SerializeField] GameObject AttackEff0;
    [SerializeField] GameObject TeleportEff;
    [SerializeField] Animator Panim;
    [SerializeField] Tilemap tileMap;
    [SerializeField] Tile outTile;
    [SerializeField] Tile wallTile;

    Vector3 point;
    int Atick;
    int AMtick;
    bool Acool;
    int Ttick;
    int TMtick;
    bool Tcool;
    bool isCursorEnabled;

    // Start is called before the first frame update
    void Start()
    {
        isCursorEnabled = false;
        Cursor.visible = false;
        Atick = 0;
        AMtick = 10;
        Acool = false;
        Ttick = 0;
        TMtick = 30;
        Tcool = false;
    }

    // Update is called once per frame
    void Update() {
        if(!ExitManager.isStop) {
            if (Input.GetKeyDown(KeyCode.O))
            {
                isCursorEnabled = !isCursorEnabled;
            }
            if (GameManager.instance.player_curHealth <= 0) return;
            if (Input.GetMouseButtonDown(0))
            {
                if (!Acool)
                {
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.attack);
                    Acool = true;
                    Panim.SetBool("attack0", true);
                    Vector3 dir = point - player.GetComponent<Transform>().position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    GameObject eff = Instantiate(AttackEff0, player.GetComponent<Transform>().position, Quaternion.identity);
                    eff.GetComponent<Transform>().rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                }

            }
            if (Input.GetMouseButtonDown(1))
            {
                if (!Tcool)
                {
                    if (MapGenerator.currentNode.clear || (MapGenerator.currentNode.nodeRect.x - MapGenerator.mapSize.x / 2 < point.x && MapGenerator.currentNode.nodeRect.x + MapGenerator.currentNode.nodeRect.width - MapGenerator.mapSize.x / 2 > point.x))
                    {
                        if (MapGenerator.currentNode.clear || (MapGenerator.currentNode.nodeRect.y - MapGenerator.mapSize.y / 2 < point.y && MapGenerator.currentNode.nodeRect.y + MapGenerator.currentNode.nodeRect.height - MapGenerator.mapSize.y / 2 > point.y))
                        {
                            if (tileMap.GetTile(new Vector3Int((int)point.x, (int)point.y, 0)) != outTile && tileMap.GetTile(new Vector3Int((int)point.x, (int)point.y, 0)) != wallTile)
                            {
                                Tcool = true;
                                Panim.SetBool("teleport", true);
                                Vector3 des = point;
                                AudioManager.instance.PlaySfx(AudioManager.Sfx.teleport);
                                GameObject eff = Instantiate(TeleportEff, player.GetComponent<Transform>().position, Quaternion.identity);
                                if (player.GetComponent<SpriteRenderer>().flipX == true)
                                {
                                    eff.GetComponent<SpriteRenderer>().flipX = true;
                                }
                                player.GetComponent<Transform>().position = des;

                            }
                        }
                    }


                }

            }
        }
        
        
        
    }
    void FixedUpdate()
    {
        if(Acool) {
            Atick++;
            if(Atick >= AMtick) {
                Panim.SetBool("attack0", false);
                Atick = 0;
                Acool = false;
            }
        }
        if (Tcool)
        {
            Ttick++;
            if (Ttick >= TMtick)
            {
                Ttick = 0;
                Tcool = false;
            }
        }
        if (Panim.GetCurrentAnimatorStateInfo(0).IsName("PTeleport1") == true && Panim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            if (Tcool)
            {
                Panim.SetBool("teleport", false);
            }
        }
        Cursor.visible = isCursorEnabled;
        point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        gameObject.GetComponent<Transform>().position = point;

    }
    void LateUpdate()
    {
        
    }
}
