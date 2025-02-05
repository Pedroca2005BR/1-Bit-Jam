using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Calor : MonoBehaviour
{
    public Image BarraCalorImagem;
    //calor = o valor atual da barra de calor
    //CalorMax = o valor maximo que a barra pode encher
    //VelocidadeEsquentar = a quantidade por segundo que a barra de temperatura aumenta dentro do alcance de uma tocha
    //VelocidadeEsfriar = a quantidade por segundo que a barra de temperatura diminui fora do alcance
    public float calor, CalorMax, VelocidadeEsquentar, VelocidadeEsfriar;
    
    public bool tocha;//Verdadeiro caso o jogador esteja no alcance de uma 'tocha'
    

    void Update()
    {
        if (tocha == true) calor += VelocidadeEsquentar * Time.deltaTime;

        else calor -= VelocidadeEsfriar * Time.deltaTime;

        //para o valor não passar dos limites estabelecidos
        if (calor > 100) calor = 100;
        if (calor < 0) calor = 0;

        BarraCalorImagem.fillAmount = calor / CalorMax;

    }
    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        //Quando o jogador entrar em colisão com o trigger com tag 'Fogo', a bool tocha recebera valor verdadeiro, signifcando que o jogador está no alcance da tocha.
        if (collision.gameObject.CompareTag("Fogo"))
        {
            tocha = true;
        }
    }

    private void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        //Quando o jogador sair do collider de algo a bool tocha recebera valor negativo, o que faz o jogador perder lentamente a barra de calor.
        tocha = false;
    }
}
