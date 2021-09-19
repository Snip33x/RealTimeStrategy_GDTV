using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 100;

    [SyncVar]
    private int currentHealth;

    public event Action ServerOnDie;


    #region Server

    public override void OnStartServer()
    {
        currentHealth = maxHealth;
    }

    [Server]
    public void DealDamage(int damgeAmout)
    {
        if(currentHealth <= 0) { return; }

        currentHealth -= damgeAmout;  //Nathiel made if statement currentHealth == 0 and then currentHealth = Mathf.Max(currentHealth - damageAmout, 0  -- this function is making currentHealth be 0 if its negative (imo <= is better , will see)

        if(currentHealth != 0) { return; }  //??? what for

        ServerOnDie?.Invoke(); //question mark is to eliminate throwing errors if noone is listening

        Debug.Log("We Died");
    }


    #endregion

    #region Client

    #endregion

}
