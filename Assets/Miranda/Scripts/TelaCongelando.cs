using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class TelaCongelando : MonoBehaviour
{
    public Calor calor;
    public float aux=0f, mod;
    public bool alterar; 
    public Image image;
    public Sprite[] sprites;

    private int index =0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        alterar = true;

    }
    private void Update()
    {
        aux = calor.calor;
        
        if (aux >= 35f)
        {
           
            mod = 0.2f;
        }
        else
        {
            
            mod = 0f;
        }
        var newColor = new Color(1.0f, 1.0f, 1.0f, 1-(calor.calor/150f)-0.45f);
        //GetComponent<Image>().sprite = sprites[index];
        image.color = newColor;
    }

    // Update is called once per frame
    
}
