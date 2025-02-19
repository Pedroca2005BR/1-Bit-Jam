using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class QualFinalEscolhido : MonoBehaviour
{
    //[SerializeField] public Image imagemBranca;
    public FinalBomTrigger script;
    public PlayerMovement PlayerMovement;
    public Fade FadeToWhite;
    //repete = quantas vezes a velocidade sera diminuida
    //time = quanta tempo entre uma diminuida e outra
    //velocidadeDimPorSegundo = quando a velocidade cai cada vez, velocidade maxima é dita no script PlayerMovement na variavel speed
    private float repete,time = 2f, velocidadeDimPorSegundo = 0.9f, umavez = 0;
    //Não deixa a barra de calor mudar quando o final for ativado
    public bool congelarCalor;
    private void Start()
    {
        repete = 0;
        congelarCalor = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (umavez == 0)
            {
                umavez = 1;
                //Se o jogador ativou o final bom isso acontece
                if (script.FinalBom == true)
                {
                    congelarCalor = true;
                    StartCoroutine(FinalBom());
                    

                }
                    //Caso o jogador não tenha conseguido o final bom
                else
                {
                    congelarCalor = true;
                    StartCoroutine(FinalRuim());
                }
            }

        }
    }

    IEnumerator FinalRuim()
    {
        
        while (repete < 6){
            yield return new WaitForSeconds(time);
            PlayerMovement.speed += -velocidadeDimPorSegundo;
            repete += 1;
        }
        FadeToWhite.FadeOut();


    }
    IEnumerator FinalBom()
    {
        while (repete < 6)
        {
            yield return new WaitForSeconds(time);
            PlayerMovement.speed += -velocidadeDimPorSegundo;
            repete += 1;
        }
        FadeToWhite.FadeFinalBom();
    }
        public void Final()
    {
        if(script.FinalBom) SceneManager.LoadScene("Final_bom");
        else SceneManager.LoadScene("Final_ruim");

    }

    

}
