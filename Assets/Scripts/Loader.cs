using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour
{


    public GameObject gameManager;
    // Use this for initialization
    //scriptul care incarca jocul (atribuit la main camera)
    void Awake()
    {
        if (GameManager.instance == null)
            Instantiate(gameManager);
    }

}
