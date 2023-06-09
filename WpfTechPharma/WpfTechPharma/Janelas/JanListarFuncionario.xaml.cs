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
    /// Lógica interna para JanListarFuncionario.xaml
    /// </summary>
    public partial class JanListarFuncionario : Window
    {
        public JanListarFuncionario()
        {
            InitializeComponent();
            CarregarFuncionarios();
        }

        private void CarregarFuncionarios()
        {
            try
            {
                List<Funcionario> funcionarios = new FuncionarioDAO().List();
                dgvFuncionarios.ItemsSource = funcionarios;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os funcionários: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
