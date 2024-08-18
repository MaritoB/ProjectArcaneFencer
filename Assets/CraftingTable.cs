using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    CinemachineVirtualCamera m_GameCamera, m_CraftingCamera;
    bool isInteracting = false;
    [SerializeField]
    Transform m_SwordOnTable;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player == null)
        {
            return;
        }
        m_SwordOnTable.gameObject.SetActive(true);
        //player.HideSword();
        InteractWithCraftingTable();

    }
    private void OnTriggerExit(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player == null)
        {
            return;
        }

        m_SwordOnTable.gameObject.SetActive(false);
        player.ShowSword();
        TurnOffCrafting();

    }
    private void OnTriggerStay(Collider other)
    {/*
        PlayerController player = other.GetComponent<PlayerController>();

        if (player == null)
        {
            return;
        }
        if (player.playerInputActions.Player.Attack.WasPressedThisFrame() && !isInteracting)
        {
            InteractWithCraftingTable();
        }
        if (player.playerInputActions.Player.Dash.WasPressedThisFrame() && isInteracting)
        {
            TurnOffCrafting();
        }
      */
    }

    private void InteractWithCraftingTable()
    {
        if (isInteracting)
        {
            return;

        }
        m_GameCamera.gameObject.SetActive(false);
        m_CraftingCamera.gameObject.SetActive(true);
    }
    void TurnOffCrafting()
    {

        isInteracting = false;

        m_GameCamera.gameObject.SetActive(true);
        m_CraftingCamera.gameObject.SetActive(false);

    }
    
}
