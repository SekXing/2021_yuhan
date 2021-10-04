using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class t : MonoBehaviour
{
    public float maxSpeed = 4;
    public float jumpPower = 12;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator ani;
    public GameObject transballObj1;
    public GameObject transballObj2;
    GameObject ball;
    float ballpower = 0;
    float maxpower = 1.5f;
    ParticleSystem ps;
    public GameMenager gameMenager;
    public Stagemake stagemake;
    float time;
    int layermask;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        ps = GetComponent<ParticleSystem>();
        layermask = (1 << LayerMask.NameToLayer("platform")) + (1 << LayerMask.NameToLayer("plat"));

    }

    private void Update()
    {
        transball();
        transball2();
        transplayer();
    }

    void FixedUpdate()
    {
        playerMove();
        playerJump();








    }
    void transplayer()
    {
        if (Input.GetKey(KeyCode.S))
        {
            try
            {

                transform.position = new Vector2(ball.gameObject.transform.position.x, ball.gameObject.transform.position.y);
                Destroy(ball);
                rigid.velocity = Vector2.zero;


            }
            catch (Exception)
            {

            }



        }
    }
    void transball()
    {
        if (Input.GetKey(KeyCode.A))
        {

            if (ballpower < maxpower)
            {
                ballpower += Time.deltaTime;
                if (!ps.isPlaying)
                    ps.Play();
            }

            if (ballpower > 1.5f)
            {
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                chargeMax();
 
            }


        }




    }
    void transball2()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (ballpower > 1.5f)
            {
                if (spriteRenderer.flipX)
                {
                    ball = Instantiate(transballObj1, transform.position, transform.rotation) as GameObject;
                    Rigidbody2D rigidball = ball.GetComponent<Rigidbody2D>();
                    ball.GetComponent<ParticleSystem>().Play();
                    rigidball.AddForce(Vector2.left * 5, ForceMode2D.Impulse);
                    Destroy(ball, 1.5f);
                    ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                    ballpower = 0;
                    spriteRenderer.color = new Color(1, 1, 1, 1);

                }
                else
                {
                    ball = Instantiate(transballObj2, transform.position, transform.rotation) as GameObject;
                    Rigidbody2D rigidball = ball.GetComponent<Rigidbody2D>();
                    ball.GetComponent<ParticleSystem>().Play();
                    rigidball.AddForce(Vector2.right * 5, ForceMode2D.Impulse);
                    Destroy(ball, 1.5f);
                    ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                    ballpower = 0;
                    spriteRenderer.color = new Color(1, 1, 1, 1);

                }
            }
            else
            {
                ballpower = 0;
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                spriteRenderer.color = new Color(1, 1, 1, 1);
            }


        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {


            if (GameObject.Find("AA").transform.GetChild(0).gameObject.activeSelf)
            {

                    gameMenager.NextStage();


            }

            if (GameObject.Find("AA").transform.GetChild(1).gameObject.activeSelf)
            {


                if (stagemake.SaveMode)
                {

                    stagemake.MapNameOn();

                }

            }


            if (GameObject.Find("AA").transform.GetChild(2).gameObject.activeSelf)
            {
                GameObject.Find("Stage_Load").transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("Stage_Load").transform.GetChild(0).gameObject.SetActive(true);

                gameObject.SetActive(false);




            }

        }
        /*
        if(collision.gameObject.tag == "Item")
        {

            collision.gameObject.SetActive(false);

            savech = true;
            save = transform.position;

        }
        */

    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "bottom")
        {
            if (GameObject.Find("AA").transform.GetChild(0).gameObject.activeSelf)
            {
                gameMenager.PlayerReposition();

            }

            if (GameObject.Find("AA").transform.GetChild(1).gameObject.activeSelf)
            {
               stagemake.PlayerReposition();


            }


        }
    }



    public void playerStop()
    {
        rigid.velocity = Vector2.zero;
    }

    void playerJump()
    {
        if (Input.GetKey(KeyCode.UpArrow) && !ani.GetBool("jumping"))
        {
            ani.SetBool("jumping", true);
            rigid.velocity = new Vector2 (rigid.velocity.x , 0);
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

        }


/*        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
*/          

        if(Math.Abs(rigid.velocity.y) <0.03f)
            ani.SetBool("jumping", false);

  /*      if (rigid.velocity.y < 0 || rigid.velocity.y == 0)
        {
            Debug.DrawRay(rigid.position, Vector2.down, new Color(0, 1, 0));
            RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, Vector2.down, 1, layermask);
            if(rayhit)
            {
                if(rayhit.distance > 0.4f &&rayhit.distance < 0.7f)

            }

        }*/
            
        if (rigid.velocity.y < -5f)
            ani.SetBool("jumping", true);
    }

    void chargeMax()
    {

        time += 3 * Time.deltaTime;
        if (time < 1f)
            spriteRenderer.color = new Color(1, 1-time, 1, 1);
        else
        {
            spriteRenderer.color = new Color(1, time, 1, 1);
            if(time > 1f)
            {
                time = 0;
            }
        }

    }

    void playerMove()
    {
        if (Input.GetKey(KeyCode.RightArrow) && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
        {
            spriteRenderer.flipX = false;
            rigid.AddForce(Vector2.right * 1, ForceMode2D.Impulse);
            if (rigid.velocity.x > maxSpeed * 1)
                rigid.velocity = new Vector2(maxSpeed * 1, rigid.velocity.y);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            spriteRenderer.flipX = true;
            rigid.AddForce(Vector2.right * -1, ForceMode2D.Impulse);
            if (rigid.velocity.x < maxSpeed * -1)
                rigid.velocity = new Vector2(maxSpeed * -1, rigid.velocity.y);

        }

        if (Mathf.Abs(rigid.velocity.x) > 0.45)
            ani.SetBool("moving", true);
        else
            ani.SetBool("moving", false);



    }


}
