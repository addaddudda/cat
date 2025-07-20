using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemybullet : MonoBehaviour
{
    public GameObject character;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("character"))
        {
            character charactersc = character.GetComponent<character>();
            if(charactersc.parry == false)
            {
                charactersc.givedamage(0.5f);
            }
            else if(charactersc.parry == true) 
            {
                charactersc.health += 3;
            }
            
        }
        if (collision.CompareTag("border"))
        {
            Destroy(gameObject);
        }
    }
}
