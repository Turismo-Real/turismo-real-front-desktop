using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using turismo_real_business.DTOs;
using turismo_real_controller.Controllers.Imagen;
using turismo_real_desktop.GridEntities;
using turismo_real_desktop.UIElements;

namespace turismo_real_desktop.Views.Departamentos
{
    public partial class ImagenesWindow : MetroWindow
    {
        private ImagenController imagenController;
        private ImagenesDeptoDTO imagenesDepto;
        private readonly int idDepartamento;

        public ImagenesWindow()
        {
            InitializeComponent();
        }

        public ImagenesWindow(int idDepartamento)
        {
            InitializeComponent();
            this.idDepartamento = idDepartamento;
            tituloText.Text = $"Departamento {idDepartamento}";
            FillDataGridImagenes();
        }

        private void OnHoverAgregar(object sender, MouseEventArgs e) => ChangeHoverColor(btnAgregar, agregarText, "BLUE");
        private void OnLeaveAgregar(object sender, MouseEventArgs e) => ChangeLeaveColor(btnAgregar, agregarText, "BLUE");
        private void OnHoverEliminar(object sender, MouseEventArgs e) => ChangeHoverColor(btnEliminar, eliminarText, "RED");
        private void OnLeaveEliminar(object sender, MouseEventArgs e) => ChangeLeaveColor(btnEliminar, eliminarText, "RED");
        private void OnHoverVolver(object sender, MouseEventArgs e) => ChangeHoverColor(btnVolver, volverText, "GREEN");
        private void OnLeaveVolver(object sender, MouseEventArgs e) => ChangeLeaveColor(btnVolver, volverText, "GREEN");
        private void Volver(object sender, RoutedEventArgs e) => Close();

        private void AgregarImagen(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.JPG;*.PNG)|*.JPG;*.PNG";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                string extension = Path.GetExtension(openFileDialog.FileName).Remove(0, 1);
                byte[] imageArray = File.ReadAllBytes(openFileDialog.FileName);
                string base64Image = Convert.ToBase64String(imageArray);

                imagenController = new ImagenController();
                bool saved = imagenController.AgregarImagen(idDepartamento, fileName, extension, base64Image);

                if (saved)
                {
                    FillDataGridImagenes();
                    imagenesDataGrid.SelectedIndex = imagenesDataGrid.Items.Count - 1;
                }
                else
                {
                    string title = "Error al cargar imagen";
                    string message = "Ha ocurrido un error al intentar cargar la imagen.";
                    MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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
            if(imagenesDataGrid.SelectedItem != null)
            {
                BitmapImage bitmap = new BitmapImage();
                try
                {
                    ImagenGrid selectedImage = imagenesDataGrid.SelectedItem as ImagenGrid;
                    ImagenDTO imagenDepto = imagenesDepto.imagenes.Find(x => x.idImagen == selectedImage.id);

                    string b64Image = imagenDepto.imagen;
                    byte[] binaryImage = Convert.FromBase64String(b64Image);
                    bitmap.BeginInit();
                    bitmap.StreamSource = new MemoryStream(binaryImage);
                    bitmap.EndInit();
                    imgDepto.Source = bitmap;
                }
                catch (Exception ex)
                {
                    string errorImagePath = @"/Assets/errorImage.jpg";
                    bitmap = new BitmapImage(new Uri(errorImagePath, UriKind.Relative));
                    imgDepto.Source = bitmap;
                }
            }
        }

    }
}
