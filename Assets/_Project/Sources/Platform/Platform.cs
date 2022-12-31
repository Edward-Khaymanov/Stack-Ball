using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Platform : MonoBehaviour
{
    [SerializeField] private float ThrowPointOffsetX = 8;
    [SerializeField] private float ThrowPointOffsetY = 13;
    [SerializeField] private float Destroy_duration = 1f;

    private const string COLOR_PROPERTY = "_Color";

    public int PartsCount => transform.childCount;

    public void Init(Color32 color, int maxDangerPartsCount)
    {
        var allParts = GetAllParts();
        var colorProperty = new MaterialPropertyBlock();
        var currentDangerParts = 0;

        for (int i = 0; i < PartsCount; i++)
        {
            var part = allParts.Dequeue();
            var randomnessFactor = Random.Range(0, 100) < 90;

            if (currentDangerParts <= maxDangerPartsCount && randomnessFactor)
            {
                colorProperty.SetColor(COLOR_PROPERTY, Color.black);
                part.AddComponent<PlatformDangerPart>();
            }
            else
            {
                colorProperty.SetColor(COLOR_PROPERTY, color);
            }

            part.GetComponent<MeshRenderer>().SetPropertyBlock(colorProperty);
            currentDangerParts++;
        }
    }

    public void Destroy()
    {
        var parts = GetAllParts();

        foreach (var part in parts)
        {
            var throwDirection = GetThrowDirection(part.transform.position);
            part.GetComponent<MeshCollider>().enabled = false;
            StartCoroutine(DestroyPart(part.transform, throwDirection));
        }
    }

    private IEnumerator DestroyPart(Transform partTransform, Vector3 throwDirection)
    {
        StartCoroutine(partTransform.Move(throwDirection, Destroy_duration));
        yield return partTransform.RotateExt(Random.rotation.eulerAngles, Destroy_duration);
        Destroy(partTransform.gameObject);
    }

    private Vector3 GetThrowDirection(Vector3 worldPosition)
    {
        var positionY = transform.position.y + ThrowPointOffsetY;
        var positionX = (worldPosition.x <= transform.position.x ? Vector3.left : Vector3.right).x * ThrowPointOffsetX;
        return new Vector3(positionX, positionY, worldPosition.z);
    }

    private Queue<GameObject> GetAllParts()
    {
        var childs = new Queue<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            childs.Enqueue(transform.GetChild(i).gameObject);
        }

        return childs;
    }
}
