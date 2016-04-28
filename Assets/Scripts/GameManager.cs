using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    public BoardManager boardScript;
    public static int level = 1;

    //initializez jocul
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }


    //accesez boardmanager-ul care spawneaza mapul
    void InitGame()
    {
        boardScript.SetupScene(level);
    }

    //daca game over 
    public void GameOver()
    {
        enabled = false;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
