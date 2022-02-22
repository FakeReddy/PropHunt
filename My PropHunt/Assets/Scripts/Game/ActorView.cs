using UnityEngine;

public class ActorView : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void SetValue(int value)
    {
        _animator.SetInteger(GlobalStringsVars.ValueAnim, value);
    }
}