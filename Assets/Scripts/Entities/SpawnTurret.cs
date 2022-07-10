using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnTurret : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _turret;

    public void SpawnTurretAction()
    {
        _player.PhotonView.RPC("SpawnTurretRPC", RpcTarget.All, _turret.name);
    }
}
