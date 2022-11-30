using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Resto.Front.Api;
using System.Diagnostics;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Controls;

namespace Resto.Front.Api.IRestoDeliveryScreen
{
    using static PluginContext;
    public class Window1 : IDisposable
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out Rectangle rectangle);
        [StructLayout(LayoutKind.Sequential)]
        private struct Rectangle
        {
            public readonly int Left;
            public readonly int Top;
            public readonly int Right;
            public readonly int Bottom;
        }

        private const string AppProcessName = "iikoFront.Net";
        private Window window;

        private void EntryPoint()
        {
            var button = new Button { Content = "Close" };
            button.Click += (s, e) => Close();
            window = new Window
            {
                // ваши произвольные свойства
                // вы можете вообще создать своё окно
                Content = button,
                // далее обязательные для функционирования свойства, 
                // при реализации своего окна их нужно перенести
                Topmost = true,
                ResizeMode = ResizeMode.NoResize,
                ShowInTaskbar = false,
                WindowStartupLocation = WindowStartupLocation.Manual,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                WindowStyle = WindowStyle.None,
                ShowActivated = true,
                Top = 0,
                Left = 0,
            };

            window.Loaded += (sender, args) =>
            {
                var runningProcesses = Process.GetProcessesByName(AppProcessName).SingleOrDefault();
                if (runningProcesses == null)
                    return;
                var frontHwnd = runningProcesses.MainWindowHandle;
                GetWindowRect(frontHwnd, out var rectangle);
                window.Height = rectangle.Bottom - rectangle.Top;
                window.Width = rectangle.Right - rectangle.Left;
                var currentHwnd = new WindowInteropHelper(window).Handle;
                SetParent(currentHwnd, frontHwnd);
            };

            window.ShowDialog();
        }

        private void Close()
        {
            window.Close();
            Dispose();
        }

        public void Dispose()
        {
            window.Dispatcher.InvokeShutdown();
            window.Dispatcher.Thread.Join();
        }

        public void ShowDialog()
        {
            var windowThread = new Thread(EntryPoint);
            windowThread.SetApartmentState(ApartmentState.STA);
            windowThread.Start();
        }
        public Window1()
        {
            Log.Info("Window1 created");
        }
    }
}
