using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            Debug.Log("Take " + damage + " Dmg, health now at " + health);
            if (health <= 0)
            {
                sprite.color = Color.magenta;
            }
        }
    }
}
