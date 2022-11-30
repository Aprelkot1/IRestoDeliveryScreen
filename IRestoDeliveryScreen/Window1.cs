using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resto.Front.Api;
namespace Resto.Front.Api.IRestoDeliveryScreen
{
    using static PluginContext;
    public class Window1 : IDisposable
    {
        public Window1()
        {
            Log.Info("Window1 created");
        }
        public void Dispose()
        {
            Log.Info("Window1 disposed");
        }
    }
}
