using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody), typeof(PhotonView), typeof(CharacterController))]
public class PlayerTransformation : PlayerInputs
{
    public bool IsNowCharacter => _character.activeSelf;

    [SerializeField] private float _distanceToTransformate;
    [SerializeField] private LayerMask _transformable;
    [SerializeField] private LayerMask _groundeable;
    [SerializeField] private Transform _pointTopCharacter;
    [SerializeField] private GameObject _currentProp;
    [SerializeField] private GameObject _character;

    private RaycastHit _rayHit;
    private Rigidbody _rigidbody;
    private PhotonView _photonView;
    private CharacterController _characterController;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _photonView = GetComponent<PhotonView>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_photonView.IsMine == false)
            return;

        bool isMouse0 = Mouse0;
        bool isMouse1 = Mouse1;

        if (isMouse0 == true && Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth * 0.5f, Camera.main.pixelHeight * 0.5f)), out _rayHit, _distanceToTransformate, _transformable))
        {
            GameObject transformateProp = PhotonNetwork.Instantiate(_rayHit.transform.gameObject.name, transform.position, transform.rotation);
            transformateProp.transform.parent = transform;
            transformateProp.layer = 0;

            SetBoolValueInComponentsOnSwap(false);
            DestroyCurrentProp();

            _currentProp = transformateProp;

            EventManager.SwapProp();
        }
        else if (isMouse1 == true && _character.activeSelf == false)
        {
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            Debug.DrawRay(_pointTopCharacter.position, Vector3.down * (-_character.transform.localPosition.y - -_pointTopCharacter.position.y), Color.red, 5);
            if (Physics.Raycast(_pointTopCharacter.localPosition, Vector3.down, out _rayHit, -_character.transform.localPosition.y - -_pointTopCharacter.position.y, _groundeable) && +_rayHit.point.y + -_character.transform.localPosition.y > transform.position.y)
                transform.position = new Vector3(transform.position.x, +_rayHit.point.y + -_character.transform.localPosition.y, transform.position.z);

            SetBoolValueInComponentsOnSwap(true);
            DestroyCurrentProp();

            EventManager.SwapProp();
        }
    }

    private void SetBoolValueInComponentsOnSwap(bool value)
    {
        _rigidbody.isKinematic = value;
        _characterController.enabled = value;

        if (_character.activeSelf != value)
            _character.SetActive(value);
    }

    private void DestroyCurrentProp()
    {
        if (_currentProp != null)
            PhotonNetwork.Destroy(_currentProp);
    }
}