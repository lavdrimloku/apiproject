using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Logs
{
    public interface ILog
    {
        void Information(string messasges);
        void Warning(string message);
        void Debug(string message);
        void Error(string message);

    }
}
