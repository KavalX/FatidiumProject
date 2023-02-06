using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class UIConectionManager : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = true;
    }

    public void startHostSeccion()
    {
        if (NetworkManager.Singleton.StartHost())
        {
            Debug.Log("Host arrancado");
        }
        else
        {
            Debug.Log("Horror: Host no pudo arrancar");
        }
    }

    public void startClient()
    {
        if (NetworkManager.Singleton.StartClient())
        {
            Debug.Log("Client arrancado");
        }
        else
        {
            Debug.Log("Horror: Client no pudo arrancar");
        }
    }

    public void startServer()
    {
        if (NetworkManager.Singleton.StartServer())
        {
            Debug.Log("Server arrancado");
        }
        else
        {
            Debug.Log("Horror: Server no pudo arrancar");
        }
    }

    public void checkPlayer()
    {
        Debug.Log(FindObjectOfType<PlayersManager>());
    }
}
