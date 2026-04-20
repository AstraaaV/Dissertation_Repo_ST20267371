using TMPro;
using UnityEngine;

public class CompletionCounter : MonoBehaviour
{
    public static CompletionCounter Instance;

    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private int totalNodes = 7;
    [SerializeField] private GameObject endPanel;

    private int activatedNodes = 0;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        UpdateCounterUI();

        if(endPanel != null)
        {
            endPanel.SetActive(false);
        }
    }

    public void RegisterNodeActivation()
    {
        activatedNodes++;
        UpdateCounterUI();

        Debug.Log($"Node activated! Total: {activatedNodes}/{totalNodes}");

        if (activatedNodes >= totalNodes)
        {
            ShowEndPanel();
        }
    }

    private void UpdateCounterUI()
    {
        if (counterText != null)
        {
            counterText.text = $"Nodes Activated: {activatedNodes}/{totalNodes}";
        }
    }

    private void ShowEndPanel()
    {
        if (endPanel != null)
        {
            endPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("End panel reference is missing!");
        }
    }
}
