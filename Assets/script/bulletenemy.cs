using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletenemy : MonoBehaviour
{
    float random = 0;
    public GameObject enemybulletpre;
    public GameObject character;
    void random_enemy()
    {
        random = Random.Range(1, 4);
        Debug.Log(random);
    }
    private void Start()
    {
        InvokeRepeating("random_enemy", 0f, 0.5f);
    }
    private void Update()
    {
        if (random == 2)
        {
            GameObject enemybullet = Instantiate(enemybulletpre, transform.position, Quaternion.identity);
            Vector2 dir = (character.transform.position - transform.position).normalized;
            Rigidbody2D bulletrb = enemybullet.GetComponent<Rigidbody2D>();
            bulletrb.AddForce(dir * 30f, ForceMode2D.Impulse);
            random = 0;
        }
    }
}
