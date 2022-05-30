using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Modelka
{
    class ListTitle3 : ObservableCollection<Base> //получаем список изготовителей
    {
        public ListTitle3()
        {
            DbSet<Base> titles = PageTovars.DataEntitiesTovars.Base;
            var queryTitle = from title in titles select title;
            foreach (Base titl in queryTitle)
                this.Add(titl);
        }
    }
}
