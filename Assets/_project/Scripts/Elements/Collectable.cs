using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public CollectableType collectableType;

}

public enum CollectableType
{
    Coin,
    Serum,
    Heal,
    AttackSpeed,
    Damage,
}
