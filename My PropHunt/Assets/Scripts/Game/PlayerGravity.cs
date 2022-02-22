using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerGravity : MonoBehaviour
{
    public bool IsGrounded => _isGrounded;

    [SerializeField] private float _force;
    [SerializeField] private float _startVelocity;
    [SerializeField] private float _radiusCheckGrounded;
    [SerializeField] private Transform _pointCheckOnGrounded;
    [SerializeField] private LayerMask _groundeable;

    private bool _isGrounded;
    private bool _isUseGravity = true;
    private float _velocityFall;
    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        EventManager.SwapProp += SetNotIsGrounded;
    }

    private void OnDestroy()
    {
        EventManager.SwapProp -= SetNotIsGrounded;
    }

    private void Update()
    {
        bool isNowCharacter = _characterController.enabled;

        if (isNowCharacter == true)
            _isGrounded = Physics.CheckSphere(_pointCheckOnGrounded.position, _radiusCheckGrounded, _groundeable);

        if (_isGrounded == false && isNowCharacter == true && _isUseGravity)
        {
            _characterController.Move(Vector3.down * (_velocityFall * Time.deltaTime));
            _velocityFall += _force;
        }
        else
            _velocityFall = _startVelocity;
    }

    public void DontUseGravity()
    {
        _isUseGravity = false;
    }

    public void UseGravity()
    {
        _isUseGravity = true;
    }

    private void SetNotIsGrounded()
    {
        _isGrounded = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer.CompareTo(_groundeable.value) != 0)
            _isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        SetNotIsGrounded();
    }
}