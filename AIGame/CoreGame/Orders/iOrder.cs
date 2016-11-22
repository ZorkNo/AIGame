using AIGame.Interfaces;

namespace AIGame.CoreGame.Orders
{
    public interface IOrder
    {
        void Execute(IUnit unit, IMap map);
        bool IsValid(IUnit unit, IMap map);
        string Render();
    }
}