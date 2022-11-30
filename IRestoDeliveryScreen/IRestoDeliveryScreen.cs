using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting;
using Resto.Front.Api;
using Resto.Front.Api.Attributes;
using Resto.Front.Api.Attributes.JetBrains;

namespace Resto.Front.Api.IRestoDeliveryScreen
{
    [UsedImplicitly]
    [PluginLicenseModuleId(21016318)]
    public class IRestoDeliveryScreen : IFrontPlugin
    {
        private readonly Stack<IDisposable> subscriptions = new Stack<IDisposable>();

        public IRestoDeliveryScreen()
        {
            PluginContext.Log.Info("IRestoDeliveryScreen launched");
            subscriptions.Push(new MainClass());

        }

        public void Dispose()
        {
            while (subscriptions.Any())
            {
                var subscription = subscriptions.Pop();
                try
                {
                    subscription.Dispose();
                }
                catch (RemotingException)
                {
                    // nothing to do with the lost connection
                }
            }

            PluginContext.Log.Info("IRestoTroubleshooter stopped");
        }
    }
}
