using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Fade : MonoBehaviour
{

    public Image image;
    public QualFinalEscolhido QualFinalEscolhido;
    public PlayerMovement PlayerMovement;
    public Calor calor;

    //visible or invisible
    public float target = 1.0f;

    public float second = 2.0f;

    private void Start()
    {
        image.enabled = false;
    }
    public void FadeOut()
    {
        image.enabled = true;
        StartCoroutine(FadeImage((getImageDone) =>
        {
            if (getImageDone)
            {
                
                //code after Fade visible 
            }
        }));
    }
    public void FadeFinalBom() 
    {
        image.enabled = true;
        StartCoroutine(FinalBomFade((getImageDone) =>
        {
            if (getImageDone)
            {

                //code after Fade visible 
            }
        }));

    }

    private IEnumerator FadeImage(Action<bool> action)
    {
        var alpha = image.color.a;
        for (var t = 1.0f; t > 0.0f; t -= Time.deltaTime / second)
        {
            //change color as you want
            var newColor = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(alpha, target, t));
            image.color = newColor;
            yield return null;
            action(image.color.a < 0.05f);
        }
        QualFinalEscolhido.Final();
    }
    private IEnumerator FinalBomFade(Action<bool> action)
    {
        var alpha = image.color.a;
        for (var t = 1.0f; t > 0.0f; t -= Time.deltaTime / second)
        {
            //change color as you want
            var newColor = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(alpha, target, t));
            image.color = newColor;
            yield return null;
            action(image.color.a < 0.05f);
            //a tela vai ficando branca
        }

        PlayerMovement.isDisabled = true;//player nao pode se mover
        PlayerMovement.TeleporteFinalBom();// teleporta o player para o um local separado para ele ver a pedra
        calor.calor = 100f;// enche barra de calor deixando o efeito invisivel

        yield return new WaitForSeconds(3);

        AudioManager.Instance.PlaySFX("rocha_push");

        yield return new WaitForSeconds(5);
        for (var t = 0.0f; t < 1.0f; t += Time.deltaTime / second)
        {
            //change color as you want
            var newColor = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(alpha, 0f, t));
            image.color = newColor;
            yield return null;
            action(image.color.a < 0.05f);
        }
        
        
        yield return new WaitForSecondsRealtime(3);
        QualFinalEscolhido.Final();
    }
    }
