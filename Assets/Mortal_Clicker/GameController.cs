using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameController : MonoBehaviour
{
    private int pontos = 0;
    public Text txtMsg;

    public Button SoVai;
    public Button Desistir;

    private string Login;
    private string Nome;
    private void Start()
    {
        Login = PlayerPrefs.GetString("Login", string.Empty);
        Nome = PlayerPrefs.GetString("Nome", string.Empty);


        if (!Login.Equals(string.Empty))
        {
            txtMsg.text = "Ol� " + Nome + "(" + Login + ")";
        }
    }
    public void BtnVai()
    {
        if(Random.Range(1f,20f)<19)
        {
            pontos += 1;
            
        }
        else
        {
            pontos = 0;
        }
        
        AtualizaMensagem();
    }

    private void AtualizaMensagem()
    {
        if(pontos == 0)
        {
            txtMsg.text = "Perdeu playboy...";
        }
        else
        {
            txtMsg.text = "Sua pontua��o � de " + pontos;
        }
    }

    public void BtnDesistir()
    {
        if(pontos > 0)
        {
            //Registra pontua��o
            StartCoroutine(AdicionarPontuacao());
            pontos = 0;
            txtMsg.text = "Registrando sua pontua��o...";
            SoVai.enabled = false;
            Desistir.enabled = false;
            
        }
        else
        {
            txtMsg.text = "Come�a a� primeiro... S� vai...";
        }

    }

    IEnumerator AdicionarPontuacao()
    {
        WWWForm wwwf = new WWWForm();
        wwwf.AddField("jogador", Login);
        wwwf.AddField("pontos", pontos);

        UnityWebRequest w = UnityWebRequest.Post("http://localhost/mortal_clicker/inserirPontuacao.php", wwwf);

        yield return w.SendWebRequest(); //Deixa o unity continuar com o comando, mas quando responder, volta aqui.

        if (w.isHttpError || w.isNetworkError)
        {
            Debug.Log("Erro: " + w.error);
        }
        else
        {
            if(w.downloadHandler.text.Equals("OK"))
            {
                Desistir.enabled = true;
                SoVai.enabled = true;

                txtMsg.text = "Sua pontua��o foi salva";
            }
            else if(w.downloadHandler.text.Equals("Pontuacao menor, mas OK"))
            {
                Desistir.enabled = true;
                SoVai.enabled = true;

                txtMsg.text = "Nem salvei hein, fa�a melhor...";
            }
        }
    }
}
