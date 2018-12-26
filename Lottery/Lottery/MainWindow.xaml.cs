using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;

namespace Lottery
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isEntrieView = false; //全屏状态
        private List<PersonInfo> PersonList = new List<PersonInfo>();
        DispatcherTimer timer = new DispatcherTimer();
        Random ran = new Random();
        private PersonInfo Current;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(10);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var ranValue = ran.Next(0, PersonList.Count - 1);
            Current = PersonList[ranValue];
            txt.Text = Current.Name + " " + Current.Id;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            EntireView();

            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "peoplelist.txt");
            if (!System.IO.File.Exists(path))
            {
                MessageBox.Show("文件不存在！");
                return;
            }
            var info = File.ReadAllText(path);
            var personList = info.Split('\n').ToList();
            foreach (var person in personList)
            {
                var infos = person.Split(' ').ToList();
                if (infos.Count < 2) continue;
                PersonList.Add(new PersonInfo { Id = infos[0].Trim(), Name = infos[1].Trim() });
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Escape)
                {
                    EntireView();
                }
            }
            catch (Exception ex) { }
        }

        private void EntireView()
        {
            if (!_isEntrieView)
            {
                //设置全屏
                this.WindowState = WindowState.Normal;
                this.WindowStyle = WindowStyle.None;
                this.ResizeMode = ResizeMode.NoResize;
                //this.Topmost = true;
                //this.IsTopmost = true;
                //this.CanClose = false;
                //this.CanMove = false;

                this.Left = 0;
                this.Top = 0;
                this.Width = SystemParameters.PrimaryScreenWidth;
                this.Height = SystemParameters.PrimaryScreenHeight;

                _isEntrieView = true;
            }
            else
            {
                this.WindowState = WindowState.Normal;
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.ResizeMode = ResizeMode.CanResize;
                //this.CanClose = true;
                //this.CanMove = true;

                this.Left = 100;
                this.Top = 100;
                this.Width = 800;
                this.Height = 600;

                _isEntrieView = false;
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            List.Children.Add()
        }
    }
}
