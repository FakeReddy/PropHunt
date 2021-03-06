using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody), typeof(PhotonView), typeof(CharacterController))]
public class PlayerRotation : PlayerInputs
{
    [SerializeField] private float _speed;

    private Camera _camera;
    private Rigidbody _rigidbody;
    private PhotonView _photonView;
    private CharacterController _characterController;
    private bool _isNowStopRotation;

    private void Awake()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
        _photonView = GetComponent<PhotonView>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_photonView.IsMine == false)
            return;

        bool isNowCharacter = _characterController.enabled;
        bool isPressRotateButton = Shift;

        if (isPressRotateButton == true && isNowCharacter == false)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0), _speed);
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            _isNowStopRotation = false;
        }
        else if (_isNowStopRotation == false)
        {
            _rigidbody.constraints = RigidbodyConstraints.None;
            _isNowStopRotation = true;
        }
    }
}