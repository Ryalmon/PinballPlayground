using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] Collider2D _placementAreaHitbox;
    private int _itemsBeingDragged = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Vector2 ClosestValidPlacementLocation(Vector2 inPos)
    {
        return Physics2D.ClosestPoint(inPos, _placementAreaHitbox);
    }

    public void IncreaseItemsBeingDragged()
    {
        _itemsBeingDragged++;
        GameplayManagers.Instance.UI.ShowPlacementRegion();
    }

    public void DecreaseItemsBeingDragged()
    {
        _itemsBeingDragged--;
    }

    public bool AreItemsBeingDragged()
    {
        if (_itemsBeingDragged > 0)
            return true;
        return false;
    }
}
