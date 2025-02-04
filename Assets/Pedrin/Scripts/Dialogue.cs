using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    //Campos
    //Window
    public GameObject window;
    public TMP_Text textDialogue;

    //Lista de diálogos
    public List<string> dialogues;
    //Velocidade de escrita
    public float writingSpeed = 0.1f;
    //Índice de diálogo
    private int index;
    //indice do caracter
    private int charIndex;
    //Started Dialogue bool
    private bool started;
    //Esperar o proximo diálogo bool
    private bool waitForNext;


    private void Awake()
    {
        //Desativar a janela
        ToggleWindow(false);
    }
    private void ToggleWindow(bool show)
    {
        //Ativar ou desativar a janela
        window.SetActive(show);
    }

    //Começar o diálogo
    public void StartDialogue()
    {
        if(started)
        {
            return;
        }

        started = true;
        //Ativar a janela
        ToggleWindow(true);
        
        GetDialogue(0);
    }

    private void GetDialogue(int i)
    {
        //Comecar index no 0
        index = i;
        //Resetar o index do caracter
        charIndex = 0;
        textDialogue.text = string.Empty;

        StartCoroutine(Writing());
    }

    public void EndDialogue()
    {
        //Desativar a janela
        ToggleWindow(false);
    }

    IEnumerator Writing()
    {
        yield return new WaitForSeconds(writingSpeed);
        string currentDialogue = dialogues[index];
        //Escrever caracter
        textDialogue.text += currentDialogue[charIndex].ToString();
        //Apontar para outro caracter`
        charIndex++;
        if(charIndex < currentDialogue.Length)
        {
            //Esperar um tempo
            yield return new WaitForSeconds(writingSpeed);
            //Repetir
            StartCoroutine(Writing());
        }
        else
        {
            yield return new WaitForSeconds(1f);
            waitForNext = true;
        }
    }

    private void Update()
    {
        if(!started)
        {
            return;
        }

        if(waitForNext)
        {
            waitForNext = false;
            index++;
            if(index < dialogues.Count)
            {
                GetDialogue(index);
            }
            else
            {
                EndDialogue();
            }
        }
    }
}
