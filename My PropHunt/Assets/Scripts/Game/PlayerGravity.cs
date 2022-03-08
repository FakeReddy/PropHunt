using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(ActorView))]
public class PlayerGravity : MonoBehaviour
{
    public bool IsOnGround => _isOnGround;

    [SerializeField] private float _force;
    [SerializeField] private float _beginVelocity;
    [SerializeField] private float _radiusCheckGround;
    [SerializeField] private Transform _checkPointOnGround;
    [SerializeField] private LayerMask _groundeable;

    private bool _isOnGround;
    private bool _isUseGravity = true;
    private bool _isNowCharacter;
    private float _velocityFall;
    private CharacterController _characterController;
    private ActorView _actorView;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _actorView = GetComponent<ActorView>();

        EventManager.SwapProp += SetNotIsGrounded;
    }

    private void OnDestroy()
    {
        EventManager.SwapProp -= SetNotIsGrounded;
    }

    private void Update()
    {
        _isNowCharacter = _characterController.enabled;

        CheckOnGroundIfNowCharacter();

        if (_isOnGround == false && _isNowCharacter == true && _isUseGravity)
        {
            _characterController.Move(Vector3.down * (_velocityFall * Time.deltaTime));
            _velocityFall += _force;
            _actorView.SetValue(GlobalStringsVars.FallValueAnimator);
        }
        else
            _velocityFall = _beginVelocity;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer.CompareTo(_groundeable.value) != GlobalStringsVars.DefaultLayer)
            _isOnGround = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        SetNotIsGrounded();
    }

    public void DontUseGravity()
    {
        _isUseGravity = false;
    }

    public void UseGravity()
    {
        _isUseGravity = true;
    }

    private bool CheckOnGroundIfNowCharacter()
    {
        if (_isNowCharacter == true)
            _isOnGround = Physics.CheckSphere(_checkPointOnGround.position, _radiusCheckGround, _groundeable);
    
        return _isOnGround;
    }

    private void SetNotIsGrounded()
    {
        _isOnGround = false;
    }
}