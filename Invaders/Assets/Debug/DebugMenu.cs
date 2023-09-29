using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>
/// This is a super inflexible class for holding the state of the debug menu UI.
/// Lots of tight coupling here, and I'd recommend looking into binding frameworks
/// for more flexibility at the cost of complexity.
/// </summary>
public class DebugMenu : MonoBehaviour
{
    [SerializeField] private UIDocument document;

    void OnEnable()
    {
        OnNewStatRequested += AddStat;

        // Do on enable to ensure reference is set up when registering to the channel
        statBoxContainer = document.rootVisualElement.Q<VisualElement>(k_stat_box_container);
    }

    void OnDisable()
    {
        OnNewStatRequested -= AddStat;
    }

    // Update is called once per frame
    void Update()
    {
        // Polling is pretty expensive since most of the time nothing changes that the UI cares about,
        // but it IS super simple to implement...
        foreach (StatTracker tracker in statBoxInfos.Keys)
        {
            tracker.UpdateStat();
        }
    }


    // NEW STUFF
    [SerializeField] private VisualTreeAsset statBox;
    private VisualElement statBoxContainer; 

    public struct StatBoxInfo
    {
        public StatBoxInfo(TemplateContainer box, StatTracker tracker)
        {
            this.box = box;
            this.tracker = tracker;
        }
        public TemplateContainer box;
        public StatTracker tracker;
    }
    private const string k_stat_title = "title";
    private const string k_stat_box_container = "stats-container";
    private Dictionary<StatTracker, StatBoxInfo> statBoxInfos = new Dictionary<StatTracker, StatBoxInfo>();
    public void AddStat(StatTracker tracker)
    {
        // Instantiate a new stat box
        // Assign its name based on the tracker name
        // Assign the tracker's element as a child of the stat box instance
        // Store a reference to the tracker (and its container) in some data structure (prob set) 
        //  -- this will allow us to update it from here and also remove it quickly

        TemplateContainer box = statBox.Instantiate();
        statBoxContainer.Add(box);
        box.Q<Label>(k_stat_title).text = tracker.GetStatName();
        box.Add(tracker.GetStatVisual());
        statBoxInfos[tracker] = new StatBoxInfo(box: box, tracker: tracker);
    }

    public void RemoveStat(StatTracker tracker)
    {
        // TODO: Validation
        statBoxContainer.Remove(statBoxInfos[tracker].box);
    }

    
    // Essentially create a global function that notifies the debug network. It's okay that it's global since
    // we're not really not modifying data, just sending messages. It's effectively a global message channel.
    private static event System.Action<StatTracker> OnNewStatRequested;
    public static void RequestNewStat(StatTracker tracker)
    {
        OnNewStatRequested?.Invoke(tracker);
    }

}
