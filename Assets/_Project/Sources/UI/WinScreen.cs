using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class WinScreen : Screen, IPointerDownHandler
{
    [SerializeField] private TMP_Text _congratulation;

    public void Render(int levelNumber)
    {
        _congratulation.text = $"Level {levelNumber} \n COMPLETED!";
        Show();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        EventsHolder.BuildLevel();
        Hide();
    }
}
