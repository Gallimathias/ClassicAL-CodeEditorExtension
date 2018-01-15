using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.ComponentModel;

namespace AnZw.NavCodeEditor.Extensions
{
    public abstract class ObservableObject<TObjectType> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void CopyFrom(TObjectType source)
        {
            foreach (var property in GetType().GetProperties())
            {
                if (!property.CanWrite || !property.CanRead)
                    continue;

                property.SetValue(this, property.GetValue(source));
            }
        }

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
