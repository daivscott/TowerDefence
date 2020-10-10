using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public PlacementController objPC;

    //
    public void PurchaseCannonTurret()
    {
        Debug.Log("Turret Purchased");
        if (objPC)
        {
            objPC.PlacePrefab();
        }
    }

}