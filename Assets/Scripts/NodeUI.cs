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

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOpen)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            return;
        }

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

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Close()
    {
        panel.SetActive(false);
        isOpen = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public bool IsOpen => isOpen;
}
