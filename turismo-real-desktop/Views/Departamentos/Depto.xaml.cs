using MahApps.Metro.Controls;
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using turismo_real_business.DTOs;
using turismo_real_controller.Controllers.Departamento;
using turismo_real_controller.Controllers.Util;
using System.Windows.Media;

namespace turismo_real_desktop.Views.Departamentos
{

    public partial class Depto : MetroWindow
    {
        private UtilController utilController;
        private DepartamentoDTO selectedDepto;
        private const string nuevaInstalacionPlace = "Nueva Instalación";

        public Depto()
        {
            InitializeComponent();
        }

        public Depto(int deptoId)
        {
            InitializeComponent();
            string titulo = $"DEPARTAMENTO {deptoId}";
            tituloDepto.Content = titulo;
            tituloDeptoEditar.Content = titulo;
            selectedDepto = new DepartamentoController().ObtenerDepartamento(deptoId);
            SetDeptoDataLabels(selectedDepto);
            SetDeptoDataInputs(selectedDepto);
            SetComboTiposDepto();
            SetComboDormitorios();
            SetComboBanios();
            SetComboRegiones();
            comboRegion.SelectedItem = selectedDepto.direccion.region;
            RegionChanged();
            comboComuna.SelectedItem = selectedDepto.direccion.comuna;
            SetInstalaciones();

            gridEditar.Visibility = Visibility.Hidden;
            gridVer.Visibility = Visibility.Visible;
        }

        private void SetComboTiposDepto()
        {
            utilController = new UtilController();
            List<string> tiposDepto = utilController.ObtenerTiposDepto();
            foreach (string tipo in tiposDepto)
            {
                comboTipo.Items.Add(tipo);
            }
            comboTipo.SelectedItem = selectedDepto.tipo;
        }

        public void SetComboDormitorios()
        {
            comboDormitorios.ItemsSource = GenerateIntRange(0, 11);
            comboDormitorios.SelectedItem = selectedDepto.dormitorios;
        }

        public void SetComboBanios()
        {
            comboBanios.ItemsSource = GenerateIntRange(0, 11);
            comboBanios.SelectedItem = selectedDepto.banios;
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

        public void SetComboRegiones()
        {
            utilController = new UtilController();
            List<string> regiones = utilController.ObtenerRegiones();
            comboRegion.ItemsSource = regiones;
            comboRegion.SelectedItem = selectedDepto.direccion.region;
        }

        public void SetInstalaciones()
        {
            foreach (string instalacion in selectedDepto.instalaciones)
            {
                InstalacionesAgregadas.Items.Add(instalacion);
                instalacionesList.Items.Add(instalacion);
            }
        }

        public void RegionChanged()
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

        public void SetDeptoDataLabels(DepartamentoDTO depto)
        {
            rolText.Content = depto.rol;
            tipoText.Content = depto.tipo;
            dormitoriosText.Content = depto.dormitorios;
            baniosText.Content = depto.banios;
            superficieText.Content = depto.superficie;
            valorDiarioText.Content = depto.valorDiario;
            nroDeptoText.Content = depto.direccion.depto;
            regionText.Content = depto.direccion.region;
            comunaText.Content = depto.direccion.comuna;
            calleText.Content = depto.direccion.calle;
            numeroText.Content = depto.direccion.numero;
            descripcionText.Content = depto.descripcion;
        }

        public void SetDeptoDataInputs(DepartamentoDTO depto)
        {
            txtRol.Text = depto.rol;
            txtSuperficie.Text = depto.superficie.ToString();
            txtValorDiario.Text = depto.valorDiario.ToString();
            txtNroDepto.Text = depto.direccion.depto;
            txtCalle.Text = depto.direccion.calle;
            txtNumero.Text = depto.direccion.numero;
            txtDescripcion.Text = depto.descripcion;
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

        private void RegionChanged(object sender, SelectionChangedEventArgs e)
        {

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
            List<string> disponibles = GetStringListFromListBox(instalacionesDisponibles);
            List<string> agregadas = GetStringListFromListBox(InstalacionesAgregadas);

            // llenar disponibles
            instalacionesDisponibles.Items.Clear();
            foreach (string disponible in agregadas)
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
            if (txtInstalacion.Text.Equals(string.Empty) && txtInstalacion.Foreground.Equals(Brushes.Black))
            {
                txtInstalacion.Text = nuevaInstalacionPlace;
                txtInstalacion.Foreground = Brushes.Gray;
            }
        }

        private void OnHoverGuardar(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveGuardar(object sender, MouseEventArgs e)
        {

        }

        private void OnHoverCancelar(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveCancelar(object sender, MouseEventArgs e)
        {

        }

        private void EditarDepto(object sender, RoutedEventArgs e)
        {
            gridVer.Visibility = Visibility.Hidden;
            gridEditar.Visibility = Visibility.Visible;

            SetInstalacionesRestantes();
        }

        public void SetInstalacionesRestantes()
        {
            utilController = new UtilController();
            List<string> instalaciones = utilController.ObtenerInstalaciones();
            List<string> agregadas = GetStringListFromListBox(InstalacionesAgregadas);
            List<string> restantes = instalaciones.Except(agregadas).ToList();
            SetListBoxFromStrList(instalacionesDisponibles, restantes);
        }

        private void CancelarEdicion(object sender, RoutedEventArgs e)
        {
            gridEditar.Visibility = Visibility.Hidden;
            gridVer.Visibility = Visibility.Visible;
        }

        private void ClickVolver(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public List<string> GetStringListFromListBox(ListBox listbox)
        {
            List<string> strCollection = new List<string>();
            foreach (var strItem in listbox.Items)
            {
                strCollection.Add(strItem.ToString());
            }
            return strCollection;
        }

        private void SetListBoxFromStrList(ListBox listbox, List<string> strList)
        {
            listbox.Items.Clear();
            foreach (string str in strList)
            {
                listbox.Items.Add(str);
            }
        }

    }
}
