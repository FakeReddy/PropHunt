using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class DeactivatorIfNotMine : MonoBehaviour
{
    [SerializeField] private bool _destroyOnStart;

    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();

        if (_photonView.IsMine == true)
            gameObject.SetActive(true);

        if (_destroyOnStart == true)
            Destroy(gameObject);
    }
}