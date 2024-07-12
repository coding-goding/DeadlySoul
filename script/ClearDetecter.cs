using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearDetecter : MonoBehaviour
{
    Node nowNode = null;
    bool isStartSet = false;
    List<enemy> mobList = new List<enemy>();
    List<GameObject> enemyList = new List<GameObject>();
    [SerializeField] GameObject Goblin;
    [SerializeField] GameObject WhiteBat;
    [SerializeField] GameObject Golem;
    [SerializeField] GameObject Tree;
    [SerializeField] GameObject Soul;
    [SerializeField] GameObject heal;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MapGenerator.currentNode != null && !isStartSet) {
            nowNode = MapGenerator.currentNode;
            isStartSet = true;
            return;
        }
        if(MapGenerator.currentNode != nowNode) {
            //map changed
            Node node = MapGenerator.currentNode;
            if(node.clear == false && node.active == false) {
                MapGenerator.currentNode.active = true;
                AudioManager.instance.PlaySfx(AudioManager.Sfx.roomEnter);
                if (node.type == 0) {
                    int count = 0;
                    while(count <= node.difficulty) {
                        int enemyn = Random.Range(1, 4);
                        enemy Senemy = new enemy(enemyn);
                        count += Senemy.cost;
                        

                        Vector2Int spawnPlace = new Vector2Int(1, 1);
                        spawnPlace.x = Random.Range(MapGenerator.currentNode.roomRect.x - MapGenerator.mapSize.x / 2, MapGenerator.currentNode.roomRect.x + MapGenerator.currentNode.roomRect.width - MapGenerator.mapSize.x / 2);
                        spawnPlace.y = Random.Range(MapGenerator.currentNode.roomRect.y - MapGenerator.mapSize.y / 2, MapGenerator.currentNode.roomRect.y + MapGenerator.currentNode.roomRect.height - MapGenerator.mapSize.y / 2);
                        if(Senemy.type == 1) {
                            Senemy.mob = Instantiate(Goblin, new Vector3(spawnPlace.x, spawnPlace.y, 0), Quaternion.identity);
                        }
                        if (Senemy.type == 2)
                        {
                            Senemy.mob = Instantiate(WhiteBat, new Vector3(spawnPlace.x, spawnPlace.y, 0), Quaternion.identity);
                        }
                        if (Senemy.type == 3)
                        {
                            Senemy.mob = Instantiate(Golem, new Vector3(spawnPlace.x, spawnPlace.y, 0), Quaternion.identity);
                        }
                        mobList.Add(Senemy);
                    }
                }
                else if(node.type == 2) {
                    Vector2Int spawnPlace = new Vector2Int(1, 1);
                    enemy Senemy = new enemy(4);
                    spawnPlace.x = (int)MapGenerator.currentNode.roomRect.center.x - MapGenerator.mapSize.x/2;
                    spawnPlace.y = (int)MapGenerator.currentNode.roomRect.center.y- MapGenerator.mapSize.y / 2;
                    Senemy.mob = Instantiate(Tree, new Vector3(spawnPlace.x, spawnPlace.y, 0), Quaternion.identity);
                    mobList.Add(Senemy);
                }
                else if(node.type == 1) {
                    // treasure
                    int val = Random.Range(3, 6);
                    for(int i = 0;i < val;i++) {
                        Vector2Int spawnPlace = new Vector2Int(1, 1);   
                        spawnPlace.x = Random.Range(MapGenerator.currentNode.roomRect.x - MapGenerator.mapSize.x / 2, MapGenerator.currentNode.roomRect.x + MapGenerator.currentNode.roomRect.width - MapGenerator.mapSize.x / 2);
                        spawnPlace.y = Random.Range(MapGenerator.currentNode.roomRect.y - MapGenerator.mapSize.y / 2, MapGenerator.currentNode.roomRect.y + MapGenerator.currentNode.roomRect.height - MapGenerator.mapSize.y / 2);
                        Instantiate(Soul, new Vector3(spawnPlace.x, spawnPlace.y, 0), Quaternion.identity);
                    }

                    node.clear = true;
                    node.active = false;
                }
            } 
            if(node.clear == false && node.active == true) {
                
                for(int i = 0;i < mobList.Count;i++) {
                    if(!mobList[i].mob) {
                        mobList.RemoveAt(i);
                        i--;
                    }
                    
                }
                GameManager.instance.existEnemy = mobList.Count;
                if(mobList.Count == 0) {
                    MapGenerator.currentNode.clear = true;
                    MapGenerator.currentNode.active = false;
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.roomClear);
                    nowNode = MapGenerator.currentNode;
                    int healup = Random.Range(1, 4);
                    if(healup == 1) {
                        GameManager.instance.player_curHealth += 20;
                        if(GameManager.instance.player_curHealth > GameManager.instance.player_maxHealth) {
                            GameManager.instance.player_curHealth = GameManager.instance.player_maxHealth;
                        }
                        Instantiate(heal, GameObject.Find("Player").transform.position, Quaternion.identity);
                    }
                    //health
                    if(node.type == 2) {
                        StageRemainder.instance.stage++;
                        StageRemainder.instance.speed = GameManager.instance.player_speed;
                        StageRemainder.instance.maxHealth = GameManager.instance.player_maxHealth;
                        StageRemainder.instance.curHealth = GameManager.instance.player_curHealth;
                        StageRemainder.instance.damage = GameManager.instance.player_damage;
                        StageRemainder.instance.Hitcool = GameManager.instance.player_Hitcool;

                        

                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        AudioManager.instance.PlayBgm(true);
                    }
                }
                
            }
            
        }
        else {
            nowNode = MapGenerator.currentNode;
        }
    }
}
