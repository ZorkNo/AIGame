using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.CoreGame;

namespace AIGame.Interfaces
{
    public interface IMap : IArea
    {
        Tuple<int, int> GetValidStartPosition(List<IUnit> units);
    }
}
