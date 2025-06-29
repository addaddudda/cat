using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if(collision.gameObject.tag == "border")
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("enemy"))
        {
            enemy Enemy = collision.GetComponent<enemy>();
            Enemy.givedamage(1f);
        }
    }
}
