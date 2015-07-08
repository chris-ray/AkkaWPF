namespace AkkaWPF.ModHelloWorld
{
    using System;
    using System.ComponentModel;
    using Akka.Actor;
    using AkkaWPF.ModHelloWorld.ViewModels;
    using AkkaWPF.Shared;

    public class HelloWorldModule : ReceiveActor
    {
        readonly IActorRef _moduleManagerActor;
        readonly string _name;
        readonly HelloWorldVM _vm;

        public HelloWorldModule(IActorRef moduleManagerActor, string name)
        {
            _moduleManagerActor = moduleManagerActor;
            _name = name;
            _vm = new HelloWorldVM();

            Receive<Messages.RequestClose>(x => Sender.Tell(true, Self));
            Receive<string>(x => 
            {
                System.Diagnostics.Debug.WriteLine(System.Threading.Thread.CurrentThread.ManagedThreadId);
                if (x.Contains("5"))
                    throw new Exception("I cannot handle 'five' messages");
                _vm.Text = string.Format("Hello {0}", x);
            });
        }

        protected override void PreStart()
        {
            _moduleManagerActor.Tell(new Messages.InitModuleMessage(_name, Self, _vm));
        }
    }
}