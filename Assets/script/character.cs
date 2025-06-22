using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class character : MonoBehaviour
{
    public float jumppower;
    public float maxspeed;
    public GameObject bulletpre;
    public Camera cam;
    public LayerMask enemyLayerMask;

    float h;
    bool jumped;
    bool dashed;
    bool candash = true;
    bool ismkbread = false;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriterenderer;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }


    //점프
    void jump()
    {
       

        if (jumped == true && rigid.velocity.y == 0)
        {
            rigid.AddForce(Vector2.up * jumppower, ForceMode2D.Impulse);
            jumped = false;
        }
        if (rigid.velocity.y < 0)
        {
            rigid.gravityScale = 3f;
        }
        else if (rigid.velocity.y >= 0)
        {
            rigid.gravityScale = 2.0f;
        }
    }

    //대쉬
    IEnumerator dash()
    {
        foreach(var param in anim.parameters)
        {
            if(param.type == AnimatorControllerParameterType.Bool && param.name != "isdash")
            {
                anim.SetBool(param.name, false);
            }
        }
        anim.SetBool("isdash", true);
        float locate = 0;

        float rigid_buho = Mathf.Sign(rigid.velocity.x);
        if (rigid_buho > 0)
        {
            locate = 1;

        }else if(rigid_buho < 0)
        {
            locate = -1;
        }
        rigid.velocity = new Vector2(rigid.velocity.x, -2f);
        

        for(int i = 0; i < 5; i++)
        {
            if (locate == 1)
            {
                rigid.MovePosition(rigid.position + Vector2.right);
                yield return new WaitForSeconds(0.03f);
            }else if(locate == -1)
            {
                rigid.MovePosition(rigid.position + Vector2.left);
                yield return new WaitForSeconds(0.03f);
            }

        }
        yield return new WaitForSeconds(0.2f);

        candash = false;
        anim.SetBool("isdash", false);
        
        
    }
    //대쉬 기다림
    IEnumerator candash_fn()
    {
        yield return new WaitForSeconds(1.5f);
        candash = true;
    }
    //총 발사
    void fire()
    {
        Vector3 mouselocate = cam.ScreenToWorldPoint(Input.mousePosition);
        mouselocate.z = 0f;
        Vector2 dir = (mouselocate - transform.position).normalized;
        GameObject bullet = Instantiate(bulletpre, transform.position, Quaternion.identity);
        Rigidbody2D rigidbullet = bullet.GetComponent<Rigidbody2D>();
        rigidbullet.AddForce(dir * 30f, ForceMode2D.Impulse);
        
        
    }
    IEnumerator mkbread()
    {
        Collider2D[] mkbreadoverlap = Physics2D.OverlapCircleAll(transform.position, 3f, enemyLayerMask);
        Debug.Log(mkbreadoverlap);
        foreach (Collider2D hits in mkbreadoverlap) { 
            enemy enemysc = hits.GetComponent<enemy>();
            if (enemysc != null) {
                enemysc.givedamage(0.3f);
                Debug.Log(enemysc.health);
            }
        }
        yield return new WaitForSeconds(0.3f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(ismkbread == true)
        {
            Gizmos.DrawWireSphere(transform.position, 3f);
        }
    }


    void FixedUpdate()
    {

        if (ismkbread == false) {
            rigid.velocity = new Vector2(maxspeed * h, rigid.velocity.y);
        }
        //점프
        if(jumped == true)
        {
            jump();
            jumped = false;
        }

        //대쉬
        if(dashed == true && candash == true)
        {
            StartCoroutine(dash());
            StartCoroutine(candash_fn());
            dashed = false;
        }
        
    }

    //update
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fire();
        }

        if(anim.GetBool("isdash") == false)
        {
            anim.SetBool("iswalk", Mathf.Abs(rigid.velocity.x) > 0.1f && rigid.velocity.y == 0);
            anim.SetBool("isjump", rigid.velocity.y != 0);
            
        }

        h = Input.GetAxisRaw("Horizontal");

        
        if (h != 0)
        {
            spriterenderer.flipX = h > 0;
        }
        if (Input.GetKey(KeyCode.E))
        {
            mkbread();
            ismkbread = true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            ismkbread = false;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            jumped = true;

        }
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) {
            if (candash == true) {
                dashed = true;
            }
        }
    }
}
