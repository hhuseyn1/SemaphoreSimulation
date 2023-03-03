using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Source;
public partial class MainWindow : Window,INotifyPropertyChanged
{
    public int _numValue=3;

    public event PropertyChangedEventHandler? PropertyChanged;

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
    }
    private void cmdDown_Click(object sender, RoutedEventArgs e)
    {
        if(NumValue>0)
            NumValue--;
        else
            MessageBox.Show("Semaphore places must greater than 0","Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
    }

    private void cmdUp_Click(object sender, RoutedEventArgs e)
    {
        if (NumValue < 7)
            NumValue++;
        else
            MessageBox.Show("Semaphore places must less than 7","Warning",MessageBoxButton.OK,MessageBoxImage.Warning);
    }

    private void txtNum_TextChanged(object sender,TextChangedEventArgs e)
    {
        if (SCountTxtbox.Text == null)
        {
            return;
        }

        if (!int.TryParse(SCountTxtbox.Text, out _numValue))
            SCountTxtbox.Text = _numValue.ToString();
    }
}
