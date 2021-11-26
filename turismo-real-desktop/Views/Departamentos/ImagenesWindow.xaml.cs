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
using turismo_real_desktop.UIElements;
using turismo_real_desktop.Views.Administrador.Departamentos;

namespace turismo_real_desktop.Views.Departamentos
{
    public partial class ImagenesWindow : MetroWindow
    {
        private readonly Deptos activeWindow;
        private readonly int idDepartamento;

        public ImagenesWindow()
        {
            InitializeComponent();
        }

        public ImagenesWindow(Deptos activeWindow, int idDepartamento)
        {
            InitializeComponent();
            this.activeWindow = activeWindow;
            this.idDepartamento = idDepartamento;
        }

        private void OnHoverAgregar(object sender, MouseEventArgs e) => ChangeHoverColor(btnAgregar, agregarText, "BLUE");
        private void OnLeaveAgregar(object sender, MouseEventArgs e) => ChangeLeaveColor(btnAgregar, agregarText, "BLUE");
        private void OnHoverEliminar(object sender, MouseEventArgs e) => ChangeHoverColor(btnEliminar, eliminarText, "RED");
        private void OnLeaveEliminar(object sender, MouseEventArgs e) => ChangeLeaveColor(btnEliminar, eliminarText, "RED");
        private void OnHoverVolver(object sender, MouseEventArgs e) => ChangeHoverColor(btnVolver, volverText, "GREEN");
        private void OnLeaveVolver(object sender, MouseEventArgs e) => ChangeLeaveColor(btnVolver, volverText, "GREEN");
        private void Volver(object sender, RoutedEventArgs e) => Close();

        private void AgregarImagen(object sender, TouchEventArgs e)
        {

        }

        private void EliminarImagen(object sender, RoutedEventArgs e)
        {

        }

        // CHANGE COLORS METHODS
        public void ChangeHoverColor(Tile tile, TextBlock text, string color)
        {
            if (color.Equals("GREEN")) tile.Background = UIColors.NormalGreen;
            if (color.Equals("BLUE")) tile.Background = UIColors.Blue;
            if (color.Equals("RED")) tile.Background = UIColors.Red;
        }

        public void ChangeLeaveColor(Tile tile, TextBlock text, string color)
        {
            if (color.Equals("BLUE")) text.Foreground = UIColors.Blue;
            if (color.Equals("RED")) text.Foreground = UIColors.Red;
            if (color.Equals("GREEN")) text.Foreground = UIColors.NormalGreen;
            tile.Background = UIColors.White;
        }

    }
}
