using UnityEngine;
using System.Collections;

public class CameraSmoothFollow : MonoBehaviour
{

    public Transform player;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x + 0, player.position.y + 0, -1);
    }
}