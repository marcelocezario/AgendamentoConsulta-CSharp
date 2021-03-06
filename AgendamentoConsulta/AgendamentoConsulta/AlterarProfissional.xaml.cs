﻿using System;
using Controller;
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
using Model;
using Model.DAL;
using MahApps.Metro.Controls;


namespace AgendamentoConsulta
{

    /// <summary>
    /// Interaction logic for Produto.xaml
    /// </summary>
    public partial class AlterarProfissional : MetroWindow
    {
        public AlterarProfissional()
        {
            InitializeComponent();
            ProfissionalController proControl = new ProfissionalController();
            // Inicializa a listagem de Profissional
            DgDados.ItemsSource = proControl.ListarProfissionais();
        }

        //
        private void EditarProfissional()
        {
            ProfissionalController prControl = new ProfissionalController();
           

            Profissional pr = new Profissional
            {
                Nome = BoxNomeProfissional.Text,
                Cpf = BoxCpfProfissional.Text,
                Celular = BoxContatoProfissional.Text,
                DtNascimento = DatePickerDtNascimentoProfissional.SelectedDate.Value,
                Especialidade = BoxEspecialidadeProfissional.Text,
                Email = BoxEmailProfissional.Text,
                ResgistroProfissional = BoxResgistroProfissional.Text,

                Domingo = CheckBoxDomingo.IsChecked,
                Segunda = CheckBoxSegunda.IsChecked,
                Terca = CheckBoxTerca.IsChecked,
                Quarta = CheckBoxQuarta.IsChecked,
                Quinta = CheckBoxQuinta.IsChecked,
                Sexta = CheckBoxSexta.IsChecked,
                Sabado = CheckBoxSabado.IsChecked,

                HrInicio = TimePickerHInicioProfissional.SelectedTime.Value,
                HrFim = TimePickerHFimProfissional.SelectedTime.Value,
            };


            if (prControl.EditarProfissional(int.Parse(TxtProfissionalID.Text), pr))
                {                   
                    ListagemProfissional();
                    MessageBox.Show("Profissional omf");
            }            
            else
            {
                MessageBox.Show("Profissional vazio");

            }
        }

        private void ExcluirProfissional()
        {
            // Metódo para excluir o insumo no banco
            ProfissionalController prControl = new ProfissionalController();

            Profissional pr = prControl.PesquisarPorID(Convert.ToInt32(TxtProfissionalID.Text));

            if (pr != null)
            {
                if (prControl.ExcluirProfissional(pr.ID))
                {
                    ListagemProfissional(); // Lista os Profissional atualizados
                    LimpaCampos(); //  Limpa campos antigos
                    MessageBox.Show("ox");
                }
            }
            else
            {
                MessageBox.Show("Profissional não selecionado");
            }
        }

        private void PesquisarProfissionalPorNome()
        {
            // Metódo que pesquisa por nome e exibe o campo  buscado
            ProfissionalController prControl = new ProfissionalController();

            try
            {
                var consulta = from c in ContextoSingleton.Instancia.Profissionais
                               where c.Nome.Equals(BoxNomeProfissional.Text)
                               select c;

                DgDados.ItemsSource = consulta.ToList();
            }
            catch {
                MessageBox.Show("Profissional não encontrado");
            }


        }

        private void ListagemProfissional()
        {
            //Metodo para listar os Profissional no DataGrid
            ProfissionalController prControl = new ProfissionalController();

            DgDados.ItemsSource = prControl.ListarProfissionais();

        }
        //limpar campos 
        private void LimpaCampos()
        {
            BoxNomeProfissional.Clear();
            BoxCpfProfissional.Clear();
            BoxContatoProfissional.Clear();
            BoxEspecialidadeProfissional.Clear();
            BoxEmailProfissional.Clear();
            BoxResgistroProfissional.Clear();
        }

        /// <summary>
        /// botoes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Editar_Profissional_Click(object sender, RoutedEventArgs e)
        {
            EditarProfissional();
        }

        private void Excluir_Profissional_Click(object sender, RoutedEventArgs e)
        {
            ExcluirProfissional();
        }

        private void Pesquisar_Profissional_Click(object sender, RoutedEventArgs e)
        {
            PesquisarProfissionalPorNome();
        }
        //carega a lista
        private void DgDados_Loaded(object sended, RoutedEventArgs e)
        {
            this.ListagemProfissional();
        }

        //carrega objeto selecionado para os textbox
        private void DgDados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DgDados.SelectedIndex >= 0)
            {
                Profissional pr = (Profissional)DgDados.SelectedItem;

                TxtProfissionalID.Text = pr.ID.ToString();
                BoxNomeProfissional.Text = pr.Nome;
                BoxCpfProfissional.Text = pr.Cpf;
                BoxContatoProfissional.Text = pr.Celular;
                DatePickerDtNascimentoProfissional.SelectedDate = pr.DtNascimento;
                BoxEspecialidadeProfissional.Text = pr.Especialidade;
                BoxEmailProfissional.Text = pr.Email;
                BoxResgistroProfissional.Text = pr.ResgistroProfissional;

                CheckBoxDomingo.IsChecked = pr.Domingo;
                CheckBoxSegunda.IsChecked = pr.Segunda;
                CheckBoxTerca.IsChecked = pr.Terca;
                CheckBoxQuarta.IsChecked = pr.Quarta;
                CheckBoxQuinta.IsChecked = pr.Quinta;
                CheckBoxSexta.IsChecked = pr.Sexta;
                CheckBoxSabado.IsChecked = pr.Sabado;

                TimePickerHInicioProfissional.SelectedTime = pr.HrInicio;
                TimePickerHFimProfissional.SelectedTime = pr.HrFim;
            }
            else
            {
                MessageBox.Show("Profissional vazio");
            }
        }
    }

}



