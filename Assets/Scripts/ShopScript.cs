using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public GroundPlacementController objPC;

    public int i = 0;

    public void PurchaseCannonTurret()
    {
        Debug.Log("Cannon Turret Purchased");
        if (objPC)
        {
            i = 0;
            objPC.PlacePrefab(i);
        }
    }

    public void PurchaseRepeaterTurret()
    {
        Debug.Log("Repeater Turret Purchased");
        if (objPC)
        {
            i = 2;
            objPC.PlacePrefab(i);
        }
    }

    public void PurchaseLaserTurret()
    {
        Debug.Log("Laser Turret Purchased");
        if (objPC)
        {
            i = 1;
            objPC.PlacePrefab(i);
        }
    }

}