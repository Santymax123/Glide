using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    private int health;

    public int maxArmour;
    private int armour;



    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightShift))
        {
            DamagePlayer(30);
            Debug.Log("Player Damaged");
        }
    }


    public void DamagePlayer(int damage)
    {
        if (armour > damage)
        {
            armour -= damage;
        }
        else if (armour != 0)
        {
            damage -= armour;
            armour = 0;
            health -= damage;
        }
        else
        {
            health -= damage;
        }

        if (health <= 0)
        {
            Debug.Log("Player Died");

            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }
        
    }

    public void GiveHealth(int amount, GameObject pickup)
    {
        if (health < maxHealth)
        {
            health += amount;
            Destroy(pickup);
        }

        
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void GiveArmour(int amount, GameObject pickup)
    {
        if (armour < maxArmour)
        {
            armour += amount;
            Destroy(pickup);
        }
        
        if (armour > maxArmour)
        {
            armour = maxArmour;
        }
    }


    public int GetArmour()
    {
        return armour;
    }

    public int GetHealth()
    {
        return health;
    }
}
