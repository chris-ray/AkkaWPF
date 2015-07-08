namespace AkkaWPF.Shell
{
    using Akka.Actor;
    using AkkaWPF.Shared;
    using AkkaWPF.Shell.ViewModels;

    class AppActor : ReceiveActor
    {
        private readonly MainVM _vm;
        private IActorRef _uiActor;
        private IActorRef _moduleManagerActor;
        private IActorRef _incrementerActor;

        public AppActor(MainVM vm)
        {
            _vm = vm;
            Receive<Messages.RequestClose>(x => Sender.Tell(_moduleManagerActor.Ask(x).Result));
            Receive<Messages.IncrementerMessage>(x => _moduleManagerActor.Tell(x));
        }

        protected override void PreStart()
        {
            _uiActor = Context.ActorOf(Props.Create(() => new UIActor(Self, _vm)).WithDispatcher("synchronized-dispatcher"), "ui");
            _moduleManagerActor = Context.ActorOf(Props.Create(() => new ModuleManagerActor(_uiActor)), "mod");
            _incrementerActor = Context.ActorOf(Props.Create(() => new IncrementerActor(Self)));
        }
    }
}
