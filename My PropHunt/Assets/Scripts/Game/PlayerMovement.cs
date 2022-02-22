using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody), typeof(PhotonView), typeof(PlayerGravity))]
[RequireComponent(typeof(CharacterController), typeof(ActorView))]
public class PlayerMovement : PlayerInputs
{
    [SerializeField] private float _speedInProp;
    [SerializeField] private float _speedInCharacter;
    [SerializeField] private float _addingToSpeedIfRunningInCharacter;
    [SerializeField] private float _rangeSmoothTimeRotationToDirection;

    private Camera _camera;
    private Rigidbody _rigidbody;
    private ActorView _actorView;
    private PhotonView _photonView;
    private PlayerGravity _playerGravity;
    private CharacterController _characterController;
    private float _nVelocityRotationToDirection;

    private void Awake()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
        _actorView = GetComponent<ActorView>();
        _photonView = GetComponent<PhotonView>();
        _playerGravity = GetComponent<PlayerGravity>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_photonView.IsMine == false)
            return;

        bool isNowCharacter = _characterController.enabled;
        bool isGrounded = _playerGravity.IsGrounded;
        bool isPressRunButton = Shift;

        float horizontal = Horizontal;
        float vertical = Vertical;

        Vector3 direction = new Vector3(horizontal, 0, vertical);

        if (isNowCharacter == true)
            _actorView.SetValue(_playerGravity.IsGrounded == true ? GlobalStringsVars.IdleValueAnim : GlobalStringsVars.FallValueAnim);

        if (direction.magnitude != 0)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.transform.eulerAngles.y;
            direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;

            if (isNowCharacter == false)
            {
                direction = direction.normalized * (_speedInProp * Time.deltaTime);
                direction.y = _rigidbody.velocity.y;

                _rigidbody.velocity = direction;
            }
            else
            {
                float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref _nVelocityRotationToDirection, _rangeSmoothTimeRotationToDirection);
                direction = direction.normalized * (_speedInCharacter * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, smoothAngle, 0);
                _actorView.SetValue(GlobalStringsVars.WalkValueAnim);

                if (isPressRunButton == true && isGrounded == true)
                {
                    direction = direction.normalized * ((_speedInCharacter + _addingToSpeedIfRunningInCharacter) * Time.deltaTime);
                    _actorView.SetValue(GlobalStringsVars.RunValueAnim);
                }
                else if (isGrounded == false)
                    _actorView.SetValue(GlobalStringsVars.FallValueAnim);

                _characterController.Move(direction);
            }
        }
        else if (isGrounded == false && isNowCharacter == false)
            _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, 0);
    }
}