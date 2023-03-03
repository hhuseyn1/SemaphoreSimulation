using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Source;
public partial class MainWindow : Window, INotifyPropertyChanged
{
    private int threadCount = 0;
    public int _numValue = 3;

    public event PropertyChangedEventHandler? PropertyChanged;
    public ObservableCollection<Thread> Created { get; set; }
    public ObservableCollection<Thread> Waiting { get; set; }
    public ObservableCollection<Thread> Working { get; set; }

    private Semaphore semaphore;

    public int NumValue
    {
        get { return _numValue; }
        set
        {
            _numValue = value;
            PropertyChanged?.Invoke(
               this, new PropertyChangedEventArgs(nameof(NumValue)));
        }

    }

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
        semaphore = new(NumValue, 3, "Semaphore");

        Created = new();
        Waiting = new();
        Working = new();

        for (int i = 0; i < 3; i++)
        {
            var t = new Thread(ThreadWork);
            t.Name = "Thread "+ threadCount++;
            Working.Add(t);
        }
    }
    private void cmdDown_Click(object sender, RoutedEventArgs e)
    {
        if (NumValue > 0)
            NumValue--;
        else
            MessageBox.Show("Semaphore places must greater than 0", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
    }

    private void cmdUp_Click(object sender, RoutedEventArgs e)
    {
        if (NumValue < 7)
            NumValue++;
        else
            MessageBox.Show("Semaphore places must less than 7", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
    }

    private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (SCountTxtbox.Text == null)
        {
            return;
        }

        if (!int.TryParse(SCountTxtbox.Text, out _numValue))
            SCountTxtbox.Text = _numValue.ToString();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var thread = new Thread(ThreadWork);
        thread.Name = $"Thread {threadCount++}";
        Created.Add(thread);
    }

    private void ThreadWork(object? semaphore)
    {
        if (semaphore is Semaphore sem)
        {
            if (sem.WaitOne())
            {
                try
                {
                    var t = Thread.CurrentThread;
                    Dispatcher.Invoke(() => t.Name=$"Thread {threadCount++}");
                    Dispatcher.Invoke(() => Waiting.Remove(t));
                    Dispatcher.Invoke(() => Working.Add(t));
                    Thread.Sleep(1000);
                    Dispatcher.Invoke(() => Working.Remove(t));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sem.Release();
                }

            }
        }
    }

    private void CreatedLBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (CreatedLBox.SelectedItem is Thread t)
        {
            Created.Remove(t);

            Waiting.Add(t);
            t.Start(semaphore);
        }
    }

    private void WorkingLBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (CreatedLBox.SelectedItem is Thread t)
        {
            Working.Remove(t);

            t.Start(semaphore);
        }
    }
}
