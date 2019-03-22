using System;
using System.Collections.Generic;

namespace MG.PowerShell.DsReg
{
    public interface IDsRegParser
    {
        IEnumerable<BaseDetail> ParseFrom(string[] lines);
    }
}
