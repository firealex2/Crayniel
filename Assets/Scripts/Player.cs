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
    public float restartLevelDelay = 1f;


    // Use this for initialization
    void Start()
    {
        boxcollider = GetComponent<BoxCollider2D>();
    }

   

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
    void Update()
    {

        RaycastHit2D hit;
        if (Input.GetKey(KeyCode.D))
            if (move(speed * Time.deltaTime, 0.0f, out hit) == true)
                transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
        if (Input.GetKey(KeyCode.A))
            if (move(-speed * Time.deltaTime, 0.0f, out hit))
                transform.position -= new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
        if (Input.GetKey(KeyCode.W))
            if (move(0.0f, speed * Time.deltaTime, out hit))
                transform.position += new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
        if (Input.GetKey(KeyCode.S))
            if (move(0.0f, -speed * Time.deltaTime, out hit))
                transform.position -= new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
    }

    private void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    // Update is called once per frame
}
