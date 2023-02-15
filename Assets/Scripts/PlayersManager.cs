using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayersManager : SingletonNetBehaviour<PlayersManager>
{
    private NetworkVariable<int> playersInGame = new NetworkVariable<int>();
    private NetworkVariable<int> lastTypePlayer = new NetworkVariable<int>();
    [SerializeField] private GameObject PlayerPrefabs;
    [SerializeField] private GameObject GoshtPrefabs;
    public int PlayersInGame
    {
        get
        {
            return playersInGame.Value;

        }
    }

    public NetworkVariable<int> LastTypePlayer
    {
        get => lastTypePlayer;
        set => lastTypePlayer = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        NetworkManager.Singleton.OnTransportFailure += () =>
        {
            Debug.Log("El autob�s se cay�");
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

                if (lastTypePlayer.Value == 2)
                {
                    //GameObject gosht = Instantiate(GoshtPrefabs);
                    //gosht.GetComponent<NetworkObject>().Spawn();
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
    
    [ServerRpc(RequireOwnership=false)] //server owns this object but client can request a spawn
    public void SpawnPlayerServerRpc(ulong clientId,int prefabId) {
        /*GameObject newPlayer;
        if (prefabId==0)
            newPlayer=(GameObject)Instantiate(PlayerPrefabs);
        else
            newPlayer=(GameObject)Instantiate(GoshtPrefabs);
        */
        print("SPAWNEO UN NUEVO PLAYER...");
    }
}
