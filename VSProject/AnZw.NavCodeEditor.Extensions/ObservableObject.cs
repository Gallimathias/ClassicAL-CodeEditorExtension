using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.ComponentModel;

namespace AnZw.NavCodeEditor.Extensions
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        
        protected bool SetProperty<T>(ref T propertyValue, T newValue, [CallerMemberName]string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(propertyValue, newValue))
                return false;

            propertyValue = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }

    }
}
