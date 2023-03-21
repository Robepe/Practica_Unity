using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    public bool isHealth;   // Comprueba si el Pick Up es de Salud
    public bool isAmmo;     // Comprueba si el Pick Up es de Munición
    public int amount;      // Cantidad que otorga

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isHealth)
            {
                other.GetComponent<PlayerHealth>().GiveHealth(amount, this.gameObject);
            }
            if (isAmmo)
            {
                other.GetComponentInChildren<GunController>().GiveAmmo(amount, this.gameObject);
            }
        }
    }
}
