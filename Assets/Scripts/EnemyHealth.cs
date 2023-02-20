using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth;
    public float enemyMaxHealth = 100;

    private GameObject enemyStructure;

    private GameObject shuttleCock;

    private BoxCollider enemyCollider;

    private BoxCollider shuttleCollider;


    // Start is called before the first frame update
    void Start()
    {
        enemyStructure = GameObject.Find("Castle");
        if (enemyHealth == 0)
        {
            enemyHealth = enemyMaxHealth;
        }
        enemyHealth = enemyMaxHealth;
        shuttleCock = GameObject.Find("Shuttlecock");
        shuttleCollider = shuttleCock.GetComponent<BoxCollider>();
        enemyCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DoDamageToHealth(float damage)
    {
        
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            Destroy(enemyStructure);
            Destroy(shuttleCock);
        }
    }


    void OnTriggerEnter(Collider collision)
    {
        float damage = 5f; //Calculate this later
        Debug.Log("Hey I'm hitting here");
        DoDamageToHealth(damage);
        
    }
}
