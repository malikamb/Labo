using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo.BLL.Exceptions
{
    public class TournamentException(string message) : Exception(message)
    {
    }
}
