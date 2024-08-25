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
using System.Windows.Shapes;

namespace WPFProg6221ICE
{
    public partial class NewEventWindow : Window
    {
        public Event NewEvent { get; private set; }

        public NewEventWindow()
        {
            InitializeComponent();
        }

        private async void OnCreateEventClick(object sender, RoutedEventArgs e)
        {
            // Collect user input
            string title = TitleTextBox.Text;
            DateTime date = DatePicker.SelectedDate ?? DateTime.Today;
            string type = TypeTextBox.Text;
            string department = DepartmentTextBox.Text;
            string status = ((ComboBoxItem)StatusComboBox.SelectedItem)?.Content.ToString() ?? "Pending";
            string description = DescriptionTextBox.Text;
            int maxAttendees = int.TryParse(MaxAttendeesTextBox.Text, out int max) ? max : 0;

            // Create a new event asynchronously
            var newEvent = new Event
            {
                Title = title,
                Date = date,
                Type = type,
                Department = department,
                Status = status,
                Description = description,
                MaxAttendees = maxAttendees,
                RegisteredAttendees = 0 // Default to 0
            };

            NewEvent = newEvent;

            // Close the dialog and return control to the main window
            DialogResult = true;
            Close();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == "Title")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Title";
                textBox.Foreground = Brushes.Gray;
            }
        }

    }

}
