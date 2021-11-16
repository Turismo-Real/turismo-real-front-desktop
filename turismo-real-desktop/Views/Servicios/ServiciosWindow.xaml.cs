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
using turismo_real_controller.Controllers.Servicio;
using turismo_real_desktop.GridEntities;
using turismo_real_desktop.UIElements;
using turismo_real_desktop.Views.Administrador;

namespace turismo_real_desktop.Views.Servicios
{
    public partial class ServiciosWindow : MetroWindow
    {
        public ServicioController servicioController;
        public List<ServicioDTO> servicios;

        public ServiciosWindow()
        {
            InitializeComponent();
            FillDataGridServicios();
            dataGridServicios.IsReadOnly = true;
        }

        public string GetSource(FrameworkElement src) => src.Name;
        private void OnHoverNuevoServicio(object sender, MouseEventArgs e) => ChangeHoverColor(btnNuevoServicio, nuevoServicioText, GetSource(e.OriginalSource as FrameworkElement), nuevoServicioIcon);
        private void OnLeaveNuevoServicio(object sender, MouseEventArgs e) => ChangeLeaveColor(btnNuevoServicio, nuevoServicioText, GetSource(e.OriginalSource as FrameworkElement), nuevoServicioIcon);
        private void OnHoverSeleccionar(object sender, MouseEventArgs e) => ChangeHoverColor(btnSeleccionar, seleccionarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnLeaveSeleccionar(object sender, MouseEventArgs e) => ChangeLeaveColor(btnSeleccionar, seleccionarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnHoverEliminar(object sender, MouseEventArgs e) => ChangeHoverColor(btnEliminar, eliminarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnLeaverEliminar(object sender, MouseEventArgs e) => ChangeLeaveColor(btnEliminar, eliminarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnHoverVolver(object sender, MouseEventArgs e) => ChangeHoverColor(btnVolver, volverText, GetSource(e.OriginalSource as FrameworkElement), volverIcon);
        private void OnLeaveVolver(object sender, MouseEventArgs e) => ChangeLeaveColor(btnVolver, volverText, GetSource(e.OriginalSource as FrameworkElement), volverIcon);

        public void FillDataGridServicios()
        {
            servicioController = new ServicioController();
            servicios = servicioController.ObtenerServicios();

            if (servicios != null)
            {
                ObservableCollection<ServicioGrid> serviciosGrid = new ObservableCollection<ServicioGrid>();
                List<ServicioGrid> serviciosGridList = ConvertToServicioGrid(servicios);

                foreach (ServicioGrid servicio in serviciosGridList)
                {
                    serviciosGrid.Add(servicio);
                }
                dataGridServicios.ItemsSource = serviciosGrid;
                contadorservicios.Text = $"Total servicios: {servicios.Count}";
            }
        }

        private List<ServicioGrid> ConvertToServicioGrid(List<ServicioDTO> servicios)
        {
            List<ServicioGrid> serviciosGridList = new List<ServicioGrid>();
            if (servicios != null)
            {
                foreach (ServicioDTO servicio in servicios)
                {
                    ServicioGrid servicioGrid = new ServicioGrid();
                    servicioGrid.idServicio = servicio.idServicio;
                    servicioGrid.nombre = servicio.nombre;
                    servicioGrid.valor = servicio.valor.ToString("C", CultureInfo.CurrentCulture);
                    servicioGrid.tipo = servicio.tipo;
                    serviciosGridList.Add(servicioGrid);
                }
                return serviciosGridList;
            }
            serviciosGridList.Add(new ServicioGrid());
            return serviciosGridList;
        }

        //Btn nuevo servicio
        private void OpenNuevoServicio(object sender, RoutedEventArgs e)
        {
            NuevoServicio nuevoServicioWin = new NuevoServicio(this);
            nuevoServicioWin.Show();
        }

        // Btn seleccionar servicio
        private void SeleccionarServicio(object sender, RoutedEventArgs e)
        {
            if (dataGridServicios.SelectedItem != null)
            {
                ServicioGrid selectedServicio = dataGridServicios.SelectedItem as ServicioGrid;
                ServicioWindow servicioWindow = new ServicioWindow(this, selectedServicio.idServicio);
                servicioWindow.Show();
            }
        }

        //Btn eliminar servicio
        private void EliminarServicio(object sender, RoutedEventArgs e)
        {
            if (dataGridServicios.SelectedItem == null) return;

            string title = "¡Atención!";
            string message = "¿Esta seguro que desea eliminar este departamento?";
            MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result.Equals(MessageBoxResult.Yes))
            {
                servicioController = new ServicioController();
                ServicioGrid selectedServicio = dataGridServicios.SelectedItem as ServicioGrid;
                int id = Convert.ToInt32(selectedServicio.idServicio);
                bool removed = servicioController.EliminarServicio(id);
                if (removed) FillDataGridServicios();
            }
        }

        //btn Volver 
        private void CloseWindow(object sender, RoutedEventArgs e) {
            Dashboard dashWindow = new Dashboard();
            dashWindow.Show();
            Close();
        }

        // CHANGE COLORS METHODS
        public void ChangeHoverColor(Tile tile, TextBlock text, string source, PackIconFontAwesome icon = null)
        {
            if (source.Equals("btnSeleccionar"))
            {
                tile.Background = UIColors.Blue;
                text.Foreground = UIColors.White;
            }
            else if (source.Equals("btnNuevoServicio") || source.Equals("btnVolver"))
            {
                tile.Background = UIColors.NormalGreen;
                text.Foreground = UIColors.White;
                if (icon != null) icon.Foreground = UIColors.White;
            }
            else if (source.Equals("btnEliminar"))
            {
                tile.Background = UIColors.Red;
                text.Foreground = UIColors.White;
            }
        }

        public void ChangeLeaveColor(Tile tile, TextBlock text, string source, PackIconFontAwesome icon = null)
        {
            tile.Background = UIColors.White;
            if (source.Equals("btnSeleccionar"))
                text.Foreground = UIColors.Blue;
            else if (source.Equals("btnNuevoServicio") || source.Equals("btnVolver"))
                text.Foreground = UIColors.NormalGreen;
                if (icon != null) icon.Foreground = UIColors.NormalGreen;
            else if (source.Equals("btnEliminar"))
                text.Foreground = UIColors.Red;
        }
    }
}
