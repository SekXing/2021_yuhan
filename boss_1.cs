using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_1 : MonoBehaviour
{
    public GameMenager gameMenager;
    public GameObject player;
    public GameObject att_1_1;
    public GameObject att_1_2;
    public GameObject att_1_3;
    public GameObject att_1_4;
    public GameObject att_2_1;
    public GameObject att_2_2;
    public GameObject att_3;
    public GameObject att_3_pref;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator att_ani;
    int hp = 10;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    private void Awake()
    {
        att_ani = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(think());

    }
    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator think()
    {
        rigid.velocity = Vector2.zero;

        att_ani.SetTrigger("1-0");
        yield return new WaitForSeconds(2f);
           



        int randomaction = Random.Range(0, 3);

        switch (randomaction)
        {
            case 0:
                StartCoroutine(att2_1());
                break;
            case 1:
                att3_1();
                break;
            case 2:
                StartCoroutine(att6_1());
                break;

        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "transball")
        {
            if (hp == 0)
            {
                Destroy(collision.gameObject);
                gameObject.SetActive(false);
                gameMenager.NextStage();
            }
            else
            {
                Destroy(collision.gameObject);
                hp--;
            }

        }
    }

    IEnumerator att1()
    {
        //걷기
        FliptoPlayer();
        att_ani.SetTrigger("1-1");
        if (spriteRenderer.flipX)
        {
            rigid.velocity = new Vector2(5, rigid.velocity.y);
        }
        else
        {
            rigid.velocity = new Vector2(-5, rigid.velocity.y);
        }

        yield return new WaitForSeconds(2.5f);
        StartCoroutine(think());
    }
    IEnumerator att2_1()
    {
        //텔포
        FliptoPlayer();
        att_ani.SetTrigger("1-4");

        yield return new WaitForSeconds(1.3f);
        StartCoroutine(att2_2());
    }
    IEnumerator att2_2()
    {

            if (player.transform.position.y < 0)
            {
                transform.position = new Vector2(20, 0);
                att_ani.SetTrigger("1-5");
            }
            else
            {
                transform.position = new Vector2(20, player.transform.position.y);
                att_ani.SetTrigger("1-5");
            }
       
        yield return new WaitForSeconds(1.3f);
        StartCoroutine(att2_3());
    }

    IEnumerator att2_3()
    {
        // 레이저


            att_2_1.transform.position = new Vector2(transform.position.x - 12f, transform.position.y);
            att_2_2.transform.position = new Vector2(transform.position.x - 2f, transform.position.y);

            att_ani.SetTrigger("1-2");
        yield return new WaitForSeconds(3.5f);
        StartCoroutine(att2_4());

    }
    IEnumerator att2_4()
    {

        att_ani.SetTrigger("1-4");

        yield return new WaitForSeconds(1.3f);
        StartCoroutine(att2_5());
    }
    IEnumerator att2_5()
    {

            transform.position = new Vector2(16, 0);
            att_ani.SetTrigger("1-5");

        yield return new WaitForSeconds(1.3f);
        StartCoroutine(think());
    }
     void att3_1()
    {
        // 땅찍
        FliptoPlayer();
        if(player.transform.position.y > 8f)
        {
            StartCoroutine(att3_2());

        }
        else if (Mathf.Abs(player.transform.position.x - transform.position.x) > 5)
        {
            StartCoroutine(att3_4());
        }
        else
        {
            StartCoroutine(att3_5());
        }
    }
    IEnumerator att3_2()
    {
        //텔포
        att_ani.SetTrigger("1-4");

        yield return new WaitForSeconds(1.3f);
        StartCoroutine(att3_3());
    }
    IEnumerator att3_3()
    {

            transform.position = new Vector2(transform.position.x, 8.5f);
            att_ani.SetTrigger("1-5");

        yield return new WaitForSeconds(1.3f);
        if (Mathf.Abs(player.transform.position.x - transform.position.x) > 5)
        {
            StartCoroutine(att3_4());
        }
        else
        StartCoroutine(att3_5());
    }

    IEnumerator att3_4()
    {
        //걷기
        FliptoPlayer();
        att_ani.SetTrigger("1-1");
        if (spriteRenderer.flipX)
        {
            rigid.velocity = new Vector2(3, rigid.velocity.y);
        }
        else
        {
            rigid.velocity = new Vector2(-3, rigid.velocity.y);
        }

        yield return new WaitForSeconds(2f);
        StartCoroutine(att3_5());


    }
    IEnumerator att3_5()
    {
        rigid.velocity = Vector2.zero;
        if (spriteRenderer.flipX)
        {
            att_1_1.transform.position = new Vector2(transform.position.x + 2.3f, transform.position.y);
            att_1_2.transform.position = new Vector2(transform.position.x + 4.8f, transform.position.y);
            att_1_3.transform.position = new Vector2(transform.position.x + 7.3f, transform.position.y);
            att_1_4.transform.position = new Vector2(transform.position.x + 9.8f, transform.position.y);
        }
        else
        {
            att_1_1.transform.position = new Vector2(transform.position.x - 2.3f, transform.position.y);
            att_1_2.transform.position = new Vector2(transform.position.x - 4.8f, transform.position.y);
            att_1_3.transform.position = new Vector2(transform.position.x - 7.3f, transform.position.y);
            att_1_4.transform.position = new Vector2(transform.position.x - 9.8f, transform.position.y);
        }
        att_ani.SetTrigger("1-3");
        yield return new WaitForSeconds(2f);
        StartCoroutine(think());
    }
    /*   IEnumerator att4()
       {
           //텔포
           FliptoPlayer();
           att_ani.SetTrigger("1-4");

           yield return new WaitForSeconds(1.3f);
           StartCoroutine(att4_1());
       }
       IEnumerator att4_1()
       {
           if (spriteRenderer.flipX)
           {
               transform.position = new Vector2(- 5, player.transform.position.y);
               att_ani.SetTrigger("1-5");
           }
           else
           {
               transform.position = new Vector2(20, player.transform.position.y);
               att_ani.SetTrigger("1-5");
           }
           yield return new WaitForSeconds(1.3f);
           StartCoroutine(att2());
       }
   */
    IEnumerator att5()
    {
        //기본
        FliptoPlayer();
        att_ani.SetTrigger("1-0");
        yield return new WaitForSeconds(1f);
        StartCoroutine(think());
    }

    IEnumerator att6_1()
    {
        //공중공격
        FliptoPlayer();
        att_ani.SetTrigger("1-6");
        yield return new WaitForSeconds(1.1f);
        StartCoroutine(att6_2());
    }
    IEnumerator att6_2()
    {
        //공중공격
        int randomattak = Random.Range(5, 10);
        for(int i = 0; i < randomattak; i++)
        {
            int randomposition = Random.Range(-6, 21);
            att_3_pref = Instantiate(att_3) as GameObject;
            att_3_pref.transform.position = new Vector2(randomposition, 13);
            Destroy(att_3_pref, 3);
        }
        att_ani.SetTrigger("1-0");
        yield return new WaitForSeconds(3f);
        StartCoroutine(think());
    }

    void FliptoPlayer()
    {

            if (player.transform.position.x < transform.position.x)
                spriteRenderer.flipX = false;
            else
                spriteRenderer.flipX = true;

    }
}
