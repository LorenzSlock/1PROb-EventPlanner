using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace EventPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] _eventTypes = { "Festival", "Orkest", "Opera" };

        DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Ontimer_Tick;
            _timer.Start();
          
        }

        private void Ontimer_Tick(object? sender, EventArgs e)
        {
            timeLabel.Content = DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToLongDateString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string type in _eventTypes)
            {
                typeComboBox.Items.Add(type);
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (typeComboBox.SelectedIndex == 1)
            {
                return; //geen type geselecteerd
            }
            if (string.IsNullOrEmpty(nameTextBox.Text))
            {
                return; // geen naam ingegeven
            }
            if(!int.TryParse(visitorsTextBox.Text, out int visitors))
            {
                return;
            }

            Event eventObject = new Event();
            eventObject.EventType = typeComboBox.SelectedItem.ToString();
            eventObject.Name = nameTextBox.Text;
            eventObject.Visitors = int.Parse(visitorsTextBox.Text);

            eventsListBox.Items.Add(eventObject);

            quantityStatusBarItem.Content = $"Aantal events: {eventsListBox.Items.Count}";

            typeComboBox.SelectedIndex = -1;
            nameTextBox.Clear();
            visitorsTextBox.Clear();
            typeComboBox.Focus();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (eventsListBox.SelectedItem != null)
            {
                eventsListBox.Items.Remove(eventsListBox.SelectedItem);
            }
            //if (eventsListBox.SelectedIndex == -1)
            //{
            //    eventsListBox.Items.RemoveAt(eventsListBox.SelectedIndex);
            //}
            quantityStatusBarItem.Content = $"Aantal events: {eventsListBox.Items.Count}";
        }

        private void Onclose_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OndeleteAll_Clicked(object sender, RoutedEventArgs e)
        {
            eventsListBox.Items.Clear();
            quantityStatusBarItem.Content = $"Aantal events: {eventsListBox.Items.Count}";
        }

        private void OnCreateDefault_Clicked(object sender, RoutedEventArgs e)
        {
            Event eventObject = new Event();
            eventObject.EventType = "Orkest";
            eventObject.Name = "Jaarlijks optreden";
            eventObject.Visitors = 100;

            eventsListBox.Items.Add(eventObject);

            quantityStatusBarItem.Content = $"Aantal events: {eventsListBox.Items.Count}";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Bent u zeker?","Afsluiten", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(answer == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}