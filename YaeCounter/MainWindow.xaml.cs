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
using YaeCounter.ViewModels;
using System;
using System.IO;

namespace YaeCounter;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindowVM? vm;
    public string filepath;
    public MainWindow()
    {
        try 
        {
            InitializeComponent();

            // View と ViewModel を連携
            vm = new MainWindowVM();
            this.DataContext = vm;

            // セーブデータ呼び出し
            filepath = Directory.GetCurrentDirectory() + "\\savedata.csv";
            // セーブデータが無ければ処理終わり
            if (!File.Exists(filepath))
            {
                return;
            }
            // セーブデータがあれば読み込む
            using (StreamReader reader = new StreamReader(filepath))
            {
                List<string> savedatas = new List<string>();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');
                    savedatas.AddRange(values);
                }
                int total;
                int.TryParse(savedatas[3], out total);
                vm.total = total;
                int previousDeath;
                int.TryParse(savedatas[2], out previousDeath);
                vm.current = previousDeath;
                vm.copytext = $"【やえデスカウンター】本日:{vm.current}回 通算:{vm.total}回";
            }
        }
        catch
        {
            MessageBox.Show("エラーが発生しました。強制終了します。",
                "error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            Application.Current.Shutdown();
        }
    }

    private void countup_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            vm.current++;
            vm.total++;
            vm.copytext = $"【やえデスカウンター】本日:{vm.current}回 通算:{vm.total}回";
            Clipboard.SetText(vm.copytext);
        }
        catch
        {
            MessageBox.Show("エラーが発生しました。強制終了します。",
                "error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            Application.Current.Shutdown();
        }
    }

    private void countdown_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            vm.current--;
            vm.total--;
            vm.copytext = $"【やえデスカウンター】本日:{vm.current}回 通算:{vm.total}回";
            Clipboard.SetText(vm.copytext);
        } 
        catch 
        {
            MessageBox.Show("エラーが発生しました。強制終了します。",
                "error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            Application.Current.Shutdown();
        }
    }

    private void save_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            using StreamWriter writer = new StreamWriter(filepath);
            writer.WriteLine("前回死亡回数,通算死亡回数");
            writer.WriteLine($"{vm.current},{vm.total}");
            MessageBox.Show("セーブしました",
                "info",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        } 
        catch 
        {
            MessageBox.Show("エラーが発生しました。強制終了します。",
                "error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            Application.Current.Shutdown();
        }

    }

    private void reset_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            vm.current = 0;
        }
        catch
        {
            MessageBox.Show("エラーが発生しました。強制終了します。",
                "error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            Application.Current.Shutdown();
        }
    }
}