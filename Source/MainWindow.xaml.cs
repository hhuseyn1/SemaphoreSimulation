using System.Windows;
using System.Windows.Controls;

namespace Source;
public partial class MainWindow : Window
{
    private int _numValue = 0;

    public int NumValue
    {
        get { return _numValue; }
        set
        {
            _numValue = value;
            txtNum.Text = value.ToString();
        }
    }
    public MainWindow()
    {
        InitializeComponent();
        txtNum.Text = _numValue.ToString();

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
        if (NumValue < 15)
            NumValue++;
        else
            MessageBox.Show("Semaphore places must less than 15","Warning",MessageBoxButton.OK,MessageBoxImage.Warning);
    }

    private void txtNum_TextChanged(object sender,TextChangedEventArgs e)
    {
        if (txtNum == null)
        {
            return;
        }

        if (!int.TryParse(txtNum.Text, out _numValue))
            txtNum.Text = _numValue.ToString();
    }
}
