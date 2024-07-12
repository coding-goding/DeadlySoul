using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine.Timeline;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] public static Vector2Int mapSize;
    [SerializeField] float minDivision;
    [SerializeField] float maxDivision;
    [SerializeField] private int Depth;
    [SerializeField] Tilemap tileMap;
    [SerializeField] RuleTile roomTile;
    [SerializeField] Tile treasureTile;
    [SerializeField] Tile BossTile;
    [SerializeField] Tile wallTile;
    [SerializeField] Tile blockTile;
    [SerializeField] Tile outTile;
    [SerializeField] GameObject player;
    [SerializeField] GameObject fog;
    public Node[] room;
    int count = 1;
    public int maxRoom;
    public static int maxRoomSize;
    public static Vector2 bossRoomVec;
    public static Node currentNode = null;
    Vector3 spawn;
    // Start is called before the first frame update
    void Start()
    {
        mapSize = new Vector2Int(100, 100);
        maxRoomSize = Mathf.Max(mapSize.x, mapSize.y);
        maxRoom = (int)Mathf.Pow(2, Depth);
        room = new Node[maxRoom];
        FillBackground();
        Node root = new Node (new RectInt(0, 0, mapSize.x, mapSize.y));
        int randT1 = Random.Range(1, maxRoom - 7);
        int randT2 = Random.Range(maxRoom - 7, maxRoom - 4);
        int randS = randT1;
        while(randT1 == randS || randT2 == randS) {
            randS = Random.Range(1, maxRoom - 4);
        }
        Divide(root, 0, randT1, randT2, randS);
        GenerateRoom(root, 0);
        GenerateLoad(root, 0);
        FillWall();
        moveChar();
        setFog();

    }
    void moveChar() {
        spawn.z = 0;
        if(spawn.x % 2 == 1) {
            spawn.x++;
        }
        if(spawn.y % 2 == 1) {
            spawn.y++;
        }
        player.transform.position = spawn;
    }
    void Divide(Node tree, int n, int randT1, int randT2, int randS)
    {
        if (n == Depth){
            
            tree.nodeNum = count;
            tree.active = false;
            tree.clear = false;
            tree.difficulty = -1;
            count++;
            if (randT1 == tree.nodeNum || randT2 == tree.nodeNum || tree.nodeNum == maxRoom-1) {
                tree.type = 1;
            }
            else if (randS == tree.nodeNum)
            {
                tree.type = 3;
                tree.active = false;
                tree.clear = true;
            }
            else if(tree.nodeNum == maxRoom) {
                tree.type = 2;
            }
            else {
                tree.type = 0;
                tree.difficulty = Random.Range(1, 10);
            }
            
            return;
        }

        int maxLength = Mathf.Max(tree.nodeRect.width, tree.nodeRect.height);
        int split = Mathf.RoundToInt(Random.Range(maxLength * minDivision, maxLength * maxDivision));

        if (tree.nodeRect.width >= tree.nodeRect.height)
        {
            tree.leftNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y, split, tree.nodeRect.height));
            tree.rightNode = new Node(new RectInt(tree.nodeRect.x + split, tree.nodeRect.y, tree.nodeRect.width - split, tree.nodeRect.height));
            //DrawLine(new Vector2(tree.nodeRect.x + split, tree.nodeRect.y), new Vector2(tree.nodeRect.x + split, tree.nodeRect.y + tree.nodeRect.height));
        }
        else
        {
            tree.leftNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y, tree.nodeRect.width, split));
            tree.rightNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y + split, tree.nodeRect.width, tree.nodeRect.height - split));
            //DrawLine(new Vector2(tree.nodeRect.x, tree.nodeRect.y + split), new Vector2(tree.nodeRect.x + tree.nodeRect.width, tree.nodeRect.y + split));
        }
        tree.leftNode.parentNode = tree;
        tree.rightNode.parentNode = tree;
        Divide(tree.leftNode, n + 1, randT1, randT2, randS);
        Divide(tree.rightNode, n + 1, randT1, randT2, randS);
    }
    private RectInt GenerateRoom(Node tree, int n)
    {
        RectInt rect;
        if (n == Depth)
        {
            rect = tree.nodeRect;
            int width = Random.Range(rect.width / 2, rect.width - 1);
            int height = Random.Range(rect.height / 2, rect.height - 1);

            int x = rect.x + Random.Range(1, rect.width - width);
            int y = rect.y + Random.Range(1, rect.height - height);

            rect = new RectInt(x, y, width, height);
            if (tree.type == 3)
            {
                currentNode = tree;
                
                spawn.x = x - mapSize.x/2 + width/2;
                spawn.y = y - mapSize.y/2 + height/2;
            }
            room[tree.nodeNum - 1] = tree;
            FillRoom(rect, tree.type);
        }
        else
        {
            tree.leftNode.roomRect = GenerateRoom(tree.leftNode, n + 1);
            tree.rightNode.roomRect = GenerateRoom(tree.rightNode, n + 1);
            rect = tree.leftNode.roomRect;
        }
        return rect;
    }
   
    private void GenerateLoad(Node tree, int n)
    {
        if (n == Depth) return;

        Vector2Int leftCenter = tree.leftNode.center;
        Vector2Int rightCenter = tree.rightNode.center;
        tree.leftNode.road++;
        tree.rightNode.road++;
        for (int i = Mathf.Min(leftCenter.x, rightCenter.x); i < Mathf.Max(leftCenter.x, rightCenter.x); i++)
        {
            
            tileMap.SetTile(new Vector3Int(i - mapSize.x / 2, leftCenter.y - mapSize.y / 2, 0), roomTile);
            tileMap.SetTile(new Vector3Int(i - mapSize.x / 2, leftCenter.y - mapSize.y / 2-1, 0), roomTile);
        }

        for (int j = Mathf.Min(leftCenter.y, rightCenter.y); j < Mathf.Max(leftCenter.y, rightCenter.y); j++)
        {
            tileMap.SetTile(new Vector3Int(rightCenter.x - mapSize.x / 2, j - mapSize.y / 2, 0), roomTile);
            tileMap.SetTile(new Vector3Int(rightCenter.x - mapSize.x / 2+1, j - mapSize.y / 2, 0), roomTile);
        }
        FillRoom(tree.leftNode.roomRect, tree.leftNode.type);
        FillRoom(tree.rightNode.roomRect, tree.rightNode.type);
        GenerateLoad(tree.leftNode, n + 1);
        GenerateLoad(tree.rightNode, n + 1);
    }
    
    void FillBackground()
    {
        for (int i = -10; i < mapSize.x + 10; i++)
        {
            for (int j = -10; j < mapSize.y + 10; j++)
            {
                tileMap.SetTile(new Vector3Int(i - mapSize.x / 2, j - mapSize.y / 2, 0), outTile);
            }
        }
    }
    void FillWall()
    {
        for (int i = 0; i < mapSize.x; i++) 
        {
            for (int j = 0; j < mapSize.y; j++)
            {
                if (tileMap.GetTile(new Vector3Int(i - mapSize.x / 2, j - mapSize.y / 2, 0)) == outTile)
                {
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            if (x == 0 && y == 0) continue;
                            if (tileMap.GetTile(new Vector3Int(i - mapSize.x / 2 + x, j - mapSize.y / 2 + y, 0)) == roomTile)
                            {
                                tileMap.SetTile(new Vector3Int(i - mapSize.x / 2, j - mapSize.y / 2, 0), wallTile);
                                break;
                            }
                            else if (tileMap.GetTile(new Vector3Int(i - mapSize.x / 2 + x, j - mapSize.y / 2 + y, 0)) == treasureTile)
                            {
                                tileMap.SetTile(new Vector3Int(i - mapSize.x / 2, j - mapSize.y / 2, 0), wallTile);
                                break;
                            }
                            else if (tileMap.GetTile(new Vector3Int(i - mapSize.x / 2 + x, j - mapSize.y / 2 + y, 0)) == BossTile)
                            {
                                tileMap.SetTile(new Vector3Int(i - mapSize.x / 2, j - mapSize.y / 2, 0), wallTile);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
    private void FillRoom(RectInt rect, int type)
    {
        for (int i = rect.x; i < rect.x + rect.width; i++)
        {
            for (int j = rect.y; j < rect.y + rect.height; j++)
            {
                if(type == 0 || type == 3) {
                    tileMap.SetTile(new Vector3Int(i - mapSize.x / 2, j - mapSize.y / 2, 0), roomTile);
                    
                }
                else if(type == 1) { // treasure
                    tileMap.SetTile(new Vector3Int(i - mapSize.x / 2, j - mapSize.y / 2, 0), treasureTile);
                }
                else if(type == 2) { // boss
                    tileMap.SetTile(new Vector3Int(i - mapSize.x / 2, j - mapSize.y / 2, 0), BossTile);
                }
            }
        }
        if(type == 2) {
            bossRoomVec = new Vector2(rect.x - mapSize.x/2 + rect.width / 2, rect.y - mapSize.y/2+rect.height/2);
        }
    }
    private void BlockNode(Node room) {
        RectInt rect = room.nodeRect;
        for (int i = rect.x; i < rect.x + rect.width; i++)
        {
            if(tileMap.GetTile(new Vector3Int(i - mapSize.x / 2, rect.y - mapSize.y / 2, 0)) == roomTile) {
                tileMap.SetTile(new Vector3Int(i - mapSize.x / 2, rect.y - mapSize.y / 2, 0), blockTile);
            }

            if (tileMap.GetTile(new Vector3Int(i - mapSize.x / 2, rect.y - mapSize.y / 2 + rect.height, 0)) == roomTile)
            {
                tileMap.SetTile(new Vector3Int(i - mapSize.x / 2, rect.y - mapSize.y / 2 + rect.height, 0), blockTile);
            }
        }
        for (int i = rect.y; i < rect.y + rect.height; i++)
        {
            if (tileMap.GetTile(new Vector3Int(rect.x - mapSize.x / 2, i - mapSize.y / 2, 0)) == roomTile)
            {
                tileMap.SetTile(new Vector3Int(rect.x - mapSize.x / 2, i - mapSize.y / 2, 0), blockTile);
            }
            if (tileMap.GetTile(new Vector3Int(rect.x - mapSize.x / 2+ rect.width, i - mapSize.y / 2, 0)) == roomTile)
            {
                tileMap.SetTile(new Vector3Int(rect.x - mapSize.x / 2 + rect.width, i - mapSize.y / 2, 0), blockTile);
            }
        }
    }
    private void OpenNode(Node room)
    {
        RectInt rect = room.nodeRect;
        for (int i = rect.x; i < rect.x + rect.width; i++)
        {
            if (tileMap.GetTile(new Vector3Int(i - mapSize.x / 2, rect.y - mapSize.y / 2, 0)) == blockTile)
            {
                tileMap.SetTile(new Vector3Int(i - mapSize.x / 2, rect.y - mapSize.y / 2, 0), roomTile);
            }

            if (tileMap.GetTile(new Vector3Int(i - mapSize.x / 2, rect.y - mapSize.y / 2 + rect.height, 0)) == blockTile)
            {
                tileMap.SetTile(new Vector3Int(i - mapSize.x / 2, rect.y - mapSize.y / 2 + rect.height, 0), roomTile);
            }
        }
        for (int i = rect.y; i < rect.y + rect.height; i++)
        {
            if (tileMap.GetTile(new Vector3Int(rect.x - mapSize.x / 2, i - mapSize.y / 2, 0)) == blockTile)
            {
                tileMap.SetTile(new Vector3Int(rect.x - mapSize.x / 2, i - mapSize.y / 2, 0), roomTile);
            }
            if (tileMap.GetTile(new Vector3Int(rect.x - mapSize.x / 2 + rect.width, i - mapSize.y / 2, 0)) == blockTile)
            {
                tileMap.SetTile(new Vector3Int(rect.x - mapSize.x / 2 + rect.width, i - mapSize.y / 2, 0), roomTile);
            }
        }
    }
    public void setFog() {
        for(int i = 0;i < maxRoom;i++) {
            GameObject f = Instantiate(fog, new Vector3(room[i].nodeRect.x-mapSize.x/2+room[i].nodeRect.width/2, room[i].nodeRect.y - mapSize.y / 2 + room[i].nodeRect.height / 2, 0), Quaternion.identity);
            f.GetComponent<Transform>().localScale = new Vector3(room[i].nodeRect.width-1.5f, room[i].nodeRect.height-0.5f, 0);
            f.GetComponent<BoxCollider2D>().size = new Vector2(f.GetComponent<BoxCollider2D>().size.x-0.1f, f.GetComponent<BoxCollider2D>().size.y-0.1f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Vector2Int playerVec = new Vector2Int((int)player.GetComponent<Transform>().position.x, (int)player.GetComponent<Transform>().position.y);
        for (int i = 0; i < maxRoom; i++)
        {
            if (playerVec.x > room[i].nodeRect.x - mapSize.x / 2 +1 && playerVec.x < room[i].nodeRect.x - mapSize.x / 2 + room[i].nodeRect.width-1)
            {
                if (playerVec.y < room[i].nodeRect.y - mapSize.y / 2 + room[i].nodeRect.height-1 && playerVec.y > room[i].nodeRect.y - mapSize.y / 2+1)
                {
                    currentNode = room[i];
                }
            }
        }
        if(currentNode.clear == false) {
            BlockNode(currentNode);
        }
        if(currentNode.clear == true) {
            OpenNode(currentNode);
        }
       
    }
}
