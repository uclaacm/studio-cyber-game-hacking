using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitsStatTracker : StatTracker
{
    [SerializeField] private string _name;
    [SerializeField] private Component _propertyOwner;
    [SerializeField] private string _valuePropertyName;
    [SerializeField] private string _units;

    private PropertyInfo _currentValueProperty;

    private TemplateContainer _visual;
    private Label _valueLabel;

    private const string k_value = "text";
    private const string k_unitsValue = "units";

    private const string c_unitsStatTrackerPrefabResourcesPath = "UnitsValue";


    void Start()
    {
        // Don't do validation since these will be null if the property doesn't exist. We can give defined behavior if it is null (such as displaying "N/A").
        _currentValueProperty = _propertyOwner.GetType().GetProperty(_valuePropertyName);

        // NOTE: This is a very slow way to load an asset but was used for simplicity -- use AssetBundles for better performance
        _visual = Resources.Load<VisualTreeAsset>(c_unitsStatTrackerPrefabResourcesPath)?.Instantiate();

        _valueLabel = _visual.Q<Label>(k_value);

        _visual.Q<Label>(k_unitsValue).text = _units;

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
        _valueLabel.text = _currentValueProperty?.GetValue(_propertyOwner).ToString();
    }
}
