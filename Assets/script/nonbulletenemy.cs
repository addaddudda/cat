using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class nonbulletenemy : MonoBehaviour
{
    public LayerMask characterly;
    public GameObject character;

    Animator anim;
    Rigidbody2D rigid;

    character charactercs;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        charactercs = character.GetComponent<character>();
    }
    void attack()
    {
        var overlap = Physics2D.OverlapCircle(transform.position, 1f, characterly);
        if (overlap)
        {
            character charactercs = overlap.GetComponent<character>();
            if (charactercs != null)
            {
                anim.SetBool("isattack", true);
                charactercs.givedamage(2);
                Debug.Log(charactercs.health);
            }
        }
        else if (!overlap)
        {
            anim.SetBool("isattack", false);
        }
    }
    private void Start()
    {
        InvokeRepeating("attack", 0f, 0.8f);
    }
    private void Update()
    {
        if (rigid.velocity.y <= 2f && anim.GetBool("isattack") == false)
        {
            anim.SetBool("isattack", false);
        }
    }
}
