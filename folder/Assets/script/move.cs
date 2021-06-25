using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{

    [SerializeField]
    float speed;

    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed,0);

    }
}
