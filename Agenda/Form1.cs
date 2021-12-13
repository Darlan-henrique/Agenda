using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Agenda
{
    public partial class Form1 : Form
    {
        SqlConnection conexao = new SqlConnection(
            "Data Source=.;Initial " +
            "Catalog=Agenda;Integrated Security=True");
        SqlCommand command;
        SqlDataAdapter adapter;
        int ID = 0;

        public Form1()
        {
            InitializeComponent();
            ExibirDados();
        }

        private void ExibirDados()
        {
            try
            {
                conexao.Open();
                DataTable dt = new DataTable();
                adapter = new SqlDataAdapter("SELECT * FROM Contatos", conexao);
                adapter.Fill(dt);
                dvgAgenda.DataSource = dt;

            }
            catch
            {
                throw;
            }
            finally
            {
                conexao.Close();
            }
        }

        private void LimparDados()
        {
            txtNome.Text = "";
            txtEndereco.Text = "";
            txtCelular.Text = "";
            txtTelefone.Text = "";
            txtEmail.Text = "";
            ID = 0;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja Sair do programa ?", "Agenda", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }

            else
            {
                txtNome.Focus();
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtNome.Text = "";
            txtEndereco.Text = "";
            txtCelular.Text = "";
            txtTelefone.Text = "";
            txtEmail.Text = "";
            txtNome.Focus();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text != "" 
                && txtEndereco.Text != "" 
                && txtCelular.Text != "" 
                && txtTelefone.Text != ""
                && txtEmail.Text != "")
            {
                try
                {
                    command = new SqlCommand(" INSERT INTO Contatos (nome,endereco,celular,telefone,email) VALUES(@nome,@endereco,@celular,@telefone,@email)", conexao);
                    conexao.Open();
                    command.Parameters.AddWithValue("@nome",txtNome.Text.ToUpper());
                    command.Parameters.AddWithValue("@endereco", txtEndereco.Text.ToUpper());
                    command.Parameters.AddWithValue("@celular", txtCelular.Text.ToUpper());
                    command.Parameters.AddWithValue("@telefone", txtTelefone.Text.ToUpper());
                    command.Parameters.AddWithValue("@email", txtEmail.Text.ToLower());
                    command.ExecuteNonQuery();
                    MessageBox.Show("Registro incluído com sucesso...");

                }
                catch(Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
                finally
                {
                    conexao.Close();
                    ExibirDados();
                    LimparDados();
                }
            }
            else
            {
                MessageBox.Show("Informe todos os dados requeridos");
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text != "" 
                && txtEndereco.Text != ""
                && txtCelular.Text != ""
                && txtTelefone.Text != ""
                && txtEmail.Text != "")
            {
                try
                {
                    command = new SqlCommand("UPDATE Contatos SET nome =@nome, endereco =@endereco,celular =@celular,telefone =@telefone,email =@email WHERE id=@id", conexao);
                    conexao.Open();
                    command.Parameters.AddWithValue("@id", ID);
                    command.Parameters.AddWithValue("@nome", txtNome.Text.ToUpper());
                    command.Parameters.AddWithValue("@endereco", txtEndereco.Text.ToUpper());
                    command.Parameters.AddWithValue("@celular", txtCelular.Text.ToUpper());
                    command.Parameters.AddWithValue("@telefone", txtTelefone.Text.ToUpper());
                    command.Parameters.AddWithValue("@email", txtEmail.Text.ToLower());
                    command.ExecuteNonQuery();
                    MessageBox.Show("Registro atualizado com sucesso...");

                }

                catch(Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
                finally
                {
                    conexao.Close();
                    ExibirDados();
                    LimparDados();
                }

            }

            else
            {
                MessageBox.Show("Informe todos os dados requeridos");
            }
        }

        private void bntDeletar_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                if(MessageBox.Show("Deseja Deletar este registro ?", "Agenda", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        command = new SqlCommand("DELETE Contatos WHERE id=@id", conexao);
                        conexao.Open();
                        command.Parameters.AddWithValue("@id", ID);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Registro deletado com sucesso...!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro : " + ex.Message);
                    }
                    finally
                    {
                        conexao.Close();
                        ExibirDados();
                        LimparDados();
  
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione um registro para deletar");
            }
        }

        private void dvgAgenda_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           try
            {
                ID = Convert.ToInt32(dvgAgenda.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtNome.Text = dvgAgenda.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtEndereco.Text = dvgAgenda.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtCelular.Text = dvgAgenda.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtTelefone.Text = dvgAgenda.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtEmail.Text = dvgAgenda.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
            catch { }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Adptado por Henrique Darlan .NET", "Agenda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtNome.Focus();
        }
    }
}
