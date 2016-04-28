using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{

    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public float rows = 11;
    public float columns = 11;
    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject[] outerWallTiles;
    public GameObject[] enemyTiles;
    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();
    public static int enemyCount;

    //initializez lista cu coordonatele mapului
    void InitialiseList()
    {
        gridPositions.Clear();
        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }


    //pun pe margini wall-uri si in rest floor tile-uri
    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < columns; x++)
        {
            for (int y = -1; y < rows; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                if (x == -1 || x == columns-1 || y == -1 || y == rows-1)
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);

            }
        }
    }


    //creez o pozitie random pt spawn
    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    //functia care plaseaza obiecte pe pozitii random
    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum);
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    //fac scena
    public void SetupScene(int level)
    {
        BoardSetup();
        InitialiseList();
        enemyCount = (int)Mathf.Log(level, 2f);
        LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);
        Instantiate(exit, new Vector3(11f, 5f, 0f), Quaternion.identity);//plasez exiturile
        Instantiate(exit, new Vector3(-1f, 5f, 0f), Quaternion.identity);
        Instantiate(exit, new Vector3(5f, 11f, 0f), Quaternion.identity);
        Instantiate(exit, new Vector3(5f, -1f, 0f), Quaternion.identity);
    }



    // Update is called once per frame
    void Update()
    {

    }
}
