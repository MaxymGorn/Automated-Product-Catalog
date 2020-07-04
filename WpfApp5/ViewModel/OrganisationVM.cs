using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5.ViewModel
{
    class OrganisationVM: INotifyPropertyChanged
    {

    public event PropertyChangedEventHandler PropertyChanged;
    private WpfApp5.Models.Organisation vm;
    public OrganisationVM(WpfApp5.Models.Organisation vm) => this.vm = vm;



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
