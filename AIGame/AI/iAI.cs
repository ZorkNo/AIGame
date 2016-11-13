using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.CoreGame;

namespace AIGame.AI
{
    public interface iAiType
    {
        iAI getAi();
    }
    public interface iAI
    {
        iOrder getOrder(UnitSensor _unitSensor);
    }

}
