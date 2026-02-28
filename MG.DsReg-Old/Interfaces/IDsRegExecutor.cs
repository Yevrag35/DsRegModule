using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace MG.DsReg
{
    public interface IDsRegExecutor
    {
        IDsRegResult GetStatus();
    }
}
