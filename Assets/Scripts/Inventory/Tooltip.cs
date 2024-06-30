using StarterAssets;
using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    [SerializeField] LayerMask _layerMask;
    [SerializeField] TextMeshProUGUI _tooltip;
    [SerializeField] ItemContainerCallbacks _itemContainerCallbacks;

    private TooltipText _lookingAt;
    private int _itemContainerCount;

    private bool _axePicked = false;

    private void Start()
    {
        _itemContainerCallbacks.OnItemContainerCountChanged += UpdateItemContainerCount;
    }

    private void UpdateItemContainerCount(int itemContainerCount)
    {
        _itemContainerCount = itemContainerCount;
    }

    void Update()
    {
        SelectGameObjectBeingLookedAt();

        if (_itemContainerCount < 2 && HasGameObjectTargeted() && NoCutscene())
        {
            _tooltip.gameObject.SetActive(true);
        }
        else
        {
            _tooltip.gameObject.SetActive(false);
        }

        if (!_axePicked && Input.GetKeyDown(KeyCode.F) && _lookingAt != null)
        {
            if (_lookingAt.Text == "Press [F] to Pick Up")
            {
                _lookingAt.transform.parent.gameObject.SetActive(false);
                WeaponManager.Instance.UnlockWeapon("Fireaxe");
                Instantiate(GameObject.Find("ItemPrefabs").GetComponent<ItemPrefabs>().PickupSound, transform.position, transform.rotation);
                _axePicked = true;
            }
        }
    }

    private bool HasGameObjectTargeted()
    {
        return _lookingAt != null;
    }

    private void SelectGameObjectBeingLookedAt()
    {
        Ray ray = Camera.main.ViewportPointToRay(Vector3.one / 2f);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 2.5f, _layerMask))
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

    private bool NoCutscene()
    {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().enabled;
    }
}
