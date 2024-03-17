using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    private Slider canBari;
    private int can = 100;
    private void Awake()
    {
        canBari = GameObject.Find("Slider").GetComponent<Slider>();

    }


    private void Start()
    {
        canBari.maxValue = 100;
        canBari.minValue = 0;
        canBari.value = can;
        canBari.wholeNumbers = true;
        canBari.fillRect.GetComponent<Image>().color = Color.green;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("20 birim can azaldi");
            can -= 20;
            canBari.value = can;
        }

        else if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("15 birim can arttı");
            can += 15;
            canBari.value = can;
        }

    }

}
