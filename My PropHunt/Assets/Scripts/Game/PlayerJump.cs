using UnityEngine;
using Photon.Pun;
using System.Collections;

[System.Serializable]
internal class ItemJump
{
    [SerializeField] internal float _force;
    [SerializeField] internal int _additionalCountJumps;

    internal Rigidbody _rigidbody;
}

[System.Serializable]
internal class CharacterJump
{
    [SerializeField] internal float _height;
    [SerializeField] internal float _speed;
    [SerializeField] internal int _additionalCountJumps;

    internal int _maxProgress = 1;
    internal CharacterController _characterController;
}

[RequireComponent(typeof(PlayerGravity), typeof(PhotonView), typeof(ActorView))]
[RequireComponent(typeof(CharacterController), typeof(Rigidbody))]
public class PlayerJump : PlayerInputs
{
    [SerializeField] private int _currentCountJumps;
    [SerializeField] private CharacterJump _characterJump;
    [SerializeField] private ItemJump _itemJump;

    private PlayerGravity _playerGravity;
    private PhotonView _photonView;
    private ActorView _actorView;

    private void Awake()
    {
        _characterJump._characterController = GetComponent<CharacterController>();
        _itemJump._rigidbody = GetComponent<Rigidbody>();
        _playerGravity = GetComponent<PlayerGravity>();
        _photonView = GetComponent<PhotonView>();
        _actorView = GetComponent<ActorView>();
    }

    private void Update()
    {
        if (_photonView.IsMine == false)
            return;

        bool isPressJumpButton = Space;
        bool isOnGround = _playerGravity.IsOnGround;

        if (isOnGround == true)
            SetMaxJumps();

        if (isPressJumpButton == true)
        {
            bool isNowCharacter = _characterJump._characterController.enabled;

            if(_currentCountJumps > 0)
            {
                if (isNowCharacter == true)
                    StartCoroutine(JumpInCharacterForm());
                else
                    _itemJump._rigidbody.velocity = new Vector3(_itemJump._rigidbody.velocity.x, _itemJump._force, _itemJump._rigidbody.velocity.z);

                _currentCountJumps--;
            }
        }
    }

    private IEnumerator JumpInCharacterForm()
    {
        float progress = _characterJump._maxProgress;

        _playerGravity.DontUseGravity();

        while (progress > 0)
        {
            progress -= _characterJump._speed * Time.deltaTime;

            _characterJump._characterController.Move(Vector3.up * (progress * (_characterJump._height * (_characterJump._speed * Time.deltaTime))));
            _actorView.SetValue(GlobalStringsVars.FallValueAnimator);

            yield return null;
        }

        _actorView.SetValue(GlobalStringsVars.FallValueAnimator);
        _playerGravity.UseGravity();
    }

    private void SetMaxJumps()
    {
        _currentCountJumps = _characterJump._characterController.enabled == true ? _characterJump._additionalCountJumps : _itemJump._additionalCountJumps;
    }
}