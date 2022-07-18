using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnTurret : MonoBehaviour
{
    private PhotonView _photonView;
    [SerializeField] private GameObject _turret;
    [SerializeField] private BoxCollider _turretSpawnerCollider;
    private const string _playerTag = "Player";
    private bool _canSpawn = false;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _turretSpawnerCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (_photonView.IsMine && Input.GetKeyDown(KeyCode.E))
        {
            if (_canSpawn)
            {
                PhotonNetwork.Instantiate(_turret.name, transform.position, Quaternion.identity);
                BankManager.Instance.PhotonView.RPC("BuyTurret", RpcTarget.All);
                print("ME PUT TARTARTARTER");
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_photonView.IsMine && other.tag == _playerTag)
        {
            _canSpawn = true;
        }
        print("ME INSSIDE");
        

    }

    private void OnTriggerExit(Collider other)
    {
        if (_photonView.IsMine && other.tag == _playerTag)
        {
            _canSpawn = false;
        }
        print("ME EXIT");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_turretSpawnerCollider.transform.position, _turretSpawnerCollider.transform.localScale); ;
        //Gizmos.DrawSphere(transform.position, 2);
    }
}
