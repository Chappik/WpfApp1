//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Manufact
    {

        static Manufact emp;




        public static Manufact CreateManufact(int Id, string Name)
        {
            emp = new Manufact();
            emp.Id = Id;
            emp.Name = Name;
            return emp;
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual Tovars Tovars { get; set; }
    }
}
