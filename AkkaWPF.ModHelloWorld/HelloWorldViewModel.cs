namespace AkkaWPF.ModHelloWorld.ViewModels
{
    using System.ComponentModel;

    public class HelloWorldVM : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName) { if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); }
        #endregion

        private string _Text;
        public string Text { get { return _Text; } set { _Text = value; RaisePropertyChanged("Text"); } }
    }
}