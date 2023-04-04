using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Privileges
{
    public interface IAuditTrail : IAuditTrail<string>
    {
    }


    public interface IAuditTrail<TKey>
    {
        TKey Id { get; set; }
    }
}
