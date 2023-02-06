using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayersManager : SingletonNetBehaviour<PlayersManager>
{
    private NetworkVariable<int> playersInGame = new NetworkVariable<int>();

    public int PlayersInGame
    {
        get
        {
            return playersInGame.Value;

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        NetworkManager.Singleton.OnTransportFailure += () =>
        {
            Debug.Log("El autobús se cayó");
        };

        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {
            if (IsServer)
            {
                Debug.Log($"{id} just connected...");
                if (playersInGame.Value == 0)
                {
                    playersInGame.Value += 2;
                }
                else
                {
                    playersInGame.Value++;
                }

            }
        };

        NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
        {
            if (IsServer)
            {
                Debug.Log($"{id} just (dis)connected...");
                playersInGame.Value--;
            }
        };

        NetworkManager.Singleton.OnServerStarted += () =>
        {
            if (IsServer)
            {
                playersInGame.Value = 0;
                Debug.Log("Server started and playersInGame reset");
            }
        };
    }
    public void restPlayers()
    {
        playersInGame.Value--;
    }
}
