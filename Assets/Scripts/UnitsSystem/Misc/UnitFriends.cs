using System;
using System.Collections.Generic;

namespace UnitsSystem.Misc
{
    [Serializable]
    public class UnitFriends
    {
        public UnitType unit;
        public List<UnitType> friendsToMe;
    }
}