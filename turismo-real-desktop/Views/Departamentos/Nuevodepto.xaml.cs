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

    public partial class Nuevodepto : MetroWindow
    {
        private UtilController utilController;
        private DepartamentoController deptoController;
        private const string nuevaInstalacionPlace = "Nueva Instalación";
        private readonly Deptos activeDeptosWindow;

        public Nuevodepto()
        {
            InitializeWindow();
        }

        public Nuevodepto(Deptos deptosWindow)
        {
            InitializeWindow();
            activeDeptosWindow = deptosWindow;
        }

        public void InitializeWindow()
        {
            InitializeComponent();
            SetComboTiposDepto();
            SetComboRegiones();
            SetComboDormitorios();
            SetComboBanios();
            SetInstalaciones();
            mensajeUsuario.Text = string.Empty;
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

        private void CancelarInsercion(object sender, RoutedEventArgs e) => Close();

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
            deptoController = new DepartamentoController();
            bool nuevoDeptoInsertado = deptoController.AgregarNuevoDepto(nuevoDepto);

            if (nuevoDeptoInsertado)
            {
                ClearForm();
                activeDeptosWindow.FillDataGridDeptos();
                ShowSuccessMessage();
                return;
            }
            mensajeUsuario.Text = "Error al crear departamento.";
            mensajeUsuario.Foreground = Brushes.Red;
        }

        public DepartamentoDTO ConvertFormToDepto()
        {
            DepartamentoDTO depto = new DepartamentoDTO();
            depto.rol = txtRol.Text;
            depto.dormitorios = Convert.ToInt32(comboDormitorios.SelectedItem.ToString());
            depto.banios = Convert.ToInt32(comboBanios.SelectedItem.ToString());
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

        public List<string> GetStringListFromListBox(ListBox listbox)
        {
            List<string> strCollection = new List<string>();
            foreach (var strItem in listbox.Items)
            {
                strCollection.Add(strItem.ToString());
            }
            return strCollection;
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
            List<string> disponibles = GetStringListFromListBox(instalacionesDisponibles);
            List<string> agregadas = GetStringListFromListBox(InstalacionesAgregadas);

            SetListBoxFromStrList(instalacionesDisponibles, agregadas);
            SetListBoxFromStrList(InstalacionesAgregadas, disponibles);
        }

        private void SetListBoxFromStrList(ListBox listbox, List<string> strList)
        {
            listbox.Items.Clear();
            foreach (string str in strList)
            {
                listbox.Items.Add(str);
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

        private void SetComboTiposDepto()
        {
            utilController = new UtilController();
            List<string> tiposDepto = utilController.ObtenerTiposDepto();
            foreach(string tipo in tiposDepto)
            {
                comboTipo.Items.Add(tipo);
            }
            comboTipo.SelectedIndex = 0;
        }

        public void ClearForm()
        {
            txtRol.Text = string.Empty;
            comboTipo.SelectedIndex = 0;
            comboDormitorios.SelectedIndex = 0;
            comboBanios.SelectedIndex = 0;
            txtSuperficie.Text = string.Empty;
            txtValorDiario.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtNroDepto.Text = string.Empty;
            comboRegion.SelectedIndex = 0;
            RegionChanged(null, null);
            txtCalle.Text = string.Empty;
            txtNumero.Text = string.Empty;
            InstalacionesAgregadas.Items.Clear();
            SetInstalaciones();
        }

        public void ShowSuccessMessage()
        {
            string title = "Departamento Creado";
            string message = "El departamento se ha creado exitosamente.";
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //--//

        //Sentencia para que textbox lea solamente numeros 
        private void txtRol_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[0-9.-]"))
            {
                e.Handled = true;
            }
        }
        //Validador de texbox Vacio
        private void txtRol_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtRol.Text))
            {
                MessageBox.Show("El campo Rol no puede estar vacío", "¡Atención!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                return;
            }
        }

        //--//

        //Sentencia para que textbox lea solamente numeros 
        private void txtSuperficie_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[0-9]"))
            {
                e.Handled = true;
            }
        }
        //Validador de texbox Vacio
        private void txtSuperficie_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSuperficie.Text))
            {
                MessageBox.Show("El campo Superficie no puede estar vacío", "¡Atención!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                return;
            }
        }

        //--//

        //Sentencia para que textbox lea solamente numeros 
        private void txtValorDiario_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[0-9.]"))
            {
                e.Handled = true;
            }
        }
        //Validador de textbox Vacio
        private void txtValorDiario_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtValorDiario.Text))
            {
                MessageBox.Show("El campo Valor Diario no puede estar vacío", "¡Atención!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                return;
            }
        }

        //--//

        //Sentencia para que textbox lea solamente letras 
        private void txtCalle_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[a-zA-Z]"))
            {
                e.Handled = true;
            }
        }
        //Validador de textbox Vacio
        private void txtCalle_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCalle.Text))
            {
                MessageBox.Show("El campo Calle no puede estar vacío", "¡Atención!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                return;
            }
        }

        //--//

        //Sentencia para que textbox lea solamente numeros 
        private void txtNumero_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[0-9]"))
            {
                e.Handled = true;
            }

        }
        //Validador de textbox Vacio
        private void txtNumero_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNumero.Text))
            {
                MessageBox.Show("El campo Numero no puede estar vacío", "¡Atención!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                return;
            }
        }

        //--//

        //Sentencia para que textbox lea solamente numeros 
        private void txtNroDepto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[0-9]"))
            {
                e.Handled = true;
            }
        }
        //Validador de textbox Vacio
        private void txtNroDepto_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNroDepto.Text))
            {
                MessageBox.Show("El campo Nro Departamento no puede estar vacío", "¡Atención!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                return;
            }
        }

        //--//

        //Sentencia para que textbox lea solamente letras 
        private void txtDescripcion_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[a-zA-Z.,]"))
            {
                e.Handled = true;
            }
        }
        //Validador de texbox Vacio
        private void txtDescripcion_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                MessageBox.Show("El campo Descripción no puede estar vacío", "¡Atención!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                return;
            }
        }
    }
}
