using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChamandoPHP : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine( pegarInformacaoPHP()); // Está funcionando em paralelo.
    }

    // É necessário o IEnumerator para uma programação assincrona, já que não dá pra ficar esperando a resposta da internet enquanto você joga.
    IEnumerator pegarInformacaoPHP()
    {
        WWWForm webForm = new WWWForm();
        webForm.AddField("pesquisa", "");

        UnityWebRequest uw = UnityWebRequest.Post("http://localhost/Aula1/index.php", webForm);
        yield return uw.SendWebRequest();// Se for assincrona, o Unity continua enquanto espera a resposta. Se for sincrona ele para aqui.

        if(uw.isHttpError || uw.isNetworkError)
        {
            Debug.Log("Deu ruim: "+uw.error);
        }
        else //Funfou
        {
            Debug.Log("Deu bom: "+uw.downloadHandler.text);
        }


    }

}
