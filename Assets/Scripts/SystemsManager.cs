using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemsManager : MonoBehaviour
{
    public float enemyhealth;
    public float enemyMaxHealth = 100;
    public float enemyMinHealth;

    public float enemystamina;
    public float enemyMaxStamina = 100;

    private GameObject enemyStructure;

    private BoxCollider enemyCollider;

    private BoxCollider shuttleCollider;


    // Start is called before the first frame update
    void Start()
    {
        enemyStructure = GameObject.Find("Castle");
        enemyhealth = enemyMaxHealth;
        shuttleCollider = GameObject.Find("Shuttlecock").GetComponent<BoxCollider>();
        enemyCollider = GameObject.Find("DefenderPlane").GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DoDamage(float damage)
    {
        
        enemyhealth -= damage;

        if (enemyhealth <= 0)
        {
            Destroy(enemyStructure);
        }
    }
}
