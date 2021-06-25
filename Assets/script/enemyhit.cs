using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyhit : MonoBehaviour
{

    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("bullet"))
        {
            StartCoroutine(colorchange());
        }
    }

    IEnumerator colorchange()
    {
        sprite.material.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        sprite.material.color = Color.white;
    }    
}
