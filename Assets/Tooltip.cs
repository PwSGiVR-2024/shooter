using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    [SerializeField] LayerMask _layerMask;
    [SerializeField] TextMeshProUGUI _tooltip;
    [SerializeField] ItemContainerCallbacks _itemContainerCallbacks;

    private TooltipText _lookingAt;
    private int _itemContainerCount;

    private void Start()
    {
        _itemContainerCallbacks.OnItemContainerCountChanged += UpdateItemContainerCount;
    }

    private void UpdateItemContainerCount(int itemContainerCount)
    {
        _itemContainerCount = itemContainerCount;
        Debug.Log(_itemContainerCount);
    }

    void Update()
    {
        SelectGameObjectBeingLookedAt();

        if (_itemContainerCount < 2 && HasGameObjectTargeted())
        {
            _tooltip.gameObject.SetActive(true);
        }
        else
        {
            _tooltip.gameObject.SetActive(false);
        }
    }

    private bool HasGameObjectTargeted()
    {
        return _lookingAt != null;
    }

    private void SelectGameObjectBeingLookedAt()
    {
        Ray ray = Camera.main.ViewportPointToRay(Vector3.one / 2f);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 3f, _layerMask))
        {
            if (!hitInfo.collider.TryGetComponent<TooltipText>(out var hitObject))
            {
                _lookingAt = null;
            }
            else if (hitObject != _lookingAt)
            {
                _lookingAt = hitObject;
                _tooltip.text = hitObject.Text;
            }
        }
        else
        {
            _lookingAt = null;
        }
    }
}
