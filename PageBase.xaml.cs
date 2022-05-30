using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для PageBase.xaml
    /// </summary>
    public partial class PageBase : Page
    {

        public static TitleEmployeeEntities DataEntitiesBase { get; set; }
        ObservableCollection<Base> ListBase;
        private bool isDirty;
        public PageBase()
        {
            DataEntitiesBase = new TitleEmployeeEntities();
            InitializeComponent();
            ListBase = new ObservableCollection<Base>();
            BorderFind.Visibility = System.Windows.Visibility.Hidden;
            DataGridBase.IsReadOnly = true;             
        }



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetBases();
            DataGridBase.SelectedIndex = 0;

        }

        private void GetBases()
        {
            ListBase.Clear();
            var employees = DataEntitiesBase.Base;
            var queryEployee = from employee in employees
                               orderby employee.Name
                               select employee;
            foreach (Base emp in queryEployee)
            {
                ListBase.Add(emp);
            }
            DataGridBase.ItemsSource = ListBase;
           

        }

        private void RewriteEmploee()
        {
            DataEntitiesBase = new TitleEmployeeEntities();
            ListBase.Clear();
            GetBases();

        }


        private void SaveCommandBinding_Executed(object sender, RoutedEventArgs e)
        {
            DataEntitiesBase.SaveChanges();
            isDirty = true;
            DataGridBase.IsReadOnly = true;
        }

        private void UndoCommandBinding_Executed(object sender, RoutedEventArgs e)
        {
            RewriteEmploee();
            DataGridBase.IsReadOnly = true;
            isDirty = true;
        }

        private void AddCommandBinding_Executed(object sender, RoutedEventArgs e)
        {
            Base employee = Base.CreateBase(-1, "не задано");
            try
            {
                DataEntitiesBase.Base.Add(employee);
                ListBase.Add(employee);
                DataGridBase.ScrollIntoView(employee);
                DataGridBase.SelectedIndex = DataGridBase.Items.Count - 1;
                DataGridBase.IsReadOnly = false;
                isDirty = false;

            }
            catch (FormatException ex)
            {
                throw new ApplicationException("Ошибка добавления нового изготовителя в контекст данных");
            }
        }

        private void EditCommandBinding_Executed(object sender, RoutedEventArgs e)
        {
            DataGridBase.IsReadOnly = false;
            DataGridBase.BeginEdit();
            isDirty = false;
        }

        private void DeleteCommandBinding_Executed(object sender,

            RoutedEventArgs e)
        {
            Base emp = DataGridBase.SelectedItem as Base;
            if (emp != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить изготовителя " + emp.Name.Trim() + "?", "Предупреждение", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DataEntitiesBase.Base.Remove(emp);
                    DataGridBase.SelectedIndex = DataGridBase.SelectedIndex == 0 ? 1 : DataGridBase.SelectedIndex - 1;
                    ListBase.Remove(emp);
                    DataEntitiesBase.SaveChanges();
                }

            }
            else
            {
                MessageBox.Show("Выберите строку для удаления");
            }
        }

        private void CloseProg(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void FindCommandBinding_Executed(object sender, RoutedEventArgs e)
        {
            BorderFind.Visibility = System.Windows.Visibility.Visible;
            isDirty = false;
        }

        private void TextBoxSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            ButtonFindSurname.IsEnabled = true;
        }

        private void ButtonFindSurname_Click(object sender, RoutedEventArgs e)
        {
            string name = TextBoxSurname.Text;
            DataEntitiesBase = new TitleEmployeeEntities();
            ListBase.Clear();
            var employees = DataEntitiesBase.Base;
            var queryBase = from employee in employees
                                where employee.Name == name
                                select employee;
            foreach (Base emp in queryBase)
            {
                ListBase.Add(emp);
            }
           
            if (ListBase.Count() > 0)
            {
                DataGridBase.ItemsSource = ListBase;
                ButtonFindSurname.IsEnabled = true;
            }
            else
                MessageBox.Show("Изготовитель " + name + " не найден", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void RefreshCommandBinding_Executed(object sender, RoutedEventArgs e)
        {
            RewriteEmploee();
            DataGridBase.IsReadOnly = false;
            isDirty = true;
            BorderFind.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
