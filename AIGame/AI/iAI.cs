using AIGame.CoreGame;
using AIGame.CoreGame.Orders;

namespace AIGame.AI
{
    public interface IAiType
    {
        IAi GetAi();
    }
    public interface IAi
    {
        IOrder GetOrder(Sensor sensor);
    }

}
