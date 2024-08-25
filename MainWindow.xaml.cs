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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WPFProg6221ICE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EventManager eventManager;

        public MainWindow()
        {
            InitializeComponent();
            eventManager = new EventManager();

            // Sample data
            eventManager.Events.Add(new Event
            {
                EventID = 1,
                Title = "Workshop on AI",
                Date = DateTime.Today,
                Type = "Workshop",
                Department = "Computer Science",
                Status = "Confirmed",
                Description = "A workshop on AI and Machine Learning",
                MaxAttendees = 50,
                RegisteredAttendees = 10,
            });

            eventManager.Events.Add(new Event
            {
                EventID = 2,
                Title = "Cultural Fest",
                Date = DateTime.Today.AddDays(2),
                Type = "Festival",
                Department = "Arts",
                Status = "Canceled",
                Description = "An event to celebrate cultural diversity"
            });

            eventManager.Events.Add(new Event
            {
                EventID = 3,
                Title = "Jiafei Inauguration",
                Date = DateTime.Today.AddDays(5),
                Type = "Floptropican Event",
                Department = "The Commonwealth of Floptropica",
                Status = "Confirmed",
                Description = "An event to celebrate the inauguration of Queen Xijemomoussy",
                MaxAttendees = 200,
                RegisteredAttendees = 150
            });

            // Bind data to ListView
            eventListView.ItemsSource = eventManager.Events;

        }

        private void OnFilterEvents(object sender, RoutedEventArgs e)
        {
            var filteredEvents = eventManager.FilterEvents(null, searchBox.Text, null);
            eventListView.ItemsSource = filteredEvents.ToList();
        }

        private async void OnUpdateStatus(object sender, RoutedEventArgs e)
        {
            var selectedEvent = (Event)eventListView.SelectedItem;
            if (selectedEvent != null)
            {
                string selectedStatus = ((ComboBoxItem)statusComboBox.SelectedItem).Content.ToString();

                // Update status asynchronously using Task.Run
                await Task.Run(() => UpdateEventStatus(selectedEvent, selectedStatus));

                // Update UI on the main thread after status is updated
                Dispatcher.Invoke(() =>
                {
                    selectedEvent.Status = selectedStatus;
                    eventListView.Items.Refresh();  // Refresh ListView to reflect updated status
                });

                MessageBox.Show($"Status updated to {selectedStatus}.");
            }
            else
            {
                MessageBox.Show("Please select an event to update the status.");
            }
        }


        // Method to update event status (simulated workload)
        private void UpdateEventStatus(Event selectedEvent, string newStatus)
        {
            // Simulate some work (e.g., updating a database, sending notifications)
            Thread.Sleep(2000); // Simulate a delay for the operation
            selectedEvent.Status = newStatus; // Update the status (this happens on a separate thread)
        }


        private async void OnRegister(object sender, RoutedEventArgs e)
        {
            var selectedEvent = (Event)eventListView.SelectedItem;
            if (selectedEvent != null)
            {
                // Find the event in the eventManager based on title (or another unique identifier)
                var evt = eventManager.Events.FirstOrDefault(ev => ev.EventID == selectedEvent.EventID);

                if (evt != null && evt.Status == "Confirmed")
                {
                    if (evt.RegisteredAttendees < evt.MaxAttendees)
                    {
                        // Display progress bar and status text
                        registrationProgressBar.Visibility = Visibility.Visible;
                        statusTextBlock.Visibility = Visibility.Visible;
                        statusTextBlock.Text = "Registering...";

                        // Simulate registration process using a Task
                        await Task.Run(() =>
                        {
                            // Simulate a delay for registration process
                            for (int i = 0; i <= 100; i += 20)
                            {
                                Dispatcher.Invoke(() => registrationProgressBar.Value = i);
                                System.Threading.Thread.Sleep(500); // Simulate work being done
                            }
                        });

                        evt.RegisteredAttendees++;
                        MessageBox.Show($"Successfully registered for {evt.Title}.", "Registration Complete", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Refresh ListView to show updated registration count
                        eventListView.Items.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Registration full. No slots available.", "Registration Full", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Selected event is not confirmed or cannot be registered for.", "Invalid Event", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                // Hide progress bar and reset status text
                registrationProgressBar.Visibility = Visibility.Hidden;
                statusTextBlock.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Please select an event to register.", "No Event Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}