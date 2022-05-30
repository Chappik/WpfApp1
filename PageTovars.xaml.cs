using System;
using System.Collections.Generic;
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
using System.Collections;
using System.Collections.ObjectModel;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для PageTovars.xaml
    /// </summary>
    public partial class PageTovars : Page
    {
        public static TitleEmployeeEntities DataEntitiesTovars { get; set; }
        ObservableCollection<Tovars> ListTovars;
        private bool isDirty;
        public PageTovars()
        {
           
            DataEntitiesTovars = new TitleEmployeeEntities();
            InitializeComponent();
            ListTovars = new ObservableCollection<Tovars>();
            BorderFind.Visibility = Visibility.Hidden;
            DataGridTovars.IsReadOnly = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetTovarss();
            DataGridTovars.SelectedIndex = 0;

        }

        private void GetTovarss()
        {
            ListTovars.Clear();
            var employees = DataEntitiesTovars.Tovars;
            var queryEployee = from employee in employees
                               orderby employee.Demensions
                               select employee;
            foreach (Tovars emp in queryEployee)
            {
                ListTovars.Add(emp);
            }

            DataGridTovars.ItemsSource = ListTovars;
        }

        private void RewriteEmploee()
        {
            DataEntitiesTovars = new TitleEmployeeEntities();
            ListTovars.Clear();
            GetTovarss();

        }


        private void SaveCommandBinding_Executed(object sender, RoutedEventArgs e)
        {
            DataEntitiesTovars.SaveChanges();
            isDirty = true;
            DataGridTovars.IsReadOnly = true;
        }

        private void UndoCommandBinding_Executed(object sender, RoutedEventArgs e)
        {
            RewriteEmploee();
            DataGridTovars.IsReadOnly = true;
            isDirty = true;
        }

        private void AddCommandBinding_Executed(object sender, RoutedEventArgs e)
        {
            Tovars employee = Tovars.CreateTovars(-1, "пусто", "пусто", "пусто", 0, 0, 0, 0, "пусто", 0, 0);
            try
            {
                DataEntitiesTovars.Tovars.Add(employee);
                ListTovars.Add(employee);
                DataGridTovars.ScrollIntoView(employee);
                DataGridTovars.SelectedIndex = DataGridTovars.Items.Count - 1;
                DataGridTovars.IsReadOnly = false;
                isDirty = false;

            }
            catch (FormatException ex)
            {
                throw new ApplicationException("Ошибка добавления нового товара в контекст данных");
            }
        }

        private void EditCommandBinding_Executed(object sender, RoutedEventArgs e)
        {
            DataGridTovars.IsReadOnly = false;
            DataGridTovars.BeginEdit();
            isDirty = false;
        }

        private void DeleteCommandBinding_Executed(object sender,

            RoutedEventArgs e)
        {
            Tovars emp = DataGridTovars.SelectedItem as Tovars;
            if (emp != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить товар " + "?", "Предупреждение", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DataEntitiesTovars.Tovars.Remove(emp);
                    DataGridTovars.SelectedIndex = DataGridTovars.SelectedIndex == 0 ? 1 : DataGridTovars.SelectedIndex - 1;
                    ListTovars.Remove(emp);
                    DataEntitiesTovars.SaveChanges();
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
            ButtonFindTitle.IsEnabled = false;
            ComboBoxTitle.SelectedIndex = -1;
        }

        private void ButtonFindSurname_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ComboBoxTitle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonFindTitle.IsEnabled = true;            
        }

        private void ButtonFindTitle_Click(object sender, RoutedEventArgs e)
        {

            //string name = ComboBoxTitle.Text;
            //ListTovars.Clear();
            //var employees = DataEntitiesTovars.Tovars;
            //var queryBase = from employee in employees
            //                where employee.Complect == name
            //                select employee;
            //foreach (Tovars emp in queryBase)
            //{
            //    ListTovars.Add(emp);
            //}

            //if (ListTovars.Count() > 0)
            //{
            //    DataGridTovars.ItemsSource = ListTovars;
            //    ButtonFindTitle.IsEnabled = true;
            //}
            //else
            //    MessageBox.Show("Изготовитель " + name + " не найден", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            DataEntitiesTovars = new TitleEmployeeEntities();
            ListTovars.Clear();

            Manufact title = ComboBoxTitle.SelectedItem as Manufact;
            var employees = DataEntitiesTovars.Tovars;
            var queryTovars = from employee in employees
                              where employee.Id_N == title.Id
                              orderby employee.Id
                              select employee;
            foreach (Tovars emp in queryTovars)
            {
                ListTovars.Add(emp);
            }
            DataGridTovars.ItemsSource = ListTovars;
            if (ListTovars.Count == 0) { MessageBox.Show("Ничего не найдено!", "Уведомление", MessageBoxButton.OK); }
        }

        private void RefreshCommandBinding_Executed(object sender, RoutedEventArgs e)
        {
            RewriteEmploee();
            DataGridTovars.IsReadOnly = false;
            isDirty = true;
            BorderFind.Visibility = System.Windows.Visibility.Hidden;
        }

        private void ComboBoxTitle_SelectionChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
