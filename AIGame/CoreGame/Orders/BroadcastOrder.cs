using System;
using AIGame.Interfaces;

namespace AIGame.CoreGame.Orders
{
    public class BroadcastOrder : IOrder
    {
        private Broadcast _broadcast;
        public BroadcastOrder(Broadcast broadcast)
        {
            _broadcast = broadcast;
        }
        public void Execute(IUnit unit, IMap map)
        {

            map.SignalOrigins.Add( new SignalOrigin(unit, _broadcast));
        }
        public bool IsValid(IUnit unit, IMap map)
        {
            return _broadcast != null;
        }

        public string Render()
        {
            return _broadcast.Message + ":" + _broadcast.Type;
        }

        public bool FreeOrder()
        {
            return true;
        }
    }


}

