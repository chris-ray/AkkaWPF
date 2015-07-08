namespace AkkaWPF.Shell
{
    using System.Windows;
    using Akka.Actor;
    using AkkaWPF.Shell.ViewModels;

    public partial class App : Application
    {
        internal static MainVM mainvm;
        internal static ActorSystem actorSystem;
        internal static IActorRef appActor;

        protected override void OnStartup(StartupEventArgs e)
        {
            mainvm = new MainVM();
            actorSystem = ActorSystem.Create("sys");
            appActor = actorSystem.ActorOf(Props.Create(() => new AppActor(mainvm)), "app");
            base.OnStartup(e);
        }
    }
}