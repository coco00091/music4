using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymove : MonoBehaviour
{
    float target;
    bool up=true;
    [SerializeField]
    float speed;   

    // Update is called once per frame
    void Update()
    {
       
        if(up)
            target = Mathf.Lerp(transform.position.y, 2.3f, speed);
        else
            target = Mathf.Lerp(transform.position.y, 1.9f,speed);

        if (target>2.2f || target<2f)
            up = !up;

        transform.position = new Vector3(transform.position.x, target);
        //À§¾Æ·¡·Î Ãâ··Ãâ·· (»ï°¢ÇÔ¼ö·Î º¯°æ ¿¹Á¤)
    }
}
