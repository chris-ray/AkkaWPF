namespace AkkaWPF.Shell
{
    using Akka.Actor;
    using AkkaWPF.Shared;
    using AkkaWPF.Shell.ViewModels;

    class UIActor : ReceiveActor
    {
        private readonly IActorRef _appActor;
        private MainVM _vm;
        private MainVM2 _mainvm;

        public UIActor(IActorRef appActor, MainVM vm)
        {
            _appActor = appActor;
            _vm = vm;
            _mainvm = new MainVM2(_appActor);
            _vm.VM = _mainvm;
            Receive<Messages.InitModuleMessage>(x => Handle(x));
        }

        private void Handle(Messages.InitModuleMessage x)
        {
            _mainvm.SetModule(x.Name, x.ViewModel);
        }
    }
}
