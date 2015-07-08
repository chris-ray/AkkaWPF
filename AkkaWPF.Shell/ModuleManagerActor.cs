namespace AkkaWPF.Shell
{
    using System.Collections.Generic;
    using Akka.Actor;
    using AkkaWPF.Shared;

    class ModuleManagerActor : ReceiveActor
    {
        readonly IActorRef _uiActor;
        readonly Dictionary<string, IActorRef> _modules;

        public ModuleManagerActor(IActorRef uiActor)
        {
            _uiActor = uiActor;
            _modules = new Dictionary<string, IActorRef>();

            Receive<Messages.IncrementerMessage>(x =>
            {
                foreach (var mod in _modules)
                    mod.Value.Tell(x.Data);
            });

            Receive<Messages.InitModuleMessage>(x =>
            {
                _modules[x.Name] = x.ModuleActor;
                _uiActor.Tell(x);
            });

            Receive<Messages.RequestClose>(x =>
            {
                bool result = true;
                foreach (var m in _modules)
                    if (!m.Value.Ask<bool>(x).Result)
                        result = false;
                Sender.Tell(result);
            });
        }

        protected override void PreStart()
        {
            for (int i = 0; i < 10; i++)
            {
                Context.System.ActorOf(Props.Create(() => new AkkaWPF.ModHelloWorld.HelloWorldModule(Self, "helloWorld" + i)));
            }
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(x =>
            {
                return Directive.Stop;
            });
        }
    }
}