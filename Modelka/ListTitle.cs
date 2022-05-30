using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1.Modelka
{
    class ListTitle : INotifyPropertyChanged
    {
        TitleEmployeeEntities ctx = new TitleEmployeeEntities();

        //base = поставщик
        //мануфактура - НОМЕНКЛАТУРА
        private Tovars _selectedTovars;
        public ObservableCollection<Tovars> tovars { get; set; } = new ObservableCollection<Tovars>();
        public ObservableCollection<Tovars> Tovars
        {
            get { return tovars; }
            set
            {
                tovars = value;
                OnPropertyChanged();
            }
        }
        public Tovars SelectedTovars
        {
            get { return _selectedTovars; }
            set
            {
                _selectedTovars = value;
                OnPropertyChanged("SelectedTovars");
            }
        }




        public ObservableCollection<Base> postavhikss { get; set; }
        public ObservableCollection<Base> Bases
        {
            get { return postavhikss; }
            set
            {
                postavhikss = value;
                OnPropertyChanged();
            }
        }

        private Base _selectedBase;
        public Base SelectedPBase
        {
            get { return _selectedBase; }
            set
            {
                _selectedBase = value;
                OnPropertyChanged("SelectedPBase");
            }
        }






        public ObservableCollection<Manufact> nomenklatura { get; set; }
        public ObservableCollection<Manufact> Nomenklatura
        {
            get { return nomenklatura; }
            set
            {
                nomenklatura = value;
                OnPropertyChanged();
            }
        }


        private Manufact _selectednomenklatura;
        public Manufact SelectedNomenklatura
        {
            get { return _selectednomenklatura; }
            set
            {
                _selectednomenklatura = value;
                OnPropertyChanged("SelectedNomenklatura");
            }
        }
      
        public ListTitle()
        {
            postavhikss = new ObservableCollection<Base>();
            nomenklatura = new ObservableCollection<Manufact>();
          
            var postss = ctx.Database.SqlQuery<Base>("SELECT * FROM Base");
            foreach (var company in postss)
                postavhikss.Add(company);

            var tabll = ctx.Database.SqlQuery<Tovars>("SELECT * FROM Tovars");
            foreach (var company in tabll)
                Tovars.Add(company);

            var compss = ctx.Database.SqlQuery<Manufact>("SELECT * FROM Manufact");
            foreach (var company in compss)
                nomenklatura.Add(company);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
