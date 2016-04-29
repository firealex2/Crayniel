using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

    public int i;
    private Image Health;
	// Use this for initialization
	void Start () {
        Health = GetComponent<Image>();
	}
	
    void setHealth()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length!=0)
        {
            if(i<enemies.Length)
            {
                Debug.Log(enemies.Length);
                GameObject target = enemies[i];
                Enemy monster = target.GetComponent<Enemy>();
                Health.fillAmount = monster.health / 50;
                Health.rectTransform.position = new Vector3(monster.transform.position.x, monster.transform.position.y+0.9f, 0);
            }
        }
    }
	// Update is called once per frame
	void Update () {
        setHealth();
	}
}
