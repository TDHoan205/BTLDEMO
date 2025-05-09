using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace prjBTLDemo.NET.Helpers
{
    public class BaseViewsModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}