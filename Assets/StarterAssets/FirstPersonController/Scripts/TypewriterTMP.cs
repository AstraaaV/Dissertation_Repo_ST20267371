using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterTMP : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI target;
    [SerializeField, Range(0.001f, 0.1f)] private float secondsPerCharacter = 0.02f;

    private Coroutine currentRoutine;

    public void SetInstant(string text)
    {
        StopTyping();
        target.text = text;
    }

    public void Type(string text)
    {
        StopTyping();
        currentRoutine = StartCoroutine(TypeRoutine(text));
    }

    public void StopTyping()
    {
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
            currentRoutine = null;
        }
    }

    private IEnumerator TypeRoutine(string text)
    {
        target.text = "";
        for (int i = 0; i < text.Length; i++)
        {
            target.text += text[i];
            yield return new WaitForSeconds(secondsPerCharacter);
        }
        currentRoutine = null;
    }
}