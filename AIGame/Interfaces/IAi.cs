using AIGame.CoreGame;
using AIGame.CoreGame.Orders;

namespace AIGame.Interfaces
{
    public interface IAi
    {
        IOrder GetOrder(Sensor sensor);
    }
}