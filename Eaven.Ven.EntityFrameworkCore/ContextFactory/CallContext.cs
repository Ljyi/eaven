using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Eaven.Ven.EntityFrameworkCore.ContextFactory
{
    public class CallContext
    {
        static ConcurrentDictionary<string, AsyncLocal<DbContext>> state = new ConcurrentDictionary<string, AsyncLocal<DbContext>>();

        public static void SetData(WriteAndRead name, DbContext data) =>state.GetOrAdd(name.ToString(), _ => new AsyncLocal<DbContext>()).Value = data;

        public static DbContext GetData(WriteAndRead name) =>state.TryGetValue(name.ToString(), out AsyncLocal<DbContext> data) ? data.Value : null;
    }
}
