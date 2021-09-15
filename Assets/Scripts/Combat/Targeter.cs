using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : NetworkBehaviour
{
    [SerializeField] private Targetable target;

    #region Server
    [Command]
    public void CmdSetTarget(GameObject targetGameObject)
    {
        if(!targetGameObject.TryGetComponent<Targetable>(out Targetable target)) { return; } //if the target doesn't have Targetable component then return

        this.target = target; //target = newTarget - if we want it to be clearer click on targets here to see the reference
    }

    [Server]
    public void ClearTarget()
    {
        target = null;
    }    

    #endregion

    #region Client

    #endregion

}
