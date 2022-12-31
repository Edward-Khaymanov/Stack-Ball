using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BallCollisionHandler : MonoBehaviour
{
    private BallInput _input;
    private BallInvincibility _invincibility;
    private Rigidbody _body;
    private int _lastCollisionFrameIndex = -1;

    public event Action<Vector3, Transform> Bouncing;

    public void Init(BallInput input, BallInvincibility invincibility, Rigidbody body)
    {
        _input = input;
        _invincibility = invincibility;
        _body = body;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out FinishPlatform finish))
            Bouncing?.Invoke(GetCollidePoint(), collider.transform);

        if (_input.IsPressed)
            return;

        if (_lastCollisionFrameIndex == Time.frameCount)
            return;

        _lastCollisionFrameIndex = Time.frameCount;

        if (_body.velocity.y < 0)
            Bouncing?.Invoke(GetCollidePoint(), collider.transform);
    }

    private void OnTriggerStay(Collider collider)
    {
        if (_input.IsPressed == false)
            return;

        collider.enabled = false;

        if (_invincibility.IsInvincible == false && collider.TryGetComponent(out PlatformDangerPart dangerPart))
        {
            EventsHolder.Lose();
            return;
        }

        var parent = collider.transform?.parent;
        if (parent != null && parent.TryGetComponent(out Platform platform))
        {
            platform.Destroy();
            EventsHolder.DestroyPlatform();
        }
    }

    public void ResetHandler()
    {
        _lastCollisionFrameIndex = -1;
    }

    private Vector3 GetCollidePoint()
    {
        RaycastHit raycastHit;
        Physics.Linecast(transform.position, transform.position + Vector3.down * 0.1f, out raycastHit);
        return raycastHit.point;
    }
}
