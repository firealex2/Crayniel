using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {


    private Image health;

	// Use this for initialization
	void Start () {
        health = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        health.fillAmount = Player.player_health / 100;
	}
}
