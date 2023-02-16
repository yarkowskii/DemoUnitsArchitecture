using Managers;
using UnitsSystem.Misc;


namespace CustomHelpers
{
    public static class Helpers
    {
        public static bool IsFriendly(this UnitType unitTypeA, UnitType unitTypeB)
        {
            return GameManager.Instance.friendshipProps.unitFriends.Find(u => u.unit == unitTypeA).friendsToMe
                .Contains(unitTypeB);
        }
    }
}