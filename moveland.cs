using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveland : MonoBehaviour
{
    Rigidbody2D rigid;
    Stagemake mv;
    public int nextMove = 1;
    bool firstmove = true;
    bool move;
    Vector2 save;
    GameObject player;
    // Start is called before the first frame update
    void Awake()
    {

        rigid = GetComponent<Rigidbody2D>();
        player = GameObject.Find("PP").transform.GetChild(0).gameObject;


    }

    private void Start()
    {
        save = transform.position;
    }
    private void Update()
    {
  
        if (player.activeSelf)
        {
            if (firstmove)
            {
                Invoke("Think", 5);
                firstmove = false;
            }
            rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
        }
        else
        {
            CancelInvoke("Think");
            transform.position = save;
            nextMove = 1;
            firstmove = true;
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {




    }

    void Think()
    {
      

            nextMove = -1 * nextMove;

            Invoke("Think", 5);

    }
}
