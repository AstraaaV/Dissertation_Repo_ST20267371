using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class NodeUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TypewriterTMP typewriter;

    private bool isOpen;

    private void Awake()
    {
        Close();
    }

    private void LateUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOpen) return;

        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Close();
        }
    }

    public void Open(string text)
    {
        panel.SetActive(true);
        isOpen = true;
        typewriter.Type(text);
    }

    public void Close()
    {
        panel.SetActive(false);
        isOpen = false;
    }

    public bool IsOpen => isOpen;
}
