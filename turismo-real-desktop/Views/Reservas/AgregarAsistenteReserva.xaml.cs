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
using turismo_real_business.DTOs;

namespace turismo_real_desktop.Views.Reservas
{
    public partial class AgregarAsistenteReserva : MetroWindow
    {
        private readonly NuevaReservaAsistente _sourceWindow;

        public AgregarAsistenteReserva(NuevaReservaAsistente sourceWindow)
        {
            _sourceWindow = sourceWindow;
            InitializeComponent();
        }

        private void Cancelar(object sender, RoutedEventArgs e) => Close();

        private void AgregarAsistente(object sender, RoutedEventArgs e)
        {
            AsistenteDTO asistente = ConvertFormToAsistente();
            _sourceWindow.AgregarAsistente(asistente);
            Close();
        }

        private AsistenteDTO ConvertFormToAsistente()
        {
            AsistenteDTO asistente = new AsistenteDTO();
            asistente.pasaporte = txtPasaporte.Text;
            asistente.numRut = txtRut.Text;
            asistente.dvRut = txtDvRut.Text;
            asistente.primerNombre = txtPrimerNombre.Text;
            asistente.segundoNombre = txtSegundoNombre.Text;
            asistente.primerApellido = txtPrimerApellido.Text;
            asistente.segundoApellido = txtSegundoApellido.Text;
            asistente.correo = txtCorreo.Text;
            return asistente;
        }
    }
}
