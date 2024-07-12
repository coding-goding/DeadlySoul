using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class enemy
{
    // Start is called before the first frame update
    public int type;
    public int health;
    public int attack;
    public int speed;
    public bool alive;
    public int cost;
    public GameObject mob;
    public enemy(int type)
    {
        if(type == 1) {
            this.type = 1;
            this.health = 30;
            this.attack = 5;
            this.speed = 5;
            this. cost = 1;
            this.alive = true;
        }
        else if (type == 2)
        {
            this.type = 2;
            this.health = 1;
            this.attack = 7;
            this.speed = 10;
            this.cost = 2;
            this.alive = true;
        }
        else if (type == 3)
        {
            this.type = 3;
            this.health = 20;
            this.attack = 10;
            this.speed = 3;
            this.cost = 2;
            this.alive = true;
        }
        else if (type == 4)
        {
            this.type = 4;
            this.health = 500;
            this.attack = 10;
            this.speed = 0;
            this.cost = 1;
            this.alive = true;
        }
        else if (type == 5)
        {
            this.type = 5;
            this.health = 1;
            this.attack = 20;
            this.speed = 3;
            this.cost = 1;
            this.alive = true;
        }
    }
}
