using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemybullet : MonoBehaviour
{
    public GameObject character;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        character charactersc = character.GetComponent<character>();
        if (collision.CompareTag("enemy") && charactersc.parry == true)
        {
            enemy enemysc = GetComponent<enemy>();
            enemysc.givedamage(1.5f);
        }
        
        if (collision.CompareTag("character"))
        {
            
            if(charactersc.parry == false)
            {
                charactersc.givedamage(0.5f);
            }
            else if(charactersc.parry == true) 
            {
                Destroy(gameObject);
                charactersc.fire();
            }
            
        }
        if (collision.CompareTag("border"))
        {
            Destroy(gameObject);
        }
    }
}
