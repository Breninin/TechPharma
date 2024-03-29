﻿using MySqlX.XDevAPI;
using System;
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
using WpfTechPharma.Auxiliares;
using WpfTechPharma.Modelos;

namespace WpfTechPharma.Janelas
{
    /// <summary>
    /// Lógica interna para JanCadastrarServico.xaml
    /// </summary>
    public partial class JanCadastrarServico : Window
    {
        private int _id = 0;
        private Servico _servico = new Servico();
        private bool _update = false;

        public JanCadastrarServico()
        {
            InitializeComponent();
            InitializeEventHandlers();
            //LoadData();
        }

        public JanCadastrarServico(int id)
        {
            _id = id;

            InitializeComponent();
            InitializeEventHandlers();
            FillForm();
            //LoadData();
        }

        // Inicializa os manipuladores de eventos
        private void InitializeEventHandlers()
        {
            edNome.TextChanged += TextBox_TextChanged;
            edDuracao.TextChanged += TextBox_TextChanged;
            edTipo.TextChanged += TextBox_TextChanged;
            edValor.TextChanged += TextBox_TextChanged;
        }

        // Manipulador de evento para a alteração do texto nos campos TextBox
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Utils.Check(this, textBox);
        }

        // Manipulador de evento para a alteração da seleção no ComboBox
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Utils.Check(this, comboBox);
        }

        private void FillForm()
        {
            var servicoDAO = new ServicoDAO();
            _servico = servicoDAO.GetById(_id);

            edNome.Text = _servico.Nome;
            edDuracao.Text = _servico.Duracao;
            edTipo.Text = _servico.Tipo;
            edValor.Text = _servico.ValorVenda.ToString();

            _update = true;
        }

        /*
        // Carrega os dados iniciais
        private void LoadData()
        {
            try
            {
                
                cbInsumos.ItemsSource = null;
                cbInsumos.Items.Clear();
                cbInsumos.ItemsSource = new InsumoDAO().List();
                cbInsumos.DisplayMemberPath = "Nome";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Não Executado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        */

        private void btLimpar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Deseja realmente limpar?", "Aviso", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Utils.ResetControls(this);
                _update = false;
            }
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            List<bool> check = new List<bool>
            {
                Utils.Check(this, edNome),
                Utils.Check(this, edDuracao),
                Utils.Check(this, edTipo),
                Utils.Check(this, edValor),
            };

            if (check.All(c => c))
            {
                try
                {
                    if (_update)
                    {
                        var servico = new Servico
                        {
                            Id = _id,
                            Nome = edNome.Text,
                            Duracao = edDuracao.Text,
                            Tipo = edTipo.Text,
                            ValorVenda = float.Parse(edValor.Text),
                        };

                        var servicoDAO = new ServicoDAO();

                        servicoDAO.Update(servico);
                        MessageBox.Show("Serviço atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        _update = false;
                    }
                    else
                    {
                        var servico = new Servico
                        {
                            Nome = edNome.Text,
                            Duracao = edDuracao.Text,
                            Tipo = edTipo.Text,
                            ValorVenda = float.Parse(edValor.Text),
                        };

                        var servicoDAO = new ServicoDAO();
                        var resultado = servicoDAO.Insert(servico);
                        MessageBox.Show(resultado, "Resultado", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao inserir o serviço: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                Utils.ResetControls(this);
                this.Close();
            }
            else
            {
                check.Clear();
            }
        }
    }
}
