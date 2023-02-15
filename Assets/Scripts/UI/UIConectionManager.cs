using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class UIConectionManager : MonoBehaviour
{
    public GameObject GoshtPrefabs;
    private PlayerPrefabChanger changers;
    private void Awake()
    {
        Cursor.visible = true;
        changers = FindObjectOfType<PlayerPrefabChanger>();
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
    public void startGosht()
    {
        //NetworkManager.Singleton.NetworkConfig.PlayerPrefab = GoshtPrefabs;
        //NetworkManager.Singleton.SpawnManager
        changers.PlayerSelected.Value = 1;
        if (NetworkManager.Singleton.StartClient())
        {
            GameObject go = Instantiate(GoshtPrefabs, Vector3.zero, Quaternion.identity);
            go.GetComponent<NetworkObject>().Spawn();
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
