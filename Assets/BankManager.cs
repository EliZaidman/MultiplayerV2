using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BankManager : MonoBehaviour
{

    int _Silver = 1000;
    [SerializeField] private int _TurretCost;
    public static BankManager Instance { get; private set; }

    public PhotonView PhotonView { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        PhotonView = GetComponent<PhotonView>();
    }


    [PunRPC]
    private int CalculateCorrentMoney()
    {
        return _Silver;
    }
    [PunRPC]
    private void BuyTurret()
    {
        _Silver -= _TurretCost;
    }
}
