using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.ShaderKeywordFilter;
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

    // Components we'd like to look at for stats
    [SerializeField] private HealthStat health;
    [SerializeField] private UnitMovement movement;
    [SerializeField] private ProjectileLauncher launcher;

    // References to visual elements and their tags
    private Label currentHealthText;
    private Label maxHealthText;
    private Label moveSpeedText;
    private Label fireRateText;
    private Label currentProjectileText;
    private Label currentProjectileDamageText;

    const string k_currentHealthText = "current-health-text";
    const string k_maxHealthText = "max-health-text";
    const string k_moveSpeedText = "move-speed-text";
    const string k_fireRateText = "fire-rate-text";
    const string k_currentProjectileText = "current-projectile-text";
    const string k_currentProjectileDamageText = "current-projectile-damage-text";

    void Start()
    {
        currentHealthText = document.rootVisualElement.Q<Label>(k_currentHealthText);
        maxHealthText = document.rootVisualElement.Q<Label>(k_maxHealthText);
        moveSpeedText = document.rootVisualElement.Q<Label>(k_moveSpeedText);
        fireRateText = document.rootVisualElement.Q<Label>(k_fireRateText);
        currentProjectileText = document.rootVisualElement.Q<Label>(k_currentProjectileText);
        currentProjectileDamageText = document.rootVisualElement.Q<Label>(k_currentProjectileDamageText);
    }

    // Update is called once per frame
    void Update()
    {
        // Polling is pretty expensive since most of the time nothing changes that the UI cares about,
        // but it IS super simple to implement...
        currentHealthText.text = health?.CurrentHealth.ToString() ?? "N/A";
        maxHealthText.text = health?.MaxHealth.ToString() ?? "N/A";
        moveSpeedText.text = movement?.Speed.ToString() ?? "N/A";
        fireRateText.text = launcher?.ShotsPerSecond.ToString() ?? "N/A";
        currentProjectileText.text = launcher?.Pool?.ProjectilePrefab?.name ?? "N/A";
        currentProjectileDamageText.text = launcher?.Pool?.ProjectilePrefab?.Damage.ToString() ?? "N/A";

    }

    //public void AddStat(object obj, string propertyName, string statName)
    //{
    //    PropertyInfo info = obj.GetType().GetProperty(propertyName);
    //    if(info != null)
    //    {
    //        fireRateText.text = info.GetValue(obj).ToString();
    //    }
    //}
}
