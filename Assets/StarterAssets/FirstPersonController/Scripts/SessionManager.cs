using System;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    [Header("Task")]
    [SerializeField] private int totalNodes = 3;

    public int ActivatedCount { get; private set; }
    public bool SessionActive { get; private set; }

    private float sessionStartTime;
    private readonly List<NodeActivationEvent> activations = new();

    public event Action<int, int> OnProgressChanged;
    public event Action OnSessionCompleted;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BeginSession();
    }

    public void BeginSession()
    {
        SessionActive = true;
        ActivatedCount = 0;
        activations.Clear();
        sessionStartTime = Time.time;

        OnProgressChanged?.Invoke(ActivatedCount, totalNodes);
        Debug.Log("Session started.");
    }

    public void RegisterNodeActivation(string nodeId)
    {
        if(!SessionActive) return;

        ActivatedCount++;
        
        float t = Time.time - sessionStartTime;
        activations.Add(new NodeActivationEvent(nodeId, t));

        OnProgressChanged?.Invoke(ActivatedCount, totalNodes);

        if(ActivatedCount >= totalNodes)
        {
            SessionActive = false;
            Debug.Log("Session completed.");

            foreach (var a in activations)
                Debug.Log($"Activated {a.NodeId} as #{a.Order} at {a.SecondsSinceStart:0.00}s");
        
            OnSessionCompleted?.Invoke();
        }
    }

    [Serializable]
    public struct NodeActivationEvent
    {
        public string NodeId;
        public int Order;
        public float SecondsSinceStart;
        public NodeActivationEvent(string nodeId, float secondsSinceStart)
        {
            NodeId = nodeId;
            Order = 0;
            SecondsSinceStart = secondsSinceStart;
        }
    }
}
