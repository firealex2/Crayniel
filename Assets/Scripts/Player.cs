using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{


    public float speed = 1f;
    private BoxCollider2D boxcollider;
    //public BoardManager boardmanager;
    public LayerMask blockinglayer;
    public bool gasit = true;
    //  public Loader loader;
    public float restartLevelDelay = 0f;
    public static Vector3 position;
    public static Transform playerTransform;
    public BoardManager boardscript;
    private float damage = 10f;
    private static Vector3 pos;
    


    // Use this for initialization
    void Awake()
    {
        boxcollider = GetComponent<BoxCollider2D>();
        if (pos.x == -1)
            pos.x = 10;
        else if (pos.x == 11)
            pos.x = 0;
        if (pos.y == -1)
            pos.y = 10;
        else if (pos.y == 11)
            pos.y = 0;
        transform.position = pos;
        position = transform.position;
        transform.localScale -= new Vector3(0.95f, 0.95f, 0f);
        playerTransform = transform;
    }
   
    //vad daca se poate misca
    private bool move(float dx, float dy, out RaycastHit2D hit)
    {
        Vector3 start = transform.position;
        Vector3 end = start + new Vector3(dx, dy, 0.0f);
        boxcollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockinglayer);
        boxcollider.enabled = true;
        if (hit.transform == null)
            return true;
        return false;

    }

    // gasesc cel mai apropiat enemy (target pt attack)
    GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject enemy in gos)
        {
            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = enemy;
                distance = curDistance;
            }
        }
        return closest;
    }

    //functia de attack
    private void attack()
    {
        RaycastHit2D hit;
        GameObject target = null;
        if(BoardManager.enemyCount!=0)
            target = FindClosestEnemy();
        if (target != null )
        {
            if (Vector3.Distance(target.transform.position, transform.position) <= 1f)
            {

                Enemy monster = target.GetComponent<Enemy>();
                monster.health -= 10;
                if (monster.health <= 0)
                {
                    Destroy(monster.gameObject);
                    BoardManager.enemyCount--;
                }

            }
        }
        else BoardManager.enemyCount = 0;
 
    }

    //se misca sau ataca
    void Update()
    {

        RaycastHit2D hit;
        if (Input.GetKey(KeyCode.D))
        {
            if (move(speed * Time.deltaTime, 0.0f, out hit) == true)
                transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
            position = transform.position;
            playerTransform = transform;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (move(-speed * Time.deltaTime, 0.0f, out hit))
                transform.position -= new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
            position = transform.position;
            playerTransform = transform;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            if (move(0.0f, speed * Time.deltaTime, out hit))
                transform.position += new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
            position = transform.position;
            playerTransform = transform;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (move(0.0f, -speed * Time.deltaTime, out hit))
                transform.position -= new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
            position = transform.position;
            playerTransform = transform;
        }
        else if (Input.GetKey(KeyCode.Space))
            attack();
    }

    //daca se apropie de exit
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        
        if (other.tag == "Exit" && BoardManager.enemyCount==0)//daca nu mai sunt monstrii in camera atunci poate sa treaca prin exit
        {
            pos = other.transform.position;
            GameManager.level++;
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
    }

    //reface levelul
    private void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    // Update is called once per frame
}
