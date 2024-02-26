using UnityEngine;

public class PickUp : MonoBehaviour
{
    public void OnPickedUp()
    {
        //add points
        Destroy(this.gameObject);
    }
}
