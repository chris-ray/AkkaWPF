namespace AkkaWPF.Shared
{
    using Akka.Actor;

    public static class Messages
    {
        public class RequestClose { }

        public class IncrementerMessage
        {
            public readonly string Data;

            public IncrementerMessage(string data)
            {
                Data = data;
            }
        }

        public class InitModuleMessage
        {
            public string Name { get; set; }
            public IActorRef ModuleActor { get; set; }
            public object ViewModel { get; set; }

            public InitModuleMessage(string name, IActorRef moduleActor, object viewModel)
            {
                Name = name;
                ModuleActor = moduleActor;
                ViewModel = viewModel;
            }
        }
    }
}