using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStamina : MonoBehaviour
{

    public float enemyStamina;
    public float enemyMaxStamina = 100;

    private GameObject enemyStructure;

    private BoxCollider enemyRacketCollider;

    private BoxCollider shuttleCollider;

    // Start is called before the first frame update
    void Start()
    {
        //Replace this line with finding the enemy racket
        //enemyStructure = GameObject.Find("Castle");
        enemyStamina = enemyMaxStamina;
        shuttleCollider = GameObject.Find("Shuttlecock").GetComponent<BoxCollider>();
        enemyRacketCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DoDamageToStamina(float damage)
    {

        enemyStamina -= damage;

        if (enemyStamina <= 0)
        {
            //Destroy enemy racket
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        float damage = 5f; //Calculate this later

        DoDamageToStamina((float)damage);
    }
}
