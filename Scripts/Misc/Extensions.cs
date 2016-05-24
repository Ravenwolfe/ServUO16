using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items
{
    public static class Extensions
    {
        public static int GetInsuranceCost(this Item item)
        {
            int cost = 600; // this handles old items, set items, etc

            if (item.GetType().IsAssignableFrom(typeof(Factions.FactionItem)))
                cost = 800;
            else if (Mobiles.GenericBuyInfo.BuyPrices.ContainsKey(item.GetType()))
                cost = Math.Min(800, Math.Max(10, Mobiles.GenericBuyInfo.BuyPrices[item.GetType()]));
            else if (item.LootType == LootType.Newbied)
                return 10;
            return cost;
        }
    }
}
