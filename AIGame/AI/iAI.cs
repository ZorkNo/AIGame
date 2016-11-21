using System;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;

namespace AIGame.AI
{
    public interface IAiType
    {
        IAi GetAi();
        void SetRandomGenerator(Random Rnd);
        string Name { get; }
    }
    public interface IAi
    {
        IOrder GetOrder(Sensor sensor);
    }

}
