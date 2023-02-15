using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Unity.Networking;
using Unity.Networking.Transport;

public class PlayerPrefabChanger : NetworkBehaviour, INetworkPrefabInstanceHandler
{
    public GameObject PrefabToSpawn,GoshPrefabs;
    private NetworkVariable<int> playerSelected = new NetworkVariable<int>();
    public bool DestroyWithSpawner;        
    private GameObject m_PrefabInstance;
    private NetworkObject m_SpawnedNetworkObject;

    public NetworkVariable<int> PlayerSelected
    {
        get => playerSelected;
        set => playerSelected = value;
    }

    public void Start()
    {
        playerSelected.Value = 0;
    }

    public override void OnNetworkSpawn()
    {
        print("1.---INSTANCIADO");
        // Only the server spawns, clients will disable this component on their side
        enabled = IsServer;            
        if (!enabled || PrefabToSpawn == null)
        {
            return;
        }
        NetworkManager.PrefabHandler.AddHandler(GoshPrefabs, this);
        // Instantiate the GameObject Instance
        m_PrefabInstance = Instantiate((playerSelected.Value ==0)?PrefabToSpawn:GoshPrefabs);
            
        // Optional, this example applies the spawner's position and rotation to the new instance
        //m_PrefabInstance.transform.position = transform.position;
        //m_PrefabInstance.transform.rotation = transform.rotation;
            
        // Get the instance's NetworkObject and Spawn
        m_SpawnedNetworkObject = m_PrefabInstance.GetComponent<NetworkObject>();
        m_SpawnedNetworkObject.Spawn();
    }

    public override void OnNetworkDespawn()
    {
        if (IsServer && DestroyWithSpawner && m_SpawnedNetworkObject != null && m_SpawnedNetworkObject.IsSpawned)
        {
            m_SpawnedNetworkObject.Despawn();
        }
        base.OnNetworkDespawn();
    }

    public NetworkObject Instantiate(ulong ownerClientId, Vector3 position, Quaternion rotation)
    {
        print("3.----INSTANCIANDO");
        m_PrefabInstance.SetActive(true);
        m_PrefabInstance.transform.position = transform.position;
        m_PrefabInstance.transform.rotation = transform.rotation;
        return m_SpawnedNetworkObject;
    }
    public void SpawnInstance()
    {
        if (!IsServer)
        {
            return;
        }
        print("2.----INSTANCIANDO");
        if (m_PrefabInstance != null && m_SpawnedNetworkObject != null && !m_SpawnedNetworkObject.IsSpawned)
        {
            m_PrefabInstance.SetActive(true);
            m_SpawnedNetworkObject.Spawn();
            
        }
    }

    public void Destroy(NetworkObject networkObject)
    {
        m_PrefabInstance.SetActive(false);
    }
}