using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;

namespace AIGame.AI
{
    public class FireAllTheTimeAiType : IAiType
    {
        public IAi GetAi(Random rnd)
        {
            return new FireAllTheTimeAi(rnd);
        }
        public string Name
        {
            get { return "FireAllTheTimeAi"; }
        }
    }
    public class FireAllTheTimeAi : IAi
    {
        public Random Rnd;

        public FireAllTheTimeAi(Random rnd)
        {
            Rnd = rnd;
        }
        public IOrder GetOrder(Sensor sensor)
        {
            return new FireTorpedo(getCoordinates()); ;
        }
        private Tuple<int, int> getCoordinates()
        {
            int rndX = Rnd.Next(1, 6);
            int rndY = Rnd.Next(1, 4);
            return new Tuple<int, int>(rndX - 2, rndY);
        }
    }
}
