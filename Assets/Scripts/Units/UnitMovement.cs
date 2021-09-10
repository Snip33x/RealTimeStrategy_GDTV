using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class UnitMovement : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;


    #region Server

    [ServerCallback]
    private void Update()
    {
        if(!agent.hasPath) { return; }

        if (agent.remainingDistance > agent.stoppingDistance) { return; }

        agent.ResetPath();
    }

    [Command]
    public void CmdMove(Vector3 position)
    {
        if(!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f , NavMesh.AllAreas)) { return; }

        agent.SetDestination(hit.position);
    }

    #endregion

    //#region Client

    //public override void OnStartAuthority()
    //{
    //    mainCamera = Camera.main;
    //    base.OnStartAuthority();
    //}

    //[ClientCallback]
    //private void Update()
    //{
    //    if(!hasAuthority) { return; }

    //    if(!Mouse.current.rightButton.wasPressedThisFrame) { return; }

    //    Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()); //grab where the cursor is

    //    if(!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) { return; }

    //    CmdMove(hit.point);
    //}

    //#endregion
}
