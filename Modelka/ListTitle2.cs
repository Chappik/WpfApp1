using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Modelka
{
    class ListTitle2 : ObservableCollection<Manufact> //получаем список номенклатуры
    {
        public ListTitle2()
        {
            DbSet<Manufact> titles = PageTovars.DataEntitiesTovars.Manufact;
            var queryTitle = from title in titles select title;
            foreach (Manufact titl in queryTitle)
                this.Add(titl);
        }
    }
}
