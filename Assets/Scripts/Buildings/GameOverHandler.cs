using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverHandler : NetworkBehaviour
{
    public static event Action<string> ClientOnGameOver;

    private List<UnitBase> bases = new List<UnitBase>();

    #region Server

    public override void OnStartServer()
    {
        UnitBase.ServerOnBaseSpawned += ServerHandleBaseSpawned;
        UnitBase.ServerOnBaseDespawned += ServerHandleBaseDespawned;
    }

    public override void OnStopServer()
    {
        UnitBase.ServerOnBaseSpawned -= ServerHandleBaseSpawned;
        UnitBase.ServerOnBaseDespawned -= ServerHandleBaseDespawned;
    }

    [Server]
    private void ServerHandleBaseSpawned(UnitBase unitBase)
    {
        bases.Add(unitBase);
    }

    [Server]
    private void ServerHandleBaseDespawned(UnitBase unitBase)
    {
        bases.Remove(unitBase);

        if(bases.Count != 1) { return; }

        Debug.Log("Game over");

        int playerId = bases[0].connectionToClient.connectionId;

        RpcGameOver($"Player {playerId}");
    }

    #endregion

    #region Client

    [ClientRpc]
    private void RpcGameOver(string winner)
    {
        ClientOnGameOver?.Invoke(winner);
    }

    #endregion
}
