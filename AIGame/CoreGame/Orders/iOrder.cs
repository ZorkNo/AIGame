namespace AIGame.CoreGame.Orders
{
    public interface IOrder
    {
        OrderType Type();
        void Execute(Unit unit, Map map);
        bool IsValid(Unit unit, Map map);
    }
}