using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletmove : MonoBehaviour
{

    
    public static bulletmove bm;
    
    [SerializeField]
    float speed;

    [SerializeField]
    GameObject pool;
    // Start is called before the first frame update


    private void OnEnable()
    {
        transform.SetParent(null);
    }
    void Start()

    {
        bm = this;


    


     

        pool = GameObject.FindGameObjectWithTag("pool");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.up * speed);
        //움직이는 코드
    }
    private void Update()
    {
        if(transform.position.y>10)
        {
            transform.SetParent(pool.transform);
            gameObject.SetActive(false);
        }
        //범위 벗어나면 풀링
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        
        
        
            
        if (collision.CompareTag("left"))
            GameManager.gm.leftarm_hp -= 0.03f;
        if (collision.CompareTag("right"))
            GameManager.gm.rightarm_hp -= 0.03f;

        //팔 딜
        if (collision.CompareTag("head"))
        {
           if(GameManager.gm.rightarm_hp<0&& GameManager.gm.leftarm_hp<0)
            GameManager.gm.head_hp -= 0.027f;//적 딜

        }


            GameManager.gm.UpdateUI();

       
        if(collision.CompareTag("left")|| collision.CompareTag("right") || collision.CompareTag("head") )
        {
            transform.SetParent(pool.transform);

            gameObject.SetActive(false);

        }
        //적 맞으면 풀링


        }
       




    }

  

