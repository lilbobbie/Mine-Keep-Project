using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField]
    private float _previewYOffset = 0.06f;
    [SerializeField]
    private bool _isTileable = false;

    [SerializeField]
    private GameObject _cellIndicator;
    private GameObject _previewObject;

    [SerializeField]
    private Material _previewMaterialPrefab;
    private Material _previewMaterialInstance;

    private Renderer _cellIndicatorRenderer;

    private void Start()
    {
        _previewMaterialInstance = new Material(_previewMaterialPrefab);
        _cellIndicator.SetActive(false);
        _cellIndicatorRenderer = _cellIndicator.GetComponentInChildren<Renderer>();
    }

    public void StartShowingPlacementPreview(GameObject prefab, Vector2Int size)
    {
        _previewObject = Instantiate(prefab);
        PreparePreview(_previewObject);
        PrepareCursor(size);
        _cellIndicator.SetActive(true);
    }

    private void PrepareCursor(Vector2Int size)
    {
        if(size.x > 0 || size.y > 0)
        {
            _cellIndicator.transform.localScale = new Vector3(size.x, 1, size.y);
            if (_isTileable)
            {
                _cellIndicatorRenderer.material.mainTextureScale = size;
            }
        }
    }

    private void PreparePreview(GameObject previewObject)
    {
        Renderer[] renderers = previewObject.GetComponentsInChildren<Renderer>();
        foreach(Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = _previewMaterialInstance;
            }
            renderer.materials = materials;
        }
    }

    public void StopShowingPlacementPreview()
    {
        _cellIndicator.SetActive(false);
        if(_previewObject != null)
        {
            Destroy(_previewObject);
        }
    }

    public void UpdatePosition(Vector3 position, bool isValid)
    {
        if(_previewObject != null)
        {
            MovePreview(position);
            ApplyFeedbackToPreview(isValid);
        }

        MoveCursor(position);
        ApplyFeedbackToCursor(isValid);
    }

    private void ApplyFeedbackToPreview(bool isValid)
    {
        Color c = isValid ? Color.white : Color.red;

        c.a = 0.7f;
        _previewMaterialInstance.color = c;
    }

    private void ApplyFeedbackToCursor(bool isValid)
    {
        Color c = isValid ? Color.white : Color.red;

        c.a = 0.5f;
        _cellIndicatorRenderer.material.color = c;
    }

    private void MoveCursor(Vector3 position)
    {
        _cellIndicator.transform.position = position;
    }

    private void MovePreview(Vector3 position)
    {
        _previewObject.transform.position = new Vector3(position.x, position.y + _previewYOffset, position.z);
    }

    public void StartShowingRemovePreview()
    {
        _cellIndicator.SetActive(true);
        PrepareCursor(Vector2Int.one);
        ApplyFeedbackToCursor(false);
    }
}
