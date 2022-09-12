using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Logar : MonoBehaviour
{
    public InputField infLogin;
    public InputField infSenha;

    public void FazerLogin()
    {
        StartCoroutine(verificarLogin(infLogin.text, infSenha.text));
    }

    private void LoginDeuCerto(string Login)
    {
        PlayerPrefs.SetString("Login", Login);
        SceneManager.LoadScene("Jogo");
    }
    private void LoginDeuRuim()
    {
        Debug.Log("Não!");
    }
    IEnumerator verificarLogin(string Login, string Senha)
    {
        WWWForm wwwf = new WWWForm();
        wwwf.AddField("login", Login);
        wwwf.AddField("senha", Senha);

        UnityWebRequest w = UnityWebRequest.Post("http://localhost/mortal_clicker/verificaLogin.php", wwwf);

        yield return w.SendWebRequest(); //Deixa o unity continuar com o comando, mas quando responder, volta aqui.

        if (w.isHttpError || w.isNetworkError)
        {
            Debug.Log("Erro: " + w.error);
        }
        else
        {
            if (w.downloadHandler.text.Equals("OK"))
            {
                LoginDeuCerto(Login);
            }
            else
            {
                LoginDeuRuim();
            }
        }
    }
}
