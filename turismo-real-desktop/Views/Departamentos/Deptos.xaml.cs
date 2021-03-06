using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using turismo_real_business.DTOs;
using turismo_real_controller.Controllers.Departamento;
using turismo_real_desktop.GridEntities;
using turismo_real_desktop.UIElements;
using turismo_real_desktop.Views.Departamentos;

namespace turismo_real_desktop.Views.Administrador.Departamentos
{
    public partial class Deptos : MetroWindow
    {
        private DepartamentoController deptoController;
        private List<DepartamentoDTO> deptos;

        public Deptos()
        {
            InitializeComponent();
            FillDataGridDeptos();
            dataGridDeptos.IsReadOnly = true;
        }

        private void OnHoverNuevoDepto(object sender, MouseEventArgs e) => ChangeHoverColor(btnNuevoDepto, nuevoDeptoIcon, nuevoDeptoText, "GREEN");
        private void OnLeaveNuevoDepto(object sender, MouseEventArgs e) => ChangeLeaveColor(btnNuevoDepto, nuevoDeptoIcon, nuevoDeptoText, "GREEN");
        private void OnHoverSeleccionar(object sender, MouseEventArgs e) => ChangeHoverColor(btnSeleccionar, null, seleccionarText, "BLUE");
        private void OnLeaveSeleccionar(object sender, MouseEventArgs e) => ChangeLeaveColor(btnSeleccionar, null, seleccionarText, "BLUE");
        private void OnHoverEliminar(object sender, MouseEventArgs e) => ChangeHoverColor(btnEliminar, null, eliminarText, "RED");
        private void OnLeaverEliminar(object sender, MouseEventArgs e) => ChangeLeaveColor(btnEliminar, null, eliminarText, "RED");
        private void OnHoverVolver(object sender, MouseEventArgs e) => ChangeHoverColor(btnVolver, volverIcon, volverText, "GREEN");
        private void OnLeaveVolver(object sender, MouseEventArgs e) => ChangeLeaveColor(btnVolver, volverIcon, volverText, "GREEN");
        private void OnHoverImagenes(object sender, MouseEventArgs e) => ChangeHoverColor(btnImagenes, null, imagenesText, "BLUE");
        private void OnLeaveImagenes(object sender, MouseEventArgs e) => ChangeLeaveColor(btnImagenes, null, imagenesText, "BLUE");

        public void FillDataGridDeptos()
        {
            deptoController = new DepartamentoController();
            deptos = deptoController.ObtenerDepartamentos(); // obtener departamentos
            
            if (deptos != null)
            {
                ObservableCollection<DeptoGrid> deptosGrid = new ObservableCollection<DeptoGrid>();
                List<DeptoGrid> deptosGridList = ConvertToDeptoGrid(deptos);

                foreach (DeptoGrid depto in deptosGridList)
                {
                    deptosGrid.Add(depto);
                }
                dataGridDeptos.ItemsSource = deptosGrid;
                contadorDeptos.Content = $"Total departamentos: {deptos.Count}";
            }
        }

        public List<DeptoGrid> ConvertToDeptoGrid(List<DepartamentoDTO> deptos)
        {
            List<DeptoGrid> deptosGridList = new List<DeptoGrid>();
            if (deptos != null)
            {
                foreach (DepartamentoDTO depto in deptos)
                {
                    DeptoGrid deptoGrid = new DeptoGrid();
                    deptoGrid.id = depto.id_departamento;
                    deptoGrid.rol = depto.rol;
                    deptoGrid.tipo = depto.tipo;
                    deptoGrid.superficie = depto.superficie.ToString() + " m2";
                    deptoGrid.valorDiario = depto.valorDiario.ToString("C", CultureInfo.CurrentCulture);
                    deptoGrid.comuna = depto.direccion.comuna;
                    deptoGrid.region = depto.direccion.region;
                    deptoGrid.estado = depto.estado;
                    deptosGridList.Add(deptoGrid);
                }
                return deptosGridList;
            }
            else
            {
                DeptoGrid defaultGrid = new DeptoGrid();
                deptosGridList.Add(defaultGrid.GetDefaultGrid());
                return deptosGridList;
            }
        }

        // CHANGE COLORS METHODS
        public void ChangeHoverColor(Tile tile, PackIconFontAwesome icon, TextBlock text, string color)
        {
            if (color.Equals("GREEN")) tile.Background = UIColors.NormalGreen;
            if (color.Equals("BLUE")) tile.Background = UIColors.Blue;
            if (color.Equals("RED")) tile.Background = UIColors.Red;

            if (icon != null) icon.Foreground = UIColors.White;
            if (text != null) text.Foreground = UIColors.White;
        }

        public void ChangeLeaveColor(Tile tile, PackIconFontAwesome icon, TextBlock text, string color)
        {
            switch (color)
            {
                case "GREEN":
                    if (icon != null) icon.Foreground = UIColors.NormalGreen;
                    if (text != null) text.Foreground = UIColors.NormalGreen;
                    break;
                case "BLUE":
                    if (icon != null) icon.Foreground = UIColors.Blue;
                    if (text != null) text.Foreground = UIColors.Blue;
                    break;
                case "RED":
                    if (icon != null) icon.Foreground = UIColors.Red;
                    if (text != null) text.Foreground = UIColors.Red;
                    break;
            }
            tile.Background = UIColors.White;
        }

        private void OpenNuevoDeptoWin(object sender, RoutedEventArgs e)
        {
            Nuevodepto nuevoDeptoWindow = new Nuevodepto(this);
            nuevoDeptoWindow.Show();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Dashboard dashWindow = new Dashboard();
            dashWindow.Show();
            Close();
        }

        private void EliminarDepto(object sender, RoutedEventArgs e)
        {
            if (dataGridDeptos.SelectedItem == null) return;
            if(MessageBox.Show("¿Esta seguro que desea eliminar este departamento?", "¡Atención!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                deptoController = new DepartamentoController();
                DeptoGrid selectedDepto = dataGridDeptos.SelectedItem as DeptoGrid;
                int id = Convert.ToInt32(selectedDepto.id);
                bool removed = deptoController.EliminarDepto(id);
                if (removed) FillDataGridDeptos();
            }
        }

        private void SeleccionarDepto(object sender, RoutedEventArgs e)
        {
            if(dataGridDeptos.SelectedItem != null)
            {
                DeptoGrid selectedDepto = dataGridDeptos.SelectedItem as DeptoGrid;
                Depto selectedDeptoWin = new Depto(this, selectedDepto.id);
                selectedDeptoWin.Show();
            }
        }

        private void OpenImagenes(object sender, RoutedEventArgs e)
        {
            if (dataGridDeptos.SelectedItem != null)
            {
                DeptoGrid selectedDepto = dataGridDeptos.SelectedItem as DeptoGrid;
                ImagenesWindow imgsWindow = new ImagenesWindow(selectedDepto.id);
                imgsWindow.Show();
            }
        }
    }
}
