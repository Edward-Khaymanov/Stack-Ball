using UnityEngine;


public class BallAnimator
{
    private readonly Animator _animator;
    private readonly SphereCollider _collider;
    private readonly Vector3 _colliderDefaultCenter;
    private readonly Vector3 _moveColliderOffset = new Vector3(0, 0.2f);

    public BallAnimator(Animator animator, SphereCollider collider, Vector3 colliderDefaultCenter)
    {
        _animator = animator;
        _collider = collider;
        _colliderDefaultCenter = colliderDefaultCenter;
    }

    public void Land()
    {
        _animator.SetTrigger(AnimatorBallController.Triggers.Land);
    }

    public void Move()
    {
        _animator.enabled = false;
        _collider.center += _moveColliderOffset;
    }

    public void StopMove()
    {
        _animator.enabled = true;
        _collider.center = _colliderDefaultCenter;
    }
}
