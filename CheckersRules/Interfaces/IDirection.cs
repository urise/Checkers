using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CheckersRules.Interfaces
{
    public interface IDirection
    {
        int DirectionX { get; }
        int DirectionY { get; }
    }
}
