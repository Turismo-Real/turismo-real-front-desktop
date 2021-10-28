using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using turismo_real_business.DTOs;
using turismo_real_controller.Controllers.Util;
using turismo_real_desktop.UIElements;

namespace turismo_real_desktop.Views.Departamentos
{

    public partial class Nuevodepto : MetroWindow
    {
        private UtilController utilController;
        private const string nuevaInstalacionPlace = "Nueva Instalación";

        public Nuevodepto()
        {
            InitializeComponent();
            // Set the DataContext for your View
            this.DataContext = new VModal(DialogCoordinator.Instance);
            SetComboRegiones();
            SetComboDormitorios();
            SetComboBanios();
            SetInstalaciones();
        }

        private void OnHoverCancelar(object sender, MouseEventArgs e)
        {
            btnCancelar.Background = UIColors.Red;
            cancelarText.Foreground = UIColors.White;
        }

        private void OnLeaveCancelar(object sender, MouseEventArgs e)
        {
            btnCancelar.Background = UIColors.White;
            cancelarText.Foreground = UIColors.Red;
        }

        private void CancelarInsercion(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnHoverGuardar(object sender, MouseEventArgs e)
        {
            btnGurardar.Background = UIColors.NormalGreen;
            guardarText.Foreground = UIColors.White;
        }

        private void OnLeaveGuardar(object sender, MouseEventArgs e)
        {
            btnGurardar.Background = UIColors.White;
            guardarText.Foreground = UIColors.NormalGreen;
        }

        private void InsertarNuevoDepto(object sender, RoutedEventArgs e)
        {
            DepartamentoDTO nuevoDepto = ConvertFormToDepto();
        }

        public DepartamentoDTO ConvertFormToDepto()
        {



            return new DepartamentoDTO();
        }

        public void SetComboRegiones()
        {
            utilController = new UtilController();
            List<string> regiones = utilController.ObtenerRegiones();
            comboRegion.ItemsSource = regiones;
            comboRegion.SelectedIndex = 0;
        }

        private void RegionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboRegion.SelectedIndex != 0)
            {
                utilController = new UtilController();
                string region = comboRegion.SelectedItem.ToString();
                List<string> comunas = utilController.ObtenerComunasPorRegion(region);
                comboComuna.ItemsSource = comunas;
                comboComuna.SelectedIndex = 0;
                return;
            }
            comboComuna.ItemsSource = new List<string>();
        }

        public void SetComboDormitorios()
        {
            comboDormitorios.ItemsSource = GenerateIntRange(0, 11);
            comboDormitorios.SelectedIndex = 0;
        }

        public void SetComboBanios()
        {
            comboBanios.ItemsSource = GenerateIntRange(0, 11);
            comboBanios.SelectedIndex = 0;
        }

        public List<int> GenerateIntRange(int from, int to)
        {
            List<int> numberList = new List<int>();
            foreach (int value in Enumerable.Range(from, to))
            {
                numberList.Add(value);
            }
            return numberList;
        }

        public void SetInstalaciones()
        {
            // obtener instalaciones y cargarlas en listbox
            utilController = new UtilController();
            List<string> instalaciones= utilController.ObtenerInstalaciones();
            
            foreach(string instalacion in instalaciones)
            {
                instalacionesDisponibles.Items.Add(instalacion);
            }
        }

        private void AgregarInstalacion(object sender, RoutedEventArgs e)
        {
            if (instalacionesDisponibles.Items.Count > 0 && instalacionesDisponibles.SelectedItem != null)
            {
                string selectedItem = instalacionesDisponibles.SelectedItem.ToString();
                InstalacionesAgregadas.Items.Add(selectedItem);
                instalacionesDisponibles.Items.Remove(selectedItem);
            }
        }

        private void QuitarInstalacion(object sender, RoutedEventArgs e)
        {
            if (InstalacionesAgregadas.Items.Count > 0 && InstalacionesAgregadas.SelectedItem != null)
            {
                string selectedItem = InstalacionesAgregadas.SelectedItem.ToString();
                instalacionesDisponibles.Items.Add(selectedItem);
                InstalacionesAgregadas.Items.Remove(selectedItem);
            }
        }

        private void InvertirInstalaciones(object sender, RoutedEventArgs e)
        {
            List<string> disponibles = new List<string>();
            foreach(var disponible in instalacionesDisponibles.Items)
            {
                disponibles.Add(disponible.ToString());
            }

            List<string> agregadas = new List<string>();
            foreach(var agregada in InstalacionesAgregadas.Items)
            {
                agregadas.Add(agregada.ToString());
            }

            // llenar disponibles
            instalacionesDisponibles.Items.Clear();
            foreach(string disponible in agregadas)
            {
                instalacionesDisponibles.Items.Add(disponible);
            }

            // llenar agregadas
            InstalacionesAgregadas.Items.Clear();
            foreach (string agregada in disponibles)
            {
                InstalacionesAgregadas.Items.Add(agregada);
            }
        }

        private void NuevaInstalacion(object sender, RoutedEventArgs e)
        {
            if (txtInstalacion.Text.Equals(nuevaInstalacionPlace) && txtInstalacion.Foreground.Equals(Brushes.Gray)) return;

            string instalacion = txtInstalacion.Text.Trim();
            if (!instalacion.Equals(string.Empty))
            {
                InstalacionesAgregadas.Items.Add(instalacion);
                txtInstalacion.Text = string.Empty;
                NuevaInstalacionLostFocus(sender, e);
            }
        }

        private void NuevaInstalacionFocus(object sender, RoutedEventArgs e)
        {
            if (txtInstalacion.Text.Equals(nuevaInstalacionPlace) && txtInstalacion.Foreground.Equals(Brushes.Gray))
            {
                txtInstalacion.Text = string.Empty;
                txtInstalacion.Foreground = Brushes.Black;
            }
        }

        private void NuevaInstalacionLostFocus(object sender, RoutedEventArgs e)
        {
            if(txtInstalacion.Text.Equals(string.Empty) && txtInstalacion.Foreground.Equals(Brushes.Black))
            {
                txtInstalacion.Text = nuevaInstalacionPlace;
                txtInstalacion.Foreground = Brushes.Gray;
            }
        }
    }
}
