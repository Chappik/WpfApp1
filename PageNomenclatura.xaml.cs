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
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class PageNomenclatura : Page
    {
        
        public static TitleEmployeeEntities DataEntitiesManufact { get; set; }
        ObservableCollection<Manufact> ListManufact;
        private bool isDirty;
        public PageNomenclatura()
        {
            DataEntitiesManufact = new TitleEmployeeEntities();
            InitializeComponent();
            ListManufact = new ObservableCollection<Manufact>();
            BorderFind.Visibility = System.Windows.Visibility.Hidden;
            DataGridManufact.IsReadOnly = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetManufacts();
            DataGridManufact.SelectedIndex = 0;

        }

        private void GetManufacts()
        {
            ListManufact.Clear();
            var employees = DataEntitiesManufact.Manufact;
            var queryEployee = from employee in employees
                               orderby employee.Name
                               select employee;
            foreach (Manufact emp in queryEployee)
            {
                ListManufact.Add(emp);
            }
            DataGridManufact.ItemsSource = ListManufact;

        }

        private void RewriteEmploee()
        {
            DataEntitiesManufact = new TitleEmployeeEntities();
            ListManufact.Clear();
            GetManufacts();

        }


        private void SaveCommandBinding_Executed(object sender, RoutedEventArgs e)
        {
            DataEntitiesManufact.SaveChanges();
            isDirty = true;
            DataGridManufact.IsReadOnly = true;
        }

        private void UndoCommandBinding_Executed(object sender, RoutedEventArgs e)
        {
            RewriteEmploee();
            DataGridManufact.IsReadOnly = true;
            isDirty = true;
        }

        private void AddCommandBinding_Executed(object sender, RoutedEventArgs e)
        {
            Manufact employee = Manufact.CreateManufact(-1, "не задано");
            try
            {
                DataEntitiesManufact.Manufact.Add(employee);
                ListManufact.Add(employee);
                DataGridManufact.ScrollIntoView(employee);
                DataGridManufact.SelectedIndex = DataGridManufact.Items.Count - 1;
                DataGridManufact.IsReadOnly = false;
                isDirty = false;

            }
            catch (FormatException ex)
            {
                throw new ApplicationException("Ошибка добавления новой номенклатуры в контекст данных");
            }
        }

        private void EditCommandBinding_Executed(object sender, RoutedEventArgs e)
        {
            DataGridManufact.IsReadOnly = false;
            DataGridManufact.BeginEdit();
            isDirty = false;
        }

        private void DeleteCommandBinding_Executed(object sender,

            RoutedEventArgs e)
        {
            Manufact emp = DataGridManufact.SelectedItem as Manufact;
            if (emp != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить номенклатуру " + emp.Name.Trim() + "?", "Предупреждение", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DataEntitiesManufact.Manufact.Remove(emp);
                    DataGridManufact.SelectedIndex = DataGridManufact.SelectedIndex == 0 ? 1 : DataGridManufact.SelectedIndex - 1;
                    ListManufact.Remove(emp);
                    DataEntitiesManufact.SaveChanges();
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
            DataEntitiesManufact = new TitleEmployeeEntities();
            ListManufact.Clear();
            var employees = DataEntitiesManufact.Manufact;
            var queryManufact = from employee in employees
                                where employee.Name == name
                                select employee;
           
            foreach (Manufact emp in queryManufact)
            {
                ListManufact.Add(emp);
            }
            if (ListManufact.Count > 0)
            {
                DataGridManufact.ItemsSource = ListManufact;
                ButtonFindSurname.IsEnabled = true;
            }
            else
                MessageBox.Show("Номенклатура " + name + " не найдена", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ComboBoxTitle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonFindSurname.IsEnabled = false;
            TextBoxSurname.Text = "";
        }


        private void RefreshCommandBinding_Executed(object sender, RoutedEventArgs e)
        {
            RewriteEmploee();
            DataGridManufact.IsReadOnly = false;
            isDirty = true;
            BorderFind.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
