using UnityEngine;

public class Footprint : MonoBehaviour
{
    [SerializeField] private GameObject _footprintTemplate;

    public void SetColor(Color32 color)
    {
        _footprintTemplate.GetComponent<SpriteRenderer>().color = color;
    }

    public void LeaveFootprint(Vector3 surfacePoint, Transform container)
    {
        surfacePoint += new Vector3(0, 0.01f, 0);
        Instantiate(_footprintTemplate, surfacePoint, _footprintTemplate.transform.rotation, container);
    }
}
