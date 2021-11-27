using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace turismo_real_desktop.Views.Reservas
{
    public partial class NuevaReservaCliente : MetroWindow
    {
        private NuevaReservaDepto nextWindow;

        public NuevaReservaCliente()
        {
            InitializeComponent();
        }

        private void Salir(object sender, RoutedEventArgs e) => Close();

        private void StepDepto(object sender, RoutedEventArgs e)
        {
            if (nextWindow != null) nextWindow.Show();

            if (nextWindow == null)
            {
                NuevaReservaDepto reservaDeptoWin = new NuevaReservaDepto(this);
                reservaDeptoWin.Show();
            }
            Hide();
        }

        public void SetNextWindow(NuevaReservaDepto nextWindow)
        {
            this.nextWindow = nextWindow;
        }
    }
}
