using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.AI;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;

namespace AIGame.Interfaces
{

    public interface IUnit
    {
        string Name { get; set; }
        Tuple<int, int> Coordinates { get; set; }
        Direction Facing { get; set; }
        int Health { get; set; }
        Side Owner { get; set; }
        IAi Ai { get; set; }
        Sensor Sensor { get; set; }
        bool IsDead { get; }
        List<IOrder> LastOrders { get; set; }
        IOrder GetOrder();
        void UpdateSensor(IMap map);
        string Render();
    }
}
