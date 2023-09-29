using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class CapacityStatTracker : StatTracker
{
    [SerializeField] private string _name;
    [SerializeField] private Component _propertyOwner;
    [SerializeField] private string _currentValuePropertyName;
    [SerializeField] private string _maxValuePropertyName;

    private PropertyInfo _currentValueProperty;
    private PropertyInfo _maxValueProperty;

    private TemplateContainer _visual;
    private Label _currentValueLabel;
    private Label _maxValueLabel;

    private const string k_currentValue = "current-text";
    private const string k_maxValue = "max-text";

    private const string c_capacityStatTrackerPrefabResourcesPath = "CapacityValue";


    void Start()
    {
        // Don't do validation since these will be null if the property doesn't exist. We can give defined behavior if it is null (such as displaying "N/A").
        _currentValueProperty = _propertyOwner.GetType().GetProperty(_currentValuePropertyName);
        _maxValueProperty = _propertyOwner.GetType().GetProperty(_maxValuePropertyName);

        // NOTE: This is a very slow way to load an asset but was used for simplicity -- use AssetBundles for better performance
        _visual = Resources.Load<VisualTreeAsset>(c_capacityStatTrackerPrefabResourcesPath)?.Instantiate();

        _currentValueLabel = _visual.Q<Label>(k_currentValue);
        _maxValueLabel = _visual.Q<Label>(k_maxValue);

        DebugMenu.RequestNewStat(this);
    }
    public override string GetStatName()
    {
        return _name;
    }

    public override TemplateContainer GetStatVisual()
    {
        return _visual;
    }

    public override void UpdateStat()
    {
        _currentValueLabel.text = _currentValueProperty?.GetValue(_propertyOwner).ToString();
        _maxValueLabel.text = _maxValueProperty?.GetValue(_propertyOwner).ToString();
    }
}
