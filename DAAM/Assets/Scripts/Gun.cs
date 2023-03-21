using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float range = 20f;           // Rango horizontal escopeta
    public float verticalRange = 20f;   // " Vertical
    public float fireRate;              

    public float damage = 2f;           // Daño escopeta
    public float gunShotRadius = 20f;   // Radio del ruido producido por la escopeta

    public int maxAmmo;
    private int ammo;

    private float nextTimeToFire;       
    private BoxCollider gunTrigger;     // Box collider que determina el "area de acción" de la escopeta

    public LayerMask raycastLayerMask;
    public LayerMask enemyLayerMask;
    public EnemyManager enemyManager;   // Lista de enemigos a dañar (los que son apuntados)

    // Start is called before the first frame update
    void Start()
    {
        gunTrigger = GetComponent<BoxCollider>();
        gunTrigger.size = new Vector3(1, verticalRange, range);
        gunTrigger.center = new Vector3(0, 0, range * 0.5f);
        ammo = 15;
        CanvasManager.Instance.updateAmmo(ammo);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextTimeToFire && ammo > 0)
        {
            Fire();
        }
    }

    void Fire()
    {
        // Alerta enemigos en rango de escucha
        Collider[] enemyColliders;
        enemyColliders = Physics.OverlapSphere(transform.position, gunShotRadius, enemyLayerMask);
        foreach (var enemyCollider in enemyColliders)
        {
            enemyCollider.GetComponent<EnemyAwareness>().isAggro = true;
        }

        // Efecto de sonido Escopeta
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();

        foreach (var enemy in enemyManager.enemiesInTrigger)
        {
            if (enemy != null)
            {
                var dir = enemy.transform.position - transform.position;

                RaycastHit hit;
                if (Physics.Raycast(transform.position, dir, out hit, range * 1.5f, raycastLayerMask))
                {
                    if (hit.transform == enemy.transform)
                    {

                        float distance = Vector3.Distance(enemy.transform.position, transform.position);
                        if (distance > range * 0.5f)
                        {
                            enemy.takeDamage(damage * 0.5f);
                        }
                        else
                        {
                            enemy.takeDamage(damage);
                        }
                        

                        Debug.DrawRay(transform.position, dir, Color.blue);
                        //Debug.Break();
                    }
                }
            }
            
        }
        nextTimeToFire = Time.time + fireRate;
        ammo--;
        CanvasManager.Instance.updateAmmo(ammo);
    }

    // Método para reabastecer al jugador
    public void GiveAmmo(int amount, GameObject pickup)
    {
        if (ammo < maxAmmo)
        {
            ammo += amount;
            Destroy(pickup);
        }
        

        if (ammo >= maxAmmo)
        {
            ammo = maxAmmo;
        }
        CanvasManager.Instance.updateAmmo(ammo);
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.transform.GetComponent<Enemy>();
        if (enemy)
        {
            enemyManager.AddEnemy(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.transform.GetComponent<Enemy>();
        if (enemy)
        {
            enemyManager.RemoveEnemy(enemy);
        }
    }
}
