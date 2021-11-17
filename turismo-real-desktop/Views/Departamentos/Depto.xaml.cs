using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using turismo_real_business.DTOs;
using turismo_real_controller.Controllers.Departamento;
using turismo_real_controller.Controllers.Util;
using turismo_real_desktop.UIElements;
using turismo_real_desktop.Views.Administrador.Departamentos;

namespace turismo_real_desktop.Views.Departamentos
{

    public partial class Depto : MetroWindow
    {
        private UtilController utilController;
        private DepartamentoController deptoController;
        private DepartamentoDTO selectedDepto;
        private const string nuevaInstalacionPlace = "Nueva Instalación";
        private Deptos deptosWindow;
        protected int deptoId;

        public Depto()
        {
            InitializeComponent();
        }

        public Depto(Deptos deptosWindow, int deptoId)
        {
            InitializeComponent();
            this.deptoId = deptoId;
            this.deptosWindow = deptosWindow;
            string titulo = $"DEPARTAMENTO {deptoId}";
            tituloDepto.Content = titulo;
            tituloDeptoEditar.Content = titulo;

            selectedDepto = new DepartamentoController().ObtenerDepartamento(deptoId);
            SetDataForm(selectedDepto);
            mensajeUsuario.Text = string.Empty;
        }

        public string GetSource(FrameworkElement src) => src.Name;
        private void OnHoverGuardar(object sender, MouseEventArgs e) => ChangeHoverColor(btnGuardar, guardarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnLeaveGuardar(object sender, MouseEventArgs e) => ChangeLeaveColor(btnGuardar, guardarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnHoverCancelar(object sender, MouseEventArgs e) => ChangeHoverColor(btnCancelar, cancelarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnLeaveCancelar(object sender, MouseEventArgs e) => ChangeLeaveColor(btnCancelar, cancelarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnHoverEditar(object sender, MouseEventArgs e) => ChangeHoverColor(btnEditar, editarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnLeaveEditar(object sender, MouseEventArgs e) => ChangeLeaveColor(btnEditar, editarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnHoverVolver(object sender, MouseEventArgs e) => ChangeHoverColor(btnVolver, volverText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnLeaveVolver(object sender, MouseEventArgs e) => ChangeLeaveColor(btnVolver, volverText, GetSource(e.OriginalSource as FrameworkElement));

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

        public void SetDataForm(DepartamentoDTO depto)
        {
            SetDeptoDataLabels(depto);
            SetDeptoDataInputs(depto);
            SetComboTiposDepto();
            SetComboDormitorios();
            SetComboEstado(depto);
            SetComboBanios();
            SetComboRegiones();
            comboRegion.SelectedItem = depto.direccion.region;
            RegionChanged();
            comboComuna.SelectedItem = depto.direccion.comuna;
            SetInstalaciones();

            gridEditar.Visibility = Visibility.Hidden;
            gridVer.Visibility = Visibility.Visible;
        }

        public void SetComboEstado(DepartamentoDTO depto)
        {
            utilController = new UtilController();
            List<string> estados = utilController.ObtenerEstadosDepto();
            comboEstado.ItemsSource = estados;
            comboEstado.SelectedItem = selectedDepto.estado;
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
            estadoText.Content = depto.estado;
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

        private void EditarDepto(object sender, RoutedEventArgs e)
        {
            gridVer.Visibility = Visibility.Hidden;
            gridEditar.Visibility = Visibility.Visible;
            this.Title = "Turismo Real - Editar Departamento";

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
            this.Title = "Turismo Real - Departamento";
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

        public DepartamentoDTO ConvertFormToDepto()
        {
            DepartamentoDTO depto = new DepartamentoDTO();
            depto.rol = txtRol.Text;
            depto.dormitorios = Convert.ToInt32(comboDormitorios.SelectedItem.ToString());
            depto.banios = Convert.ToInt32(comboBanios.SelectedItem.ToString());
            depto.estado = comboEstado.SelectedItem.ToString();
            depto.descripcion = txtDescripcion.Text;
            depto.superficie = Convert.ToInt32(txtSuperficie.Text);
            depto.valorDiario = Convert.ToDouble(txtValorDiario.Text);
            depto.tipo = comboTipo.SelectedItem.ToString();

            DireccionDTO direccion = new DireccionDTO();
            direccion.region = comboRegion.SelectedItem.ToString();
            direccion.comuna = comboComuna.SelectedItem.ToString();
            direccion.calle = txtCalle.Text;
            direccion.numero = txtNumero.Text;
            direccion.depto = txtNroDepto.Text;
            depto.direccion = direccion;
            depto.instalaciones = GetStringListFromListBox(InstalacionesAgregadas);
            return depto;
        }

        private void GuardarCambios(object sender, RoutedEventArgs e)
        {
            deptoController = new DepartamentoController();
            DepartamentoDTO depto = ConvertFormToDepto();
            depto.id_departamento = deptoId;
            DepartamentoDTO updatedDepto = deptoController.ActualizarDepto(depto);

            string title;
            string message;
            if (updatedDepto != null)
            {
                SetDataForm(updatedDepto);
                deptosWindow.FillDataGridDeptos();
                title = "Departamento Actualizado";
                message = "El departamento ha sido actualizado correctamente.";
                MessageBox.Show(message, title,MessageBoxButton.OK, MessageBoxImage.Information);
                this.Title = "Turismo Real - Departamento";
                return;
            }
            title = "Error al actualizar";
            message = "Ha ocurrido un error al intentar actualizar departamento.";
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        // CHANGE COLORS METHODS
        public void ChangeHoverColor(Tile tile, TextBlock text, string source)
        {
            // btnCancelar - btnGuardar -btnVolver - btnEditar
            if (source.Equals("btnGuardar") || source.Equals("btnEditar"))
            {
                tile.Background = UIColors.NormalGreen;
                text.Foreground = UIColors.White;
            }
            else if (source.Equals("btnCancelar") || source.Equals("btnVolver"))
            {
                tile.Background = UIColors.Red;
                text.Foreground = UIColors.White;
            }
        }

        public void ChangeLeaveColor(Tile tile, TextBlock text, string source)
        {
            tile.Background = UIColors.White;
            if (source.Equals("btnGuardar") || source.Equals("btnEditar"))
                text.Foreground = UIColors.NormalGreen;
            else if (source.Equals("btnCancelar") || source.Equals("btnVolver"))
                text.Foreground = UIColors.Red;
        }
    }
}
