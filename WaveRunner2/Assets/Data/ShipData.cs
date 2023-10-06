
using UnityEngine;


[CreateAssetMenu(fileName = "New Ship", menuName = "Ships/Ship Data")]
public class ShipData : ScriptableObject
{
    public string shipName;
    public GameObject shipPrefab;
}
