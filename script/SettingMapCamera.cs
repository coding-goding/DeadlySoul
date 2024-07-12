using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMapCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerIcon;
    public GameObject player;
    GameObject p;
    void Start()
    {
        gameObject.GetComponent<Transform>().position = new Vector3(0, 0, -10);
        p = Instantiate(playerIcon, player.GetComponent<Transform>().position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Camera>().orthographicSize = MapGenerator.maxRoomSize / 2 + 5;
        if(gameObject.GetComponent<Camera>().enabled == true) {
           p.GetComponent<Transform>().position = player.GetComponent<Transform>().position;
            
        }
    }
}
