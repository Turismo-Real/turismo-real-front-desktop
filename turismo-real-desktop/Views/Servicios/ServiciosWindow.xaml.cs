using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using turismo_real_business.DTOs;
using turismo_real_controller.Controllers.Servicio;
using turismo_real_desktop.GridEntities;
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

        public void FillDataGridServicios()
        {
            servicioController = new ServicioController();
            servicios = servicioController.ObtenerServicios();

            ObservableCollection<ServicioGrid> serviciosGrid = new ObservableCollection<ServicioGrid>();
            List<ServicioGrid> serviciosGridList = ConvertToServicioGrid(servicios);

            foreach (ServicioGrid servicio in serviciosGridList)
            {
                serviciosGrid.Add(servicio);
            }
            dataGridServicios.ItemsSource = serviciosGrid;
            contadorservicios.Text = $"Total servicios: {servicios.Count}";
        }

        private List<ServicioGrid> ConvertToServicioGrid(List<ServicioDTO> servicios)
        {
            List<ServicioGrid> serviciosGridList = new List<ServicioGrid>();

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

        //Btn nuevo servicio
        private void OpenNuevoServicio(object sender, RoutedEventArgs e)
        {
            NuevoServicio nuevoServicioWin = new NuevoServicio(this);
            nuevoServicioWin.Show();
        }

        private void OnHoverNuevoServicio(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveNuevoServicio(object sender, MouseEventArgs e)
        {

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

        private void OnHoverSeleccionar(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveSeleccionar(object sender, MouseEventArgs e)
        {

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

        private void OnHoverEliminar(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaverEliminar(object sender, MouseEventArgs e)
        {

        }

        //btn Volver 
        private void CloseWindow(object sender, RoutedEventArgs e) {
            Dashboard dashWindow = new Dashboard();
            dashWindow.Show();
            Close();
        }

        private void OnHoverVolver(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveVolver(object sender, MouseEventArgs e)
        {

        }
    }
}
