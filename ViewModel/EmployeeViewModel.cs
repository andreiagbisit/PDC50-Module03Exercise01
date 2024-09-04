using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Module02Exercise01.Model;

using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Module02Exercise01.ViewModel
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        //Role of ViewModel
        private Employee _employee;

        private string _fullName; //variable for data conversion
        private string _status; //variable for data conversion

        public EmployeeViewModel()
        {
            //Initialize a sample student model. Coordination with Model
            _employee = new Employee { FirstName = "Amuro", LastName = "Ray", Position = "Manager", Department = "Management", IsActive=true };

            //UI Thread Management
            LoadEmployeeDataCommand = new Command(async () => await LoadEmployeeDataAsync());

            //Collections
            Employees = new ObservableCollection<Employee>()
            {
                new Employee {FirstName = "Kamille", LastName = "Bidan", Position = "Secretary", Department = "Finance", IsActive=false},
                new Employee {FirstName = "Judau", LastName = "Ashta", Position = "PRO", Department = "Customer Service", IsActive=true}
            };
        }

        //Setting collections in public
        public ObservableCollection<Employee> Employees { get; set; }


        public string FullName
        {
            get => _fullName;
            set
            {
                if (_fullName != value)
                {
                    _fullName = value;
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        //UI Thread Management
        public ICommand LoadEmployeeDataCommand { get; }

        //Two-way Data Binding and Data Conversion

        private async Task LoadEmployeeDataAsync()
        {
            await Task.Delay(1000); // I/O Operation
            FullName = $"{_employee.FirstName} {_employee.LastName}"; //Data
            Status = $"{_employee.Position} {_employee.Department} {_employee.IsActive}"; //Data
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
