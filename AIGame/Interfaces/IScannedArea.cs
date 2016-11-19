using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGame.Interfaces
{
    public interface IScannedArea : IArea
    {
        void Initilize();
        void Initilize(IUnit unit, IMap map);
    }
}
