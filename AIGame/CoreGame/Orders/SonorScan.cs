using System;

namespace AIGame.CoreGame.Orders
{
    public class SonorScan : IOrder
    {
        public void Execute(Unit unit, Map map)
        {
            //Måske er der mere elegant måde at gøre det her 
            unit.Sensor.HasScanned=true;
        }
        public bool IsValid(Unit unit, Map map)
        {
            return true;
        }
    }
}

