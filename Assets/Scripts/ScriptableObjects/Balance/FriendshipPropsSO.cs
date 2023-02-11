using System.Collections.Generic;
using UnitsSystem.Misc;
using UnityEngine;

namespace ScriptableObjects.Balance
{
    [CreateAssetMenu(fileName = "New Friendship", menuName = "Friendship", order = 0)]
    public class FriendshipPropsSO : ScriptableObject
    {
        public List<UnitFriends> unitFriends;
    }

}