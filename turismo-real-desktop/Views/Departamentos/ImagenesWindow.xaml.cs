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
using turismo_real_desktop.Views.Administrador.Departamentos;

namespace turismo_real_desktop.Views.Departamentos
{
    public partial class ImagenesWindow : Window
    {
        private readonly Deptos activeWindow;
        private readonly int idDepartamento;

        public ImagenesWindow()
        {
            InitializeComponent();
        }

        public ImagenesWindow(Deptos activeWindow, int idDepartamento)
        {
            this.activeWindow = activeWindow;
            this.idDepartamento = idDepartamento;
        }
    }
}
