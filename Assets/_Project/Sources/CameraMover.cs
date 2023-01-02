using UnityEngine;
using Zenject;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Vector3 _trackOffset = new Vector3(0, 3, -8.3f);
    [SerializeField] private Vector3 _startOffset = new Vector3(0, 1.5f, -8.3f);
    [SerializeField, Range(0f, 1f)] private float _trackDelay = 0.25f;

    private Input _input;
    private Transform _target;
    private Vector3 _velocity;
    private Vector3 _targetPosition;
    private Vector3 _startPosition;
    private float _minTrackHeight;

    [Inject]
    private void Constructor(
        Input input,
        Ball target,
        [Inject(Id = DIMarkers.CAMERA_MIN_HEIGHT)] float minTrackHeight,
        [Inject(Id = DIMarkers.BALL_START_POINT)] Transform targetStartPoint)
    {
        _input = input;
        _target = target.transform;
        _minTrackHeight = minTrackHeight;
        _startPosition = targetStartPoint.position + _startOffset;
    }

    private void LateUpdate()
    {
        var tempTargetPosition = _target.transform.position + _trackOffset;

        var canTrack = _input.IsPressed && tempTargetPosition.y < _targetPosition.y && tempTargetPosition.y > _minTrackHeight;
        if (canTrack)
            _targetPosition = tempTargetPosition;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            _targetPosition,
            ref _velocity,
            _trackDelay,
            Mathf.Infinity,
            Time.smoothDeltaTime);
    }

    public void ResetMover()
    {
        transform.position = _startPosition;
        _targetPosition = _startPosition;
    }
}
