using Manager.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Manager
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Browser> Browsers 
        {
            get { return _Browsers; }
            set
            {
                _Browsers = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Browser> _Browsers { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
