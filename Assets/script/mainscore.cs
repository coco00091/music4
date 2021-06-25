using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class mainscore : MonoBehaviour
{
    [SerializeField]
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "HIGHEST\n"+PlayerPrefs.GetFloat("highestscore");
    }

}
