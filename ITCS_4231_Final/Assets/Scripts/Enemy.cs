using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int damage;
    private float waitTime;
    public float startTime;
    private GameObject[] player;
    public Image healthbar;
    
    void Start()
    {
        // Get player object
        player = GameObject.FindGameObjectsWithTag("Player");
        health = maxHealth;
    }
    
    void Update()
    {
        // If-Else to have enemy attack at intervals
        if (waitTime <= 0)
        {
            // Check if the player is within attacking distance of the enemies
            if (Vector3.Distance(transform.position, player[0].transform.position) < 9)
            {
                // Randomly Access TakeDamage function in PlayerAttack class
                System.Random rnd = new System.Random();
                // 75% hit chance
                if (rnd.Next(1,5) % 3 != 0)
                {
                    player[0].GetComponent<PlayerAttack>().TakeDamage(damage);
                }
            }

            waitTime = startTime;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }

        // Check if Enemy dies
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        player[0].GetComponent<PlayerAttack>().AddKill();
        Debug.Log("addkill");
        //yield return new WaitForSeconds(1);
    }

    // Take damage from player
    public void TakeDamage(int inDamage)
    {
        health -= inDamage;
       
        healthbar.fillAmount = ((float)health / (float)maxHealth);
        transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
       
        transform.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
    }
}
