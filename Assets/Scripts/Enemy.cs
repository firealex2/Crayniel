using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {


    private Player player;
    private bool aggro = false;
    private BoxCollider2D boxCollider;
    public LayerMask blockinglayer;
    public float speed = 1f;
    private Transform target;
    public float health = 50f;


	void Start ()
    {
        player = GetComponent<Player>();
        boxCollider = GetComponent<BoxCollider2D>();
   	}
	
    //calculez distanta fata de player
	float distance()
    {
        float dist = Vector3.Distance(transform.position, Player.position);
        return dist;
    }
        
    //verific daca se poate misca
    private bool move(float dx, float dy, out RaycastHit2D hit)
    {
        Vector3 start = transform.position;
        Vector3 end = start + new Vector3(dx, dy, 0.0f);
        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockinglayer);
        boxCollider.enabled = true;
        if (hit.transform == null)
            return true;
        return false;

    }

    //verific daca are aggro si il fac sa urmareasca playerul
	void Update () 
    {
        RaycastHit2D hit;
        if (distance() <= 3f)
            aggro = true;
        if(aggro)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
           // if (move(pos.x, pos.y, out hit))    
            transform.position = Vector3.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
        }
	}
}
