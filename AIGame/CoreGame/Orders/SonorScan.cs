using System;
using AIGame.Interfaces;

namespace AIGame.CoreGame.Orders
{
    public class SonorScan : IOrder
    {
        public void Execute(IUnit unit, IMap map)
        {
            //Måske er der mere elegant måde at gøre det her 
            unit.Sensor.HasScanned=true;
        }
        public bool IsValid(IUnit unit, IMap map)
        {
            return true;
        }

        public string Render()
        {
            return "";
        }
    }
}

