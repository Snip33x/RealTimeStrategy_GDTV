using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : NetworkBehaviour
{
    [SerializeField] private Health health = null;

    public static event Action<UnitBase> ServerOnBaseSpawned; //everytime a base is spawned we add it to a list - same in Unit.cs
    public static event Action<UnitBase> ServerOnBaseDespawned;

    #region Server

    public override void OnStartServer()
    {
        health.ServerOnDie += ServerHandleDie;

        ServerOnBaseSpawned?.Invoke(this);
    }

    public override void OnStopServer()
    {
        ServerOnBaseDespawned?.Invoke(this);

        health.ServerOnDie -= ServerHandleDie;
    }

    [Server]
    private void ServerHandleDie()
    {
        NetworkServer.Destroy(gameObject);
    }
    #endregion

    #region Client

    #endregion
}
