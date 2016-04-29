using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    //animations
    bool move_r;
    bool move_l;
    bool move_b;
    bool move_f;

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
    private Animator animator;


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
        transform.localScale -= new Vector3(0.92f, 0.92f, 0f);
        playerTransform = transform;
        animator = GetComponent<Animator>();
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

        //move_r
        if (Input.GetKeyDown(KeyCode.D))
        {
            move_r = true;
            animator.SetBool("t_idler", true);
        }
            
                if (Input.GetKeyUp(KeyCode.D))
                {
                    move_r = false;
                    animator.SetBool("t_idler", false);
                }
                        if(move_r==true)
                        {
                            animator.Play("PlayerMove_r");
                            if (move(speed * Time.deltaTime, 0.0f, out hit) == true)
                                transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
                            position = transform.position;
                            playerTransform = transform;
                        }
                
        //move_l
        if (Input.GetKeyDown(KeyCode.A))
        {
            move_l = true;
            animator.SetBool("t_idlel", true);
        }
                if (Input.GetKeyUp(KeyCode.A))
                {
                    move_l = false;
                    animator.SetBool("t_idlel", false);
                }
                        if(move_l==true)
                        {
                            animator.Play("PlayerMove_l");
                            if (move(-speed * Time.deltaTime, 0.0f, out hit))
                                transform.position -= new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
                            position = transform.position;
                            playerTransform = transform;
                        }

      //move_b
      if (Input.GetKeyDown(KeyCode.W))
      {
          move_b = true;
          animator.SetBool("t_idleb", true);
      }
            if (Input.GetKeyUp(KeyCode.W))
            {
                move_b = false;
                animator.SetBool("t_idleb", false);
            }
                    if(move_b==true)
                    {       
                            //daca nu se misca nici in dreapta nici in stanga
                            if(move_r==false && move_l==false)
                            animator.Play("PlayerMove_b");

                            if (move(0.0f, speed * Time.deltaTime, out hit))
                                transform.position += new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
                            position = transform.position;
                            playerTransform = transform;
                    }
        //move_f
        if (Input.GetKeyDown(KeyCode.S))
        {
            move_f = true;
            animator.SetBool("t_idlef", true);
        }
            if(Input.GetKeyUp(KeyCode.S))
            {
                move_f = false;
                animator.SetBool("t_idlef", false);
            }
                if(move_f==true)
                {
                    //daca nu se misca nici in dreapta nici in stanga
                    if (move_r == false && move_l == false)
                    animator.Play("PlayerMove_f");

                    if (move(0.0f, -speed * Time.deltaTime, out hit))
                        transform.position -= new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
                    position = transform.position;
                    playerTransform = transform;
                }

        if (Input.GetKey(KeyCode.Space))
        {
            //anulare animatii
            move_b = false; move_f = false; move_l = false; move_r = false;
            animator.SetBool("t_idler", false); animator.SetBool("t_idlel", false); animator.SetBool("t_idleb", false); animator.SetBool("t_idlef", false);

            animator.Play("PlayerAtac_r");
            attack();
        }
            
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
