using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Resto.Front.Api.IRestoDeliveryScreen
{
    public class MainClass : IDisposable
    {
        private Thread windowThread;
        public MainClass()
        {
            PluginContext.Operations.AddButtonToPluginsMenu("IRestoDeliveryScreen: Открыть", x => new Window1().ShowDialog());
            //windowThread = new Thread(EntryPoint);
            //windowThread.SetApartmentState(ApartmentState.STA);
            //windowThread.Start();
        }
        public void EntryPoint()
        {
            //while(true)
            //{
            //    Thread.Sleep(10000);
                
            //}
        }
        public void Dispose()
        {

        }
    }
}
