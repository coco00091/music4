using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movetocenter : MonoBehaviour
{
    [SerializeField]
    float speed = 0.5f;
 
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);
        if(Vector3.Distance(transform.position,Vector3.zero)<0.2f)
        {
            
            transform.SetParent(GameObject.FindGameObjectWithTag("pool").transform);//Ç®¸µ
            gameObject.SetActive(false);
        }
    }
}
