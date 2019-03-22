using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace MG.PowerShell.DsReg
{
    public interface IDsRegExecutor
    {
        IDsRegParser Parser { get; }
        IEnumerable<BaseDetail> GetAllStatus();
        BaseDetail GetSpecific(Type type);
    }
}
