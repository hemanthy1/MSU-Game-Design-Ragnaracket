using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float timeAlive = 0;

    public float enemyHealth;
    public float enemyMaxHealth = 100;

    private GameObject enemyStructure;

    private GameObject shuttleCock;

    private BoxCollider enemyCollider;

    private BoxCollider shuttleCollider;

    private SliderUI healthBar;

    public LevelEndUI menuUI;

    [SerializeField]
    private float baseDamage = 5f;

    private GameObject player;

    private ShuttlecockMotion projectile;


    // Start is called before the first frame update
    void Start()
    {
        projectile = GameObject.Find("Shuttlecock").GetComponent<ShuttlecockMotion>();
        enemyStructure = GameObject.Find("Castle");
        if (enemyHealth == 0)
        {
            enemyHealth = enemyMaxHealth;
        }
        enemyHealth = enemyMaxHealth;
        shuttleCock = GameObject.Find("Shuttlecock");
        shuttleCollider = shuttleCock.GetComponent<BoxCollider>();
        enemyCollider = GetComponent<BoxCollider>();
        healthBar = GameObject.Find("HealthBar").GetComponent<SliderUI>();
        healthBar.SetMax(enemyMaxHealth);
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        timeAlive += Time.unscaledDeltaTime;
    }

    private void DoDamageToHealth(float damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            enemyHealth = 0;
            enemyStructure.SetActive(false);
            shuttleCock.SetActive(false);
            //Display the win UI
            menuUI.DisplayLevelUI();
            //player.SetActive(false);
            
        }
        healthBar.UpdateValue(enemyHealth);
    }


    void OnTriggerEnter(Collider collision)
    {
        float damage = baseDamage * projectile.GetDamageMultiplier(); //Calculate this later
        //Debug.Log("Hey I'm hitting here");
        DoDamageToHealth(damage);
        Debug.Log("Damage taken: " + damage);
        VolleyManager.instance.AddVolley();
    }
}
