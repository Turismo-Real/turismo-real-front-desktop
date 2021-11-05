﻿using MahApps.Metro.Controls;
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

namespace turismo_real_desktop.Views.Usuarios
{

    public partial class UsuarioMessage : MetroWindow
    {
        public UsuarioMessage(string defaultPassword)
        {
            InitializeComponent();
            lblPass.Text = defaultPassword;
        }

        private void OnHoverAceptar(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveAceptar(object sender, MouseEventArgs e)
        {

        }

        private void ClickAceptar(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}