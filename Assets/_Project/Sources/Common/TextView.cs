using TMPro;
using UnityEngine;

public class TextView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textField;

    public void Render(string text)
    {
        _textField.text = text;
    }
}
