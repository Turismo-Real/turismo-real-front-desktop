using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;
using turismo_real_business.DTOs;
using turismo_real_controller.Controllers.Imagen;
using turismo_real_desktop.GridEntities;
using turismo_real_desktop.UIElements;
using turismo_real_desktop.Views.Administrador.Departamentos;

namespace turismo_real_desktop.Views.Departamentos
{
    public partial class ImagenesWindow : MetroWindow
    {
        private ImagenController imagenController;
        private ImagenesDeptoDTO imagenesDepto;
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
            FillDataGridImagenes();
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

        public void FillDataGridImagenes()
        {
            imagenController = new ImagenController();
            imagenesDepto = imagenController.ObtenerImagenes(idDepartamento);
            List<ImagenGrid> imagenesGrid = ConvertToImagenGrid(imagenesDepto.imagenes);
            imagenesDataGrid.ItemsSource = imagenesGrid;
        }

        public List<ImagenGrid> ConvertToImagenGrid(List<ImagenDTO> imagenes)
        {
            List<ImagenGrid> imagenesGrid = new List<ImagenGrid>();
            if (imagenes != null)
            {
                foreach (ImagenDTO img in imagenes)
                {
                    ImagenGrid imagen = new ImagenGrid();
                    imagen.id = img.idImagen;
                    imagen.nombre = img.nombre;
                    imagen.formato = img.formato;
                    imagenesGrid.Add(imagen);
                }
                return imagenesGrid;
            }
            return new List<ImagenGrid>();
        }

        // CHANGE COLORS METHODS
        public void ChangeHoverColor(Tile tile, TextBlock text, string color)
        {
            text.Foreground = UIColors.White;
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

        private void ShowSelectedImage(object sender, SelectionChangedEventArgs e)
        {
            BitmapImage bitmap = new BitmapImage();
            try
            {
                ImagenGrid selectedImage = imagenesDataGrid.SelectedItem as ImagenGrid;
                string b64Image = imagenesDepto.imagenes.Find(x => x.idImagen == selectedImage.id).imagen;
                byte[] binaryImage = Convert.FromBase64String(b64Image);
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(binaryImage);
                bitmap.EndInit();
                imgDepto.Source = bitmap;
            } catch(Exception ex)
            {
                string errorImagePath = @"/Assets/errorImage.jpg";
                bitmap = new BitmapImage(new Uri(errorImagePath, UriKind.Relative));
                imgDepto.Source = bitmap;
            }
        }
    }
}
