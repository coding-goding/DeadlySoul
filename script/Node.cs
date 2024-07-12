using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Node leftNode;
    public Node rightNode;
    public Node parentNode;
    public int road;
    public int type;
    public int nodeNum;
    public int difficulty;
    public int remainEnemy;
    public bool clear;
    public bool active;
    public RectInt nodeRect;
    public RectInt roomRect;
    public Vector2Int center {
        get{
            return new Vector2Int(roomRect.x+roomRect.width/2, roomRect.y+roomRect.height/2);
        }
    }
    public Node(RectInt rect) {
        this.nodeRect = rect;
    }
}
