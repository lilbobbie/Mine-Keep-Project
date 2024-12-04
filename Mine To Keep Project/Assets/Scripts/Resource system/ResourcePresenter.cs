using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ResourcePresenter : MonoBehaviour
{
    [SerializeField]
    private GameObject _UIObject;

    private List<TMP_Text> _UIDisplays = new();

    private void Start()
    {
        TMP_Text[] UIText = _UIObject.GetComponentsInChildren<TMP_Text>(); 
        foreach (TMP_Text UIDisplay in UIText)
        {
            if (UIDisplay.transform.parent != _UIObject.transform)
                continue;
            _UIDisplays.Add(UIDisplay);
        }
    }

    public void UpdateResourceDisplay(ResourceType type)
    {
        var stringName = type.ToString() + " Counter";

        foreach (var text in _UIDisplays)
        {
            // for this to work make sure UI resource counters are named like above; "Type Counter"; ie: "Timber Counter"
            if (stringName.ToLower() == text.gameObject.name.ToLower())
            {
                text.text = type.ToString() + ": " + ResourceController.Instance.GetResourceAmount(type);
            }
        }
    }
}
