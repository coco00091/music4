using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotating : MonoBehaviour
{
    float target;
    bool up = true;
    [SerializeField]
    float speed;







    float normal;
    [SerializeField]
    float rotatespeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {


        normal += Time.deltaTime;
        transform.rotation = Quaternion.AngleAxis(-normal * rotatespeed, Vector3.forward);






        
        if(up)
            target = Mathf.Lerp(transform.position.x, 0.3f, speed);
        else
            target = Mathf.Lerp(transform.position.x, -0.3f,speed);

        if (target>0.2f || target<-0.2f)
            up = !up;

        transform.position = new Vector3(target, transform.position.y,transform.position.z);
        
    }
  
}
