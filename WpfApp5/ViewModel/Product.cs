using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApp5.Models;
namespace WpfApp5.ViewModel
{
    class Product : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private WpfApp5.Models.Product vm;
        public Product(WpfApp5.Models.Product vm) => this.vm = vm;  

        public Product()
        {
        }

        public void OnPropertyChanged([CallerMemberName]string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        public int Id
        {
            get => vm.Id;
            set
            {
                vm.Id = value;
                OnPropertyChanged("Id");
            }
        }

        public int  Count
        {
            get => vm.Count;
            set
            {
                vm.Count = value;
                OnPropertyChanged("Count");
            }
        }

        public decimal Cost
        {
            get => vm.Cost;
            set
            {
                vm.Cost = value;
                OnPropertyChanged("Cost");
            }
        }

        public int Category
        {
            get => vm.CategoryId;
            set
            {
                vm.CategoryId = value;
                OnPropertyChanged("Category");
            }
        }



        public int Organisation
        {
            get => vm.OrganisationId;
            set
            {
                vm.OrganisationId = value;
                OnPropertyChanged("Organisation");
            }
        }

        public string Name
        {
            get => vm.Name;
            set
            {
                vm.Name = value;
                OnPropertyChanged("Name");
            }
        }

    }
}
