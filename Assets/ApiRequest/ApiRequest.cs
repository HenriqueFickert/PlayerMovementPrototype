using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LoginData
{
    public string email;
    public string senha;
}

[Serializable]
public class ApiResponse<T> where T : class
{
    public List<string> mensagem;
    public bool sucesso;
    public T dados;
}

[Serializable]
public class DadosLogin
{
    public string id;
    public string nome;
    public string accessToken;
    public string refreshToken;
    public bool notificar;
}

public class DadosPaginacao<T> where T : class
{
    public List<T> pagina { get; set; }
    public PaginaDados dados { get; set; }
}

public class Usuario
{
    public string id { get; set; }
    public string nome { get; set; }
    public string sobrenome { get; set; }
    public DateTime dataNascimento { get; set; }
    public string genero { get; set; }
    public string email { get; set; }
    public bool notificar { get; set; }
    public string status { get; set; }
}

public class PaginaDados
{
    public int paginaAtual { get; set; }
    public int totalPaginas { get; set; }
    public int resultadosExibidosPagina { get; set; }
    public int contagemTotalResultados { get; set; }
    public bool existePaginaAnterior { get; set; }
    public bool existePaginaPosterior { get; set; }
}

public class ApiRequest : RequestBase
{
    private string email = "dodiko7640@poverts.com";
    private string senha = "Teste@123";

    private string token = "";

    private void Start()
    {
        SendPostRequest();
    }

    public void SendPostRequest()
    {
        LoginData data = new()
        {
            email = email,
            senha = senha
        };
        string json = JsonUtility.ToJson(data);

        SendPostRequest("autenticacao/login", json, (ApiResponse<DadosLogin> response) =>
        {
            token = response.dados.accessToken;
            SendGetRequest("usuario/usuario", token, (ApiResponse<DadosPaginacao<Usuario>> response2) =>
            {
                Debug.Log(response2.dados.pagina[0].nome);
            });
        });
    }
}