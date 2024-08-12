using Pizza_Ordering_Application.Enums;
using Pizza_Ordering_Application.Interfaces;
using Pizza_Ordering_Application.Pizza_Folder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering_Application.Application_Models
{
    internal static class Helper
    {
        /*
         * Margherita,
        Pepperoni,
        Veggie,
        Hawaiian,
        BBQChicken,
        Supreme,
        Mediterranean,
        BuffaloChicken,
        FourCheese,
        PestoDelight,
        MeatLovers,
        SpinachAndFeta,
        MushroomMadness
        */
        public static IBuyable? ConvertToBuyable(BuyableType item)
        {
            switch(item)
            {
                case BuyableType.Margherita:
                    return new Margherita();
                case BuyableType.Pepperoni:
                    return new Pepperoni();
                case BuyableType.Veggie:
                    return new Veggie();
                case BuyableType.Hawaiian:
                    return new Hawaiian();
                case BuyableType.BBQChicken:
                    return new BBQChicken();
                case BuyableType.Supreme:
                    return new Supreme();
                case BuyableType.Mediterranean:
                    return new Mediterranean();
                case BuyableType.BuffaloChicken:
                    return new BuffaloChicken();
                case BuyableType.FourCheese:
                    return new FourCheese();
                case BuyableType.PestoDelight:
                    return new PestoDelight();
                case BuyableType.MeatLovers:
                    return new MeatLovers();
                case BuyableType.SpinachAndFeta:
                    return new SpinachAndFeta();
                case BuyableType.MushroomMadness:
                    return new MushroomMadness();
                default:
                    return null;
            }
        }
    }
}
