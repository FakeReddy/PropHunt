using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody), typeof(PhotonView), typeof(CharacterController))]
public class PlayerTransformation : PlayerInputs
{
    [SerializeField] private float _distanceToTransformateProp;
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

        bool isPressTransformInPropButton = Mouse0;
        bool isPressTransformInCharacterButton = Mouse1;

        if (isPressTransformInPropButton == true && Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth * 0.5f, Camera.main.pixelHeight * 0.5f)), out _rayHit, _distanceToTransformateProp, _transformable))
            TransformInPropOnNameOnNetwork(_rayHit.transform.gameObject.name);
        else if (isPressTransformInCharacterButton == true && _character.activeSelf == false)
            TransformInCharacterOnNetwork();
    }

    private void TransformInPropOnNameOnNetwork(string nameProp)
    {
        GameObject transformateProp = PhotonNetwork.Instantiate(nameProp, transform.position, transform.rotation);

        transformateProp.transform.parent = transform;
        transformateProp.layer = GlobalStringsVars.DefaultLayer;

        SetBoolValueInComponentsAtTransform(false);
        OnTransform();

        _currentProp = transformateProp;
    }

    private void TransformInCharacterOnNetwork()
    {
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        if (Physics.Raycast(_pointTopCharacter.localPosition, Vector3.down, out _rayHit, -_character.transform.localPosition.y - -_pointTopCharacter.position.y, _groundeable) && +_rayHit.point.y + -_character.transform.localPosition.y > transform.position.y)
            transform.position = new Vector3(transform.position.x, +_rayHit.point.y + -_character.transform.localPosition.y, transform.position.z);

        SetBoolValueInComponentsAtTransform(true);
        OnTransform();
    }

    private void OnTransform()
    {
        DestroyCurrentPropFromNetwork();
        EventManager.SwapProp();
    }

    private void SetBoolValueInComponentsAtTransform(bool value)
    {
        _rigidbody.isKinematic = value;
        _characterController.enabled = value;

        if (_character.activeSelf != value)
            _character.SetActive(value);
    }

    private void DestroyCurrentPropFromNetwork()
    {
        if (_currentProp != null)
            PhotonNetwork.Destroy(_currentProp);
    }
}