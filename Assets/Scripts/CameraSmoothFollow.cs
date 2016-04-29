using UnityEngine;
using System.Collections;

public class CameraSmoothFollow : MonoBehaviour
{

    //public Transform player;
    // Use this for initialization
    private Transform player = Player.playerTransform;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
            player = Player.playerTransform;
        if(player)
            transform.position = new Vector3(player.position.x, player.position.y, -1);
    }
}