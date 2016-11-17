namespace AIGame.CoreGame.Orders
{
    public interface IOrder
    {
        void Execute(Unit unit, Map map);
        bool IsValid(Unit unit, Map map);
    }
}