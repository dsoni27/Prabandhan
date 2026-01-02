using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Parbandhan.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _errors = new();
        public bool HasErrors => _errors.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public IEnumerable GetErrors(string? propertyName)
            => _errors.ContainsKey(propertyName!) ? _errors[propertyName!] : Enumerable.Empty<string>();

        public void Validate(string propertyName, object propertyValue)
        {
            ClearErrors(propertyName);

            var results = new List<ValidationResult>();
            Validator.TryValidateProperty(propertyValue, new ValidationContext(this)
            {
                MemberName = propertyName
            }, results);

            if (results.Any())
            {
                _errors.Add(propertyName, results.Select(c => c.ErrorMessage!).ToList());
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
            else
            {
                _errors.Remove(propertyName);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        protected void AddError(string propertyName, string error)
        {
            if (!_errors.ContainsKey(propertyName))
                _errors[propertyName] = new List<string>();

            if (!_errors[propertyName].Contains(error))
            {
                _errors[propertyName].Add(error);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        protected void ClearErrors(string propertyName)
        {
            if (_errors.Remove(propertyName))
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected void SetProperty<T>(ref T field, T value, bool validate = false, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return;

            field = value;
            OnPropertyChanged(propertyName);

            if (validate)
                Validate(propertyName, value);
        }
    }
}
