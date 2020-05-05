using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    private float waitTime;
    public float startTime;

    public AudioClip playerDamage;
    public AudioClip enemyDamage;
    private GameObject[] enemies;
    public LayerMask enemy;
    public Text healthText;
    public Image healthbar;
    public int damage = 5;
    private int maxHealth = 50;
    public int health = 50;
    public int killCount = 0;

    void Start()
    {
            healthText.text = "" + maxHealth;
       
    }

    void Update()
    {
        // If-Else to ensure user isn't spamming attack
        if (waitTime <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                GetComponent<AudioSource>().PlayOneShot(enemyDamage);
                // Get all instances of enemy objects
                enemies = GameObject.FindGameObjectsWithTag("Enemy");

                // Check if any enemies are within attacking distance
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (Vector3.Distance(enemies[i].transform.position, transform.position) < 10)
                    {
                        // Access TakeDamage function in Enemy class
                        enemies[i].GetComponent<Enemy>().TakeDamage(damage);
                    }
                }
            }
            waitTime = startTime;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }


        // Check if Player dies or wins
        if (health <= 0)
        {
            // Lose Game
            SceneManager.LoadScene("Lose");
        }
        else if (killCount >= 9)
        {
            // Win Game
            SceneManager.LoadScene("Win");
        }
    }

    // Take damage from enemy
    public void TakeDamage(int inDamage)
    {
        GetComponent<AudioSource>().PlayOneShot(playerDamage);
        health -= inDamage;
        healthbar.fillAmount = ((float)health / (float)maxHealth);
        transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
    }

    public void AddKill()
    {
        resizeArray();
        killCount++;
        damage += 1;
        maxHealth += 5;
        health = maxHealth;
        healthbar.fillAmount = ((float)health / (float)maxHealth);
        healthText.text = "" + maxHealth;
    }

    private void resizeArray()
    {
        for (int i = 0; i + 1 < enemies.Length; i++)
        {
            enemies[i] = enemies[enemies.Length - 1];
        }
        System.Array.Resize(ref enemies, enemies.Length - 1);
    }
}
