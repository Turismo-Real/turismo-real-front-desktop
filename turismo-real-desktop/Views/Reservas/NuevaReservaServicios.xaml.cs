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
    public partial class NuevaReservaServicios : MetroWindow
    {
        private readonly NuevaReservaDepto previousWindow;
        private NuevaReservaAsistente nextWindow;

        public NuevaReservaServicios(NuevaReservaDepto previousWindow)
        {
            this.previousWindow = previousWindow;
            InitializeComponent();
        }

        private void BackToDepto(object sender, RoutedEventArgs e)
        {
            previousWindow.SetNextWindow(this);
            previousWindow.Show();
            Hide();
        }

        private void StepAsistentes(object sender, RoutedEventArgs e)
        {
            if (nextWindow != null) nextWindow.Show();

            if (nextWindow == null)
            {
                NuevaReservaAsistente reservaAsistentesWin = new NuevaReservaAsistente(this);
                reservaAsistentesWin.Show();
            }
            Hide();
        }

        public void SetNextWindow(NuevaReservaAsistente nextWindow) => this.nextWindow = nextWindow;
    }
}
