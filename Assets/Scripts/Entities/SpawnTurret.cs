using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnTurret : MonoBehaviour
{
    private PhotonView _photonView;
    private GameObject _turret;
    private const string _playerTag = "Player";
    private bool _canSpawn = false;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (_photonView.IsMine && Input.GetKeyDown(KeyCode.E))
        {
            if (_canSpawn)
            {
                PhotonNetwork.Instantiate(_turret.name, transform.position, Quaternion.identity);
                // BankManager.Instance.PhotonView.RPC("BuyTurret", RpcTarget.All)
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_photonView.IsMine && other.tag == _playerTag)
        {
            _canSpawn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_photonView.IsMine && other.tag == _playerTag)
        {
            _canSpawn = false;
        }
    }
}
