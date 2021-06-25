using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gothit : MonoBehaviour
{
    [SerializeField]
    float lazerdamage;
    private void OnTriggerStay2D(Collider2D collision)
    {
            //print("¸ÂÀ½");
            GameManager.gm.life_hp -= lazerdamage * Time.deltaTime * 6;

            GameManager.gm.UpdateUI();
    }
}
