using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace MyCarApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
                    }

      // protected override Window CreateWindow(IActivationState? activationState)
       // {
          //  return new Window(new AppShell());
       // }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;

            Debug.WriteLine("🔥 UNHANDLED EXCEPTION");
            Debug.WriteLine(ex?.Message);
            Debug.WriteLine(ex?.StackTrace);
        }

        private void OnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            Debug.WriteLine("🔥 UNOBSERVED TASK EXCEPTION");
            Debug.WriteLine(e.Exception.Message);
            Debug.WriteLine(e.Exception.StackTrace);

            e.SetObserved();
        }
    }
}