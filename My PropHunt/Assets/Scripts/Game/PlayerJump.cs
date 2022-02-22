using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody), typeof(PhotonView), typeof(PlayerGravity))]
[RequireComponent(typeof(CharacterController))]
public class PlayerJump : PlayerInputs
{
    [SerializeField] private int _currentCountJumps;
    [SerializeField] private CharacterJump _characterJump;
    [SerializeField] private ItemJump _itemJump;

    private PlayerGravity _playerGravity;
    private PhotonView _photonView;

    private void Awake()
    {
        _characterJump._characterController = GetComponent<CharacterController>();
        _playerGravity = GetComponent<PlayerGravity>();
        _photonView = GetComponent<PhotonView>();
        _itemJump._rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_photonView.IsMine == false)
            return;

        bool isPressJump = Space;

        if (isPressJump == true)
        {
            bool isNowCharacter = _characterJump._characterController.enabled;

            if (_playerGravity.IsGrounded == true)
                SetMaxJumps();

            if(_currentCountJumps > 0)
            {
                if (isNowCharacter == true)
                    JumpCharacter();
                else
                    _itemJump._rigidbody.velocity = new Vector3(_itemJump._rigidbody.velocity.x, _itemJump._force, _itemJump._rigidbody.velocity.z);

                _currentCountJumps--;
            }
        }

        if(_characterJump._isNeedJump == true)
        {
            _characterJump._characterController.Move(Vector3.up * (_characterJump._velocityJump * Time.deltaTime));
            _characterJump._velocityJump -= _characterJump._gravityForce;

            if (_characterJump._velocityJump <= _characterJump._minSpeedJump)
            {
                _characterJump._isNeedJump = false;
                _playerGravity.UseGravity();
            }
        }
    }

    private void JumpCharacter()
    {
        _playerGravity.DontUseGravity();
        _characterJump._isNeedJump = true;
        _characterJump._velocityJump = _characterJump._height;
    }

    private void SetMaxJumps()
    {
        _currentCountJumps = _characterJump._characterController.enabled == true ? _characterJump._additionalCountJumps : _itemJump._additionalCountJumps;
    }

    [System.Serializable]
    private struct CharacterJump
    {
        [SerializeField] internal float _height;
        [SerializeField] internal float _gravityForce;
        [SerializeField] internal float _minSpeedJump;
        [SerializeField] internal int _additionalCountJumps;

        internal bool _isNeedJump;
        internal float _velocityJump;
        internal CharacterController _characterController;
    }

    [System.Serializable]
    private struct ItemJump
    {
        [SerializeField] internal float _force;
        [SerializeField] internal int _additionalCountJumps;

        internal Rigidbody _rigidbody;
    }
}