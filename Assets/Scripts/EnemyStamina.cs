using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStamina : MonoBehaviour
{

    public float enemyStamina;
    public float enemyMaxStamina = 100;

    private GameObject enemyStructure;

    private BoxCollider enemyRacketCollider;

    private BoxCollider shuttleCollider;

    private EnemyDefender behaviorScript;

    private SliderUI staminaBar;

    [SerializeField]
    private float baseDamage = 5f;

    private ShuttlecockMotion projectile;

    // Start is called before the first frame update
    void Start()
    {
        projectile = GameObject.Find("Shuttlecock").GetComponent<ShuttlecockMotion>();
        //Replace this line with finding the enemy racket
        //enemyStructure = GameObject.Find("Castle");
        enemyStamina = enemyMaxStamina;
        shuttleCollider = GameObject.Find("Shuttlecock").GetComponent<BoxCollider>();
        enemyRacketCollider = GetComponent<BoxCollider>();
        behaviorScript = GetComponent<EnemyDefender>();
        staminaBar = GameObject.Find("StaminaBar").GetComponent<SliderUI>();
        staminaBar.SetMax(enemyMaxStamina);
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
            enemyStamina = 0;
            behaviorScript.Disable();
            //Destroy enemy racket
        }
        staminaBar.UpdateValue(enemyStamina);
    }

    void OnTriggerEnter(Collider collision)
    {
        float damage = baseDamage * projectile.GetDamageMultiplier(); //Calculate this later

        DoDamageToStamina((float)damage);
    }

    public void RegenStamina()
    {
        enemyStamina = enemyMaxStamina;
        staminaBar.UpdateValue(enemyStamina);
    }
}
