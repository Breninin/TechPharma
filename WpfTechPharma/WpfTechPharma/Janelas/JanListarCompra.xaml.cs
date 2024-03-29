﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfTechPharma.Modelos;

namespace WpfTechPharma.Janelas
{
    /// <summary>
    /// Lógica interna para JanListarCompra.xaml
    /// </summary>
    public partial class JanListarCompra : Window
    {
        public JanListarCompra()
        {
            InitializeComponent();
            CarregarCompras();
        }

        private void CarregarCompras()
        {
            try
            {
                CompraDAO CompraDAO = new CompraDAO();
                List<Compra> Compras = CompraDAO.List();
                dgvCompras.ItemsSource = Compras;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os Compras: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
