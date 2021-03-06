﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {


    private Player player;
    private bool aggro = false;
    private BoxCollider2D boxCollider;
    public LayerMask blockinglayer;
    public float speed = 1f;
    private Transform target;
    public float health = 50f;
    private bool isPlayer = true;
    private Animator anim;
    private float timeleft = 0f;
    public Image EnemyHealth;


	void Start ()
    {
        player = GetComponent<Player>();
        boxCollider = GetComponent<BoxCollider2D>();
        transform.localScale -= new Vector3(0.92f, 0.92f, 0f);
        anim = GetComponent<Animator>();
        EnemyHealth.enabled = true;
    }
	
    //calculez distanta fata de player
	float distance()
    {
        float dist = Vector3.Distance(transform.position, Player.position);
        return dist;
    }
        

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("TriggerEnter");
           // isPlayer = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {

            Debug.Log("TriggerExit");
            isPlayer = true;
    }

    //atac player
    private void attack()
    {
       Player.player_health -= 10;
        Debug.Log(Player.player_health);
    }
    //verific daca se poate misca

    //verific daca are aggro si il fac sa urmareasca playerul
	void Update () 
    {
        EnemyHealth.rectTransform.position = new Vector3(transform.position.x, transform.position.y + 40, 0);
        EnemyHealth.fillAmount = health / 50;
        timeleft -= Time.deltaTime;
        RaycastHit2D hit;
        if (distance() <= 3f)
        {
            aggro = true;
            anim.SetBool("Aggro", true);
        }
        if(aggro)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
            if (distance() >= 1f)
                transform.position = Vector3.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);

            if(distance() <= 1f)
            {
                if (timeleft <= 0)
                {
                    attack();
                    timeleft = 5f;
                }
            }
        }
	}
}
