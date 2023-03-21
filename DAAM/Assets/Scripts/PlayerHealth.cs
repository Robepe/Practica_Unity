using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth;   // Salud Máxima
    private int health;     // Salud Actual

    public AudioClip damagedSound, deadSound;
    private AudioSource audio;


    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        health = maxHealth; // Empezamos con toda la Salud
        CanvasManager.Instance.updateHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        // PRUEBA
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            DamagePlayer(30);
            Debug.Log("Player Damaged");
        }
    }

    // Método para dañar al jugador
    public void DamagePlayer(int damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            Debug.Log("GAME OVER");
            audio.clip = deadSound;
            audio.Stop();
            audio.Play();
        }
        else
        {
            audio.clip = damagedSound;
            audio.Stop();
            audio.Play();
        }
        CanvasManager.Instance.updateHealth(health);
    }

    // Método para curar al jugador
    public void GiveHealth(int amount, GameObject pickup)
    {
        if (health < maxHealth)
        {
            health += amount;
            Destroy(pickup);
        }
        

        if (health >= maxHealth)
        {
            health = maxHealth;
        }
        CanvasManager.Instance.updateHealth(health);
    }
}
