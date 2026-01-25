using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [Header("Node Identity")]
    [SerializeField] private string nodeId = "Node_01";

    [Header("Visual")]
    [SerializeField] private GameObject activatedVisual;

    [SerializeField] private string storyText;
    [SerializeField] private StoryTextController textController;

    private bool activated;
    public bool Activated => activated;
    public string NodeId => nodeId;

    public void Activate(SessionManager session)
    {
        if (activated) return;
        activated = true;

        if(activatedVisual != null)
        {
            activatedVisual.SetActive(true);
        }

        Debug.Log($"{name} activated.");

        if (session != null)
        {
            session.RegisterNodeActivation(nodeId);
        }

        if (!string.IsNullOrEmpty(storyText) && textController != null)
        {
            textController.ShowText(storyText);
        }
    }
}
