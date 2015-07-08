namespace AkkaWPF.Shell
{
    using System;
    using System.Windows;
    using Akka.Actor;
    using AkkaWPF.Shared;
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Closing += MainWindow_Closing;
            DataContext = App.mainvm;
        }

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (!App.appActor.Ask<bool>(new Messages.RequestClose()).Result)
            //    e.Cancel = true;
            App.actorSystem.Shutdown();
        }
    }
}