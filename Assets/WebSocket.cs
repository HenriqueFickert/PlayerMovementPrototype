using System.Collections;
using System.Net.Sockets;
using UnityEngine;

public class WebSocket : MonoBehaviour
{
    private const string SERVER_IP = "192.168.100.254";  // Use o endereço IP do seu servidor
    private const int SERVER_PORT = 9000;

    private TcpClient client;

    private void Start()
    {
        ConnectToServer();
        if (client.Connected)
        {
            StartCoroutine(ReceiveDataFromServer()); // Se você estiver usando Unity, corotinas são uma maneira eficaz de fazer isso.
        }
    }

    private void ConnectToServer()
    {
        client = new TcpClient(SERVER_IP, SERVER_PORT);
        Debug.Log("Conectado ao servidor!");
        ReceiveDataFromServer();
    }

    private IEnumerator ReceiveDataFromServer()
    {
        while (client.Connected)
        {
            if (client.Available > 0)
            {
                byte[] data = new byte[client.Available];
                NetworkStream stream = client.GetStream();

                int bytesRead = stream.Read(data, 0, data.Length);
                string message = System.Text.Encoding.ASCII.GetString(data, 0, bytesRead);
                Debug.Log("Recebido: " + message);
            }
            yield return new WaitForSeconds(0.1f);  // Aguarda um pouco antes de verificar novamente. Isso evita o uso excessivo da CPU.
        }
    }
}