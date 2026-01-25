using TMPro;
using UnityEngine;
using System.Collections;

public class StoryTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI storyText;
    [SerializeField] private float displayTime = 5f;

    private Coroutine currentRoutine;

    public void ShowText(string text)
    {
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }
        currentRoutine = StartCoroutine(ShowRoutine(text));
    }

    private IEnumerator ShowRoutine(string text)
    {
        storyText.gameObject.SetActive(true);
        storyText.text = text;

        yield return new WaitForSeconds(displayTime);

        storyText.gameObject.SetActive(false);
    }
}
