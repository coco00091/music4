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
        //�����̴� �ڵ�
    }
    private void Update()
    {
        if(transform.position.y>10)
        {
            transform.SetParent(pool.transform);
            gameObject.SetActive(false);
        }
        //���� ����� Ǯ��
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        
        
        
            
        if (collision.CompareTag("left"))
            GameManager.gm.leftarm_hp -= 0.03f;
        if (collision.CompareTag("right"))
            GameManager.gm.rightarm_hp -= 0.03f;

        //�� ��
        if (collision.CompareTag("head"))
        {
           if(GameManager.gm.rightarm_hp<0&& GameManager.gm.leftarm_hp<0)
            GameManager.gm.head_hp -= 0.027f;//�� ��

        }


            GameManager.gm.UpdateUI();

       
        if(collision.CompareTag("left")|| collision.CompareTag("right") || collision.CompareTag("head") )
        {
            transform.SetParent(pool.transform);

            gameObject.SetActive(false);

        }
        //�� ������ Ǯ��


        }
       




    }

  

