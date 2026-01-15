using UnityEditor.SearchService;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    [SerializeField] private int totalNodes = 3;
    private int activatedNodes = 0;

    public void RegisterActivation()
    {
        activatedNodes++;
        Debug.Log($"Activated {activatedNodes}/{totalNodes})");

        if(activatedNodes >= totalNodes)
        {
            EndSession();
        }
    }

    private void EndSession()
    {
        Debug.Log("Session complete.");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
