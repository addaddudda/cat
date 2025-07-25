using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float health = 10f;
    public void givedamage(float damage)
    {
        for(int i = 0; i < 3; i++)
        {
            health -= damage;
        }
        
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        
    }

}
