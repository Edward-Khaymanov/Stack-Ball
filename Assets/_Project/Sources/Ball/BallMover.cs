using System.Collections;
using UnityEngine;

public class BallMover : MonoBehaviour
{
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _moveSpeed;

    private Rigidbody _body;
    private IEnumerator _move;

    public void Init(Rigidbody body)
    {
        _body = body;
        _move = MoveCoroutine();
    }

    public void Enable()
    {
        StopCoroutine(_move);
        _body.velocity = Vector3.zero;
        _body.useGravity = true;
    }

    public void Disable()
    {
        StopCoroutine(_move);
        _body.velocity = Vector3.zero;
        _body.useGravity = false;
    }

    public void Jump()
    {
        _body.velocity = Vector3.zero;
        _body.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
    }

    public void Move()
    {
        _body.useGravity = false;
        _body.velocity = Vector3.zero;
        RestartMove();
    }

    public void StopMove()
    {
        StopCoroutine(_move);
        _body.velocity = Vector3.zero;
        _body.useGravity = true;
    }

    private void RestartMove()
    {
        StopCoroutine(_move);
        _move = MoveCoroutine();
        StartCoroutine(_move);
    }

    private IEnumerator MoveCoroutine()
    {
        var wait = new WaitForFixedUpdate();
        yield return wait;
        _body.velocity = Vector3.zero;

        while (true)
        {
            transform.Translate(Vector3.down * _moveSpeed * Time.fixedDeltaTime);
            yield return wait;
        }
    }
}
