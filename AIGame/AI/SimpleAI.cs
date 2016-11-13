using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.CoreGame;

namespace AIGame.AI
{
    public class SimpleAiType : iAiType
    {
        public iAI getAi()
        {
            return new SimpleAI();
        }
    }
    public class SimpleAI:iAI
    {
        public UnitSensor unitSensor;
        public iOrder getOrder(UnitSensor _unitSensor)
        {
            unitSensor = _unitSensor;
            return new DoNothing();
        }
        
    }

}
