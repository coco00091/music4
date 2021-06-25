using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public FixedJoystick variableJoystick;
    public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void FixedUpdate()
    {
        //Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        Vector3 direction = new Vector3(variableJoystick.Horizontal, variableJoystick.Vertical);
        rb.transform.Translate(direction * speed * Time.fixedDeltaTime);
        print(direction);
    }
}