using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.Interfaces;

namespace AIGame.AI
{
    public class AiType
    {
        public Type Type;
        public string[] Args;

        public static AiType Create<T>() where T : BaseAi
        {
            return new AiType(typeof(T), (string[]) null);
        }
        public static AiType Create<T>(string[] args) where T : BaseAi
        {
            return new AiType(typeof(T), args);
        }
        private  AiType(Type type,string[] args)
        {
            Type = type;
            Args = args;
        }
    }
}
