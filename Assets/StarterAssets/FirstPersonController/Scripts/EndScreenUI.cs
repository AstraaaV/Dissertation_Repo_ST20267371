using UnityEngine;

public class EndScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject endScreenPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (endScreenPanel != null)
        {
            endScreenPanel.SetActive(false);
        }

        var session = FindObjectOfType<SessionManager>();
        if(session != null)
            session.OnSessionCompleted += ShowEndScreen;
        else
            Debug.LogWarning("EndScreenUI: No SessionManager found in the scene.");
    }

    private void ShowEndScreen()
    {
        if (endScreenPanel != null)
        {
            endScreenPanel.SetActive(true);
        }

        Debug.Log("EndScreenUI: Session completed, showing end screen.");
    }
}
