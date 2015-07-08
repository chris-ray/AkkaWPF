namespace AkkaWPF.Shell
{
    using Akka.Actor;
    using AkkaWPF.Shared;

    class IncrementerActor : ReceiveActor
    {
        static int i = 0;
        readonly IActorRef _appActor;

        public IncrementerActor(IActorRef appActor)
        {
            _appActor = appActor;

            Receive<string>(x =>
            {
                if (Sender == Self)
                    _appActor.Tell(new Messages.IncrementerMessage((i++).ToString()));
            });
        }

        protected override void PreStart()
        {
            Context.System.Scheduler.ScheduleTellRepeatedly(0, 250, Self, string.Empty, Self);
        }
    }
}
