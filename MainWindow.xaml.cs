using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;

namespace Keep_Me_Alive
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public System.Drawing.Point pos = new System.Drawing.Point(0, 0);
        public System.Drawing.Point pos2 = new System.Drawing.Point(0, 0);
        public bool Stopflag;

        public MainWindow()
        {
            InitializeComponent();
        }

        //imported
        [FlagsAttribute]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;

        //My code

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
            Status.Fill = System.Windows.Media.Brushes.Green;

            pos = System.Windows.Forms.Control.MousePosition;
            pos2.X = pos.X - 150;
            pos2.Y = pos.Y;

            Stopflag = false;

            for (int i = 0; i >= 0; i++)
            {

                if (i % 2 == 1)
                {
                    System.Windows.Forms.Cursor.Position = pos;
                }
                else
                {
                    System.Windows.Forms.Cursor.Position = pos2;
                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)pos2.X, (uint)pos2.Y, 0, 0);
                }

                await Task.Delay(4000);

                if (Stopflag)
                    break;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
            Status.Fill = System.Windows.Media.Brushes.Red;

            Stopflag = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Program simulates mouse movement and clicks to appear online.", "Appear online",MessageBoxButton.OK,MessageBoxImage.Asterisk);
        }
    }
}
