using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;

namespace AIGame.AI
{
    public interface iAiType
    {
        iAI GetAi();
    }
    public interface iAI
    {
        IOrder GetOrder(Sensor sensor);
    }

}
