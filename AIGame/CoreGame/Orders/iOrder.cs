namespace AIGame.CoreGame.Orders
{
    public interface iOrder
    {
        OrderType Type();
        void Execute(Unit unit, Map map);
        bool IsValid(Unit unit, Map map);
    }
}