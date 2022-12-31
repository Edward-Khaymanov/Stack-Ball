using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(BallMover))]
[RequireComponent(typeof(BallInvincibility))]
[RequireComponent(typeof(BallCollisionHandler))]
public class Ball : MonoBehaviour
{
    [SerializeField] private Animator _animatorComponent;
    [SerializeField] private BallSFX _sfx;
    [SerializeField] private BallVFX _vfx;
    [SerializeField] private BallView _view;

    private BallAnimator _animator;
    private BallCollisionHandler _collisionHandler;
    private BallInput _input;
    private BallInvincibility _invincibility;
    private BallMover _mover;
    private SphereCollider _collider;
    private Transform _startPoint;

    public bool IsInvincible => _invincibility.IsInvincible;

    [Inject]
    private void Constructor(BallInput input, [Inject(Id = DIMarkers.BALL_START_POINT)] Transform startPoint)
    {
        _input = input;
        _startPoint = startPoint;
    }

    private void Awake()
    {
        var rigidbody = GetComponent<Rigidbody>();

        _collider = GetComponent<SphereCollider>();
        _collisionHandler = GetComponent<BallCollisionHandler>();
        _invincibility = GetComponent<BallInvincibility>();
        _mover = GetComponent<BallMover>();

        _animator = new BallAnimator(_animatorComponent, _collider, _collider.center);
        _collisionHandler.Init(_input, _invincibility, rigidbody);
        _mover.Init(rigidbody);
    }

    private void OnEnable()
    {
        EventsHolder.LevelPassed += OnLevelPassed;
        EventsHolder.Losed += OnLosed;

        _collisionHandler.Bouncing += Bounce;

        _input.Pressed += StartMove;
        _input.Unpressed += StopMove;

        _invincibility.Enabled += _vfx.PlayInvincibilityActivation;
        _invincibility.Enabled += _vfx.PlayFire;
        _invincibility.Enabled += _vfx.HideTrail;
        _invincibility.Enabled += _sfx.PlayInvincible;
        _invincibility.Disabled += _vfx.StopFire;
        _invincibility.Disabled += _vfx.ShowTrail;
    }

    private void OnDisable()
    {
        EventsHolder.LevelPassed -= OnLevelPassed;
        EventsHolder.Losed -= OnLosed;

        _collisionHandler.Bouncing -= Bounce;

        _input.Pressed -= StartMove;
        _input.Unpressed -= StopMove;

        _invincibility.Enabled -= _vfx.PlayInvincibilityActivation;
        _invincibility.Enabled -= _vfx.PlayFire;
        _invincibility.Enabled -= _vfx.HideTrail;
        _invincibility.Enabled -= _sfx.PlayInvincible;
        _invincibility.Disabled -= _vfx.StopFire;
        _invincibility.Disabled -= _vfx.ShowTrail;
    }

    public void ResetBall(BallSkin ballSkin, Color32 color)
    {
        _mover.Disable();
        _collisionHandler.ResetHandler();
        _collider.enabled = true;
        transform.position = _startPoint.position;
        _mover.Enable();
        _view.Show();
        UpdateView(ballSkin, color);
        _input.enabled = true;
    }

    public void UpdateView(BallSkin ballSkin, Color32 color)
    {
        _view.Render(ballSkin, color);
        _vfx.RenderTrail(ballSkin, color);

        var footprintColor = color;
        if (ballSkin.UseMaterialColor)
            footprintColor = ballSkin.Material.color;

        _vfx.SetFootprintColor(footprintColor);
    }

    private void OnLevelPassed()
    {
        _input.enabled = false;
    }

    private void OnLosed()
    {
        _input.enabled = false;
        _collider.enabled = false;
        _mover.Disable();
        _view.Hide();
        _sfx.PlayBreakBall();
    }

    private void Bounce(Vector3 surfacePoint, Transform container)
    {
        _animator.Land();
        _mover.Jump();
        _sfx.PlayBounce();

        if (surfacePoint != Vector3.zero)
            _vfx.LeaveFootprint(surfacePoint, container);
    }

    private void StartMove()
    {
        _animator.Move();
        _mover.Move();
    }

    private void StopMove()
    {
        _animator.StopMove();
        _mover.StopMove();
    }
}
