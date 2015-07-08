namespace AkkaWPF.Shell.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using Akka.Actor;

    public class MainVM : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName) { if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); }
        #endregion

        private object _VM;
        public object VM { get { return _VM; } set { _VM = value; RaisePropertyChanged("VM"); } }
    }

    public class MainVM2 : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName) { if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); }
        #endregion

        readonly IActorRef _appActor;
        private ObservableCollection<ModuleVM> _Modules;
        public IEnumerable<object> Modules { get { return _Modules; } }

        public MainVM2(IActorRef appActor)
        {
            _appActor = appActor;
            _Modules = new ObservableCollection<ModuleVM>();
        }

        public void SetModule(string name, object viewModel)
        {
            var mod = _Modules.FirstOrDefault(x => x.Name == name);
            if (mod == null)
            {
                mod = new ModuleVM(name, viewModel);
                _Modules.Add(mod);
            }
            else
                mod.VM = viewModel;
        }
    }

    public class ModuleVM : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName) { if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); }
        #endregion

        public readonly string Name;

        private object _VM;
        public object VM { get { return _VM; } set { _VM = value; RaisePropertyChanged("VM"); } }

        public ModuleVM(string name, object vm)
        {
            Name = name;
            _VM = vm;
        }
    }
}