using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;

public class nonbulletenemy : MonoBehaviour
{
    public LayerMask characterly;
    public GameObject character;
    public float followingtime = 0;
    float followingcooldown;
    bool isattack = false;

    Animator anim;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    character charactercs;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
                isattack = true;
                charactercs.givedamage(2);
                Debug.Log(charactercs.health);
            }
        }
        else if (!overlap)
        {
            anim.SetBool("isattack", false);
            isattack=false;
        }
    }
    private void Start()
    {
        InvokeRepeating("attack", 0f, 0.8f);
    }
    void ai()
    {
        if (followingtime >= 5)
        {
            rigid.velocity = Vector2.zero;
            followingcooldown += Time.fixedDeltaTime;
            if (followingcooldown >= 3)
            {
                followingtime = 0;
                followingcooldown = 0;
            }
            return;
        }

        var overlap = Physics2D.OverlapCircle(transform.position, 5f, characterly);

        if (overlap && isattack == false)
        {
            Vector2 dir = (character.transform.position - transform.position).normalized;
            Debug.Log(dir);
            float threshold = 0.9f;
            bool currentFlip = spriteRenderer.flipX;
            
            
            if (dir.x > threshold && currentFlip != true)
            {
                spriteRenderer.flipX = true;
            }
            else if (dir.x < -threshold && currentFlip != false)
            {
                spriteRenderer.flipX = false;
 
            }
            if (isattack == false) 
            {
                charactercs.makeanimfalse("iswalk");
                anim.SetBool("iswalk", true);
            }
            Vector2 nextPos = new Vector2(rigid.position.x + dir.x * 2 * Time.fixedDeltaTime, rigid.position.y);
            rigid.MovePosition(nextPos);
        }
        else
        {
            rigid.velocity = Vector2.zero;
            charactercs.makeanimfalse("iswalk");
            anim.SetBool("iswalk", false);
        }
    }

    private void FixedUpdate()
    {
        ai();

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
    private void Update()
    {
        if (rigid.velocity.y <= 2f && anim.GetBool("isattack") == false)
        {
            anim.SetBool("isattack", false);
        }
        
    }
}
