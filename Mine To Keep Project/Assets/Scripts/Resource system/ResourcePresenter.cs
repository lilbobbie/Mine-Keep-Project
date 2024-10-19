using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ResourcePresenter : MonoBehaviour
{
    [SerializeField]
    private List<TMP_Text> _UIDisplays;

    public void UpdateResourceDisplay(ResourceType type)
    {
        var stringName = type.ToString() + " Counter";

        foreach (var text in _UIDisplays)
        {
            // for this to work make sure UI resource counters are named like above; "Type Counter"; ie: "Timber Counter"
            if (stringName == text.gameObject.name)
            {
                text.text = type.ToString() + ": " + ResourceManager.Instance.GetResourceAmount(type);
            }
        }
    }
}
