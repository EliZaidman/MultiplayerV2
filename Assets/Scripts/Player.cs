using UnityEngine;
using Photon.Pun;
using UnityStandardAssets.Cameras;

public class Player : MonoBehaviour
{
    [SerializeField] private PhotonView _photonView;
    public PhotonView PhotonView => _photonView;

    [SerializeField] FreeLookCam freeLookCam;

    private delegate void State();
    private State _state;

    void Awake()
    {
        if (_photonView.IsMine)
        {
            freeLookCam = FindObjectOfType<FreeLookCam>();
        }
    }

    void Start()
    {
        if (_photonView.IsMine)
        {
            freeLookCam.SetTarget(gameObject.transform);
            gameObject.tag = "OtherPlayer";
            _state = Idle;
        }
    }

    void Update()
    {
        if (!_photonView.IsMine)
        {
            _state.Invoke();
            //gameObject.tag = "OtherPlayer";
        }
    }

    private void FreeMouse(bool value)
    {
        Cursor.visible = value;
        Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;
    }

    #region States
    private void Idle()
    {
        if (_photonView.IsMine)
        {
            Debug.Log("Current State = Idle State");

            if (Cursor.visible)
            {
                FreeMouse(false);
            }

            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                _state = Interface;
            }
        }
    }

    private void Interface()
    {
        if (_photonView.IsMine)
        {
            Debug.Log("Current State = Idle State");

            if (!Cursor.visible)
            {
                FreeMouse(true);
            }

            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                _state = Idle;
            }
        }
    }
    #endregion

    #region PunRPC
    // methods to call from _photonView and will happen over the network (photonView.RPC("methodNameRPC", RpcTarget.All, argument1, argument2);)

    // cannot pass custom Types as argument (no custom classes, no gameObjects no Lists, no enums etc... mostly int, stings and bool)

    [PunRPC]
    private void SpawnTurretRPC(string turretName)
    {
        if (_photonView.IsMine)
        {
            GameObject newTurret = PhotonNetwork.Instantiate(turretName, Vector3.zero, Quaternion.identity);
            newTurret.transform.SetParent(gameObject.transform.GetChild(4).transform);
        }
    }

    #endregion
}
