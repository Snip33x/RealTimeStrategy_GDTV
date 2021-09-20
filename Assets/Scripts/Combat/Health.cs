using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 100;

    [SyncVar(hook = nameof(HandleHealthUpdated))]
    private int currentHealth;

    public event Action ServerOnDie;

    public event Action<int, int> ClientOnHealthUpdated;


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

    private void HandleHealthUpdated(int oldHealth, int newHealth)
    {
        ClientOnHealthUpdated?.Invoke(newHealth, maxHealth);
    }

    #endregion

}
