﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MePresidentaServidor;
using System.Threading;

namespace President_Me
{
    public partial class Jogo : Form
    {
        public int jogador { get; set; }
        public int vez { get; set; }
        public string jogadorVez { get; set; }
        public string jogadorid { get; set; }
        public string jogadorsenha { get; set; }

        public string Cartas { get; set; }
        public static string[] jog { get; set; }
        public string ListarJogadores { get; set; }
        public string personagens { get; set; }
        public string voto { get; set; }
        public int contvoto { get; set; }

        public string personagem { get; set; }
        public int setor { get; set; }
        public string[,] matriz { get; set; } = new string[22, 4];
        public bool atualizacao { get; set; } = false;
        public string[] vot { get; set; } = { };
        public string presidente { get; set; }
        public bool hr_votar { get; set; } = false;

        /*-----------------------------------*/

        public int Jog_Id { get; set; }
        public string auxJog { get; set; }
        public string Jog_Senha { get; set; }
        public string Jog_Nome { get; set; }
        public int Part_Id { get; set; }

        public Jogo()
        {
            InitializeComponent();
            string versao = "3.0";
            Lobby f = new Lobby(versao);
            f.ShowDialog();

            timer_Verificavez.Enabled = true;

            this.Part_Id = Entrar_Partida.idpartida;
            this.Jog_Senha = Entrar_Partida.JogadorSenha;
            this.Jog_Nome = Entrar_Partida.nome_jogador;
            this.auxJog = Entrar_Partida.JogadorId;

            if (String.IsNullOrEmpty(auxJog))
            {
                this.Close();
            }
            else
                Jog_Id = int.Parse(auxJog);

            //DEVERÁ SER FEITO UM TRATAMENTO DE ERRO NO CÓDIGO DE LISTAR CARTAS
            Cartas = MePresidentaServidor.Jogo.ListarCartas(Jog_Id, Jog_Senha);
            lblCartas.Text = Cartas;

            ListarJogadores = MePresidentaServidor.Jogo.ListarJogadores(Part_Id);
            lblidjog.Text = ListarJogadores;

            this.personagens = MePresidentaServidor.Jogo.ListarPersonagens();
            lblnomejog.Text = personagens;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //votação
        private void btn_sim_Click(object sender, EventArgs e)
        {
            voto = "S";
            lblpontjog.Text = "SIM";
        }

        private void btn_nao_Click(object sender, EventArgs e)
        {
            voto = "N";
            lblpontjog.Text = "NÃO";
            contvoto++;
            if (contvoto > 3)
            {
                lblpontjog.Text = "NÃO PODE MAIS VOTAR (NÃO)";
                btn_nao.Enabled = false;
            }
        }

        public void votacao(string voto)
        {
            /*if(hr_votar == true)
            {*/
                string votar = MePresidentaServidor.Jogo.Votar(Jog_Id, Jog_Senha, voto);
                if (votar.Contains("ERRO"))
                {
                    MessageBox.Show(votar);
                }
            /*}*/
        }

        private void Jogo_Load(object sender, EventArgs e)
        {
            //timer_Verificavez.Enabled = true;
            /*if (Entrar_Partida.iniciou_partida == true)
            {
                timer_Verificavez.Enabled = true;
            }*/

            //ACRESCENTANDO DADOS NOS COMBO BOX

            //PERSONAGENS
            cbPersonagens.Items.Add("A");
            cbPersonagens.Items.Add("B");
            cbPersonagens.Items.Add("C");
            cbPersonagens.Items.Add("D");
            cbPersonagens.Items.Add("E");
            cbPersonagens.Items.Add("F");
            cbPersonagens.Items.Add("G");
            cbPersonagens.Items.Add("I");
            cbPersonagens.Items.Add("L");
            cbPersonagens.Items.Add("M");
            cbPersonagens.Items.Add("N");
            cbPersonagens.Items.Add("O");
            cbPersonagens.Items.Add("P");

            //SETORES
            cbSetores.Items.Add("1");
            cbSetores.Items.Add("2");
            cbSetores.Items.Add("3");
            cbSetores.Items.Add("4");

            //DECLARAÇÃO DA MATRIZ
            matriz[0, 0] = Convert.ToString(this.pos00.Location.X) + ',' + Convert.ToString(this.pos00.Location.Y) + ",false";
            matriz[0, 1] = Convert.ToString(this.pos01.Location.X) + ',' + Convert.ToString(this.pos01.Location.Y) + ",false";
            matriz[0, 2] = Convert.ToString(this.pos02.Location.X) + ',' + Convert.ToString(this.pos02.Location.Y) + ",false";
            matriz[0, 3] = Convert.ToString(this.pos03.Location.X) + ',' + Convert.ToString(this.pos03.Location.Y) + ",false";
            matriz[1, 0] = Convert.ToString(this.pos10.Location.X) + ',' + Convert.ToString(this.pos10.Location.Y) + ",false";
            matriz[1, 1] = Convert.ToString(this.pos11.Location.X) + ',' + Convert.ToString(this.pos11.Location.Y) + ",false";
            matriz[1, 2] = Convert.ToString(this.pos12.Location.X) + ',' + Convert.ToString(this.pos12.Location.Y) + ",false";
            matriz[1, 3] = Convert.ToString(this.pos13.Location.X) + ',' + Convert.ToString(this.pos13.Location.Y) + ",false";
            matriz[2, 0] = Convert.ToString(this.pos20.Location.X) + ',' + Convert.ToString(this.pos20.Location.Y) + ",false";
            matriz[2, 1] = Convert.ToString(this.pos21.Location.X) + ',' + Convert.ToString(this.pos21.Location.Y) + ",false";
            matriz[2, 2] = Convert.ToString(this.pos22.Location.X) + ',' + Convert.ToString(this.pos22.Location.Y) + ",false";
            matriz[2, 3] = Convert.ToString(this.pos23.Location.X) + ',' + Convert.ToString(this.pos23.Location.Y) + ",false";
            matriz[3, 0] = Convert.ToString(this.pos30.Location.X) + ',' + Convert.ToString(this.pos30.Location.Y) + ",false";
            matriz[3, 1] = Convert.ToString(this.pos31.Location.X) + ',' + Convert.ToString(this.pos31.Location.Y) + ",false";
            matriz[3, 2] = Convert.ToString(this.pos32.Location.X) + ',' + Convert.ToString(this.pos32.Location.Y) + ",false";
            matriz[3, 3] = Convert.ToString(this.pos33.Location.X) + ',' + Convert.ToString(this.pos33.Location.Y) + ",false";
            matriz[4, 0] = Convert.ToString(this.pos40.Location.X) + ',' + Convert.ToString(this.pos40.Location.Y) + ",false";
            matriz[4, 1] = Convert.ToString(this.pos41.Location.X) + ',' + Convert.ToString(this.pos41.Location.Y) + ",false";
            matriz[4, 2] = Convert.ToString(this.pos42.Location.X) + ',' + Convert.ToString(this.pos42.Location.Y) + ",false";
            matriz[4, 3] = Convert.ToString(this.pos43.Location.X) + ',' + Convert.ToString(this.pos43.Location.Y) + ",false";
            matriz[5, 0] = Convert.ToString(this.pos40.Location.X) + ',' + Convert.ToString(this.pos40.Location.Y) + ",false";
            matriz[5, 1] = Convert.ToString(this.pos41.Location.X) + ',' + Convert.ToString(this.pos41.Location.Y) + ",false";
            matriz[5, 2] = Convert.ToString(this.pos42.Location.X) + ',' + Convert.ToString(this.pos42.Location.Y) + ",false";
            matriz[5, 3] = Convert.ToString(this.pos43.Location.X) + ',' + Convert.ToString(this.pos43.Location.Y) + ",false";
            matriz[6, 0] = Convert.ToString(this.pos43.Location.X) + ',' + Convert.ToString(this.pos43.Location.Y) + ",false";

            presidente = "";
        }

        private void btnColocar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.cbPersonagens.Text) || String.IsNullOrEmpty(this.cbSetores.Text))
            {
                lblJogo.Text = "ESCOLHA O PERSONAGEM E DESTINO";
                //MessageBox.Show("Selecione o personagem e o destino.");
                return;
            }
            moverPersonagem(cbPersonagens.Text.Substring(0, 1), Convert.ToInt32(cbSetores.Text), true);
            lblJogo.Text = "";
        }

        public int moverPersonagem(string personagem, int setor, bool serv)
        {
            string[] aux = { };
            bool entrou = true;

            if (serv)
            {
                string ColocarPersonagem = MePresidentaServidor.Jogo.ColocarPersonagem(Jog_Id, Jog_Senha, setor, personagem);
                txthistorico.Text = ColocarPersonagem;
                if (ColocarPersonagem.Contains("ERRO"))
                {
                    MessageBox.Show(ColocarPersonagem);
                    return -1;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                aux = matriz[(Convert.ToInt32(setor) - 1), i].Split(',');
                if (aux[2] == "false")
                {
                    if (cbPersonagens.Text == "A")
                    {
                        A.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        cbPersonagens.Items.Remove("A");
                        entrou = true;
                        break;
                    }
                    else if (cbPersonagens.Text == "B")
                    {
                        B.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        cbPersonagens.Items.Remove("B");
                        entrou = true;
                        break;
                    }
                    else if (cbPersonagens.Text == "C")
                    {
                        C.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        cbPersonagens.Items.Remove("C");
                        entrou = true;
                        break;
                    }
                    else if (cbPersonagens.Text == "D")
                    {
                        D.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        cbPersonagens.Items.Remove("D");
                        entrou = true;
                    }
                    else if (cbPersonagens.Text == "E")
                    {
                        E.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        cbPersonagens.Items.Remove("E");
                        entrou = true;
                        break;
                    }
                    else if (cbPersonagens.Text == "F")
                    {
                        F.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        cbPersonagens.Items.Remove("F");
                        entrou = true;
                    }
                    else if (cbPersonagens.Text == "G")
                    {
                        G.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        cbPersonagens.Items.Remove("G");
                        entrou = true;
                        break;
                    }
                    else if (cbPersonagens.Text == "I")
                    {
                        I.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        cbPersonagens.Items.Remove("I");
                        entrou = true;
                        break;
                    }
                    else if (cbPersonagens.Text == "L")
                    {
                        L.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        cbPersonagens.Items.Remove("L");
                        entrou = true;
                        break;
                    }
                    else if (cbPersonagens.Text == "M")
                    {
                        M.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        cbPersonagens.Items.Remove("M");
                        entrou = true;
                        break;
                    }
                    else if (cbPersonagens.Text == "N")
                    {
                        N.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        cbPersonagens.Items.Remove("N");
                        entrou = true;
                        break;
                    }
                    else if (cbPersonagens.Text == "O")
                    {
                        O.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        cbPersonagens.Items.Remove("O");
                        entrou = true;
                        break;
                    }
                    else if (cbPersonagens.Text == "P")
                    {
                        P.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        cbPersonagens.Items.Remove("P");
                        entrou = true;
                        break;
                    }
                    cbPersonagens.Items.Remove(personagem);
                    return 1;
                }
                if (entrou == false)
                {
                    //MessageBox.Show("Nível cheio");
                    matriz[(Convert.ToInt32(cbSetores) - 1), i] = aux[0] + ',' + aux[1] + ',' + true;
                }
                aux = matriz[6, 0].Split(',');
                if (aux[2] == "true")
                {
                    votacao(voto);
                }
            }
            return 0;
        }

        public void promover(string personagem)
        {
            string promover = MePresidentaServidor.Jogo.Promover(Jog_Id, Jog_Senha, personagem);
            txthistorico.Text = promover;

            string[] vot = { };
            string vota = txthistorico.Text;
            vota = vota.Replace("\r","");
            vot = vota.Split(',');
            string setor = vot[0];

            if(Convert.ToInt32(setor) == 10)
            {
                lblJogo.Text = "HORA DE VOTAR";
                votacao(voto);
            }
            
        }

        private void timer_Verificavez_Tick(object sender, EventArgs e)
        {
            string verificavez = MePresidentaServidor.Jogo.VerificarVez(Jog_Id);
            verificavez = verificavez.Replace("\r", "");
            string[] tabuleiro = verificavez.Split('\n');
            string jogadorDaVez = tabuleiro[0];
            lbljogadorvez.Text = jogadorDaVez;
            string[] personagem = { };
            atualizacao = true;

            if (jogadorDaVez.Contains(Convert.ToString(Jog_Id)))
            {
                lblJogo.Text = "SUA VEZ DE JOGAR";
                this.btnColocar.Enabled = true;
                //moverPersonagem(cbPersonagens.Text.Substring(0, 1), Convert.ToInt32(cbSetores.Text), true);
            }
            else
            {
                lblJogo.Text = "AGUARDE SUA VEZ";
                this.btnColocar.Enabled = false;

            }

            for (int i = 0; i < tabuleiro.Length - 1; i++)
            {
                this.personagem = txthistorico.Text;
                personagem = tabuleiro[i].Split(',');
                string num = tabuleiro[0];
                string letra = tabuleiro[1];
                if (cbPersonagens.Items.Contains(letra) && atualizacao == true)
                {
                    atualizaPersonagem(letra, Convert.ToInt32(num));
                }
                else if (cbPersonagens.Items.Contains(letra))
                {
                    moverPersonagem(letra, Convert.ToInt32(num), false);
                }

            }

        }

        public void atualizaPersonagem(string personagem, int setor)
        {
            string[] aux = { };
            bool entrou = true;

            for (int i = 0; i < 4; i++)
            {
                aux = matriz[(Convert.ToInt32(setor) - 1), i].Split(',');
                if (aux[2] == "false")
                {
                    if (personagem == "A")
                    {
                        A.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        entrou = true;
                        break;
                    }
                    else if (personagem == "B")
                    {
                        B.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        entrou = true;
                        break;
                    }
                    else if (personagem == "C")
                    {
                        C.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        entrou = true;
                        break;
                    }
                    else if (personagem == "D")
                    {
                        D.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        entrou = true;
                    }
                    else if (personagem == "E")
                    {
                        E.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        entrou = true;
                        break;
                    }
                    else if (personagem == "F")
                    {
                        F.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        entrou = true;
                    }
                    else if (personagem == "G")
                    {
                        G.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        entrou = true;
                        break;
                    }
                    else if (personagem == "I")
                    {
                        I.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        entrou = true;
                        break;
                    }
                    else if (personagem == "L")
                    {
                        L.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        entrou = true;
                        break;
                    }
                    else if (personagem == "M")
                    {
                        M.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        entrou = true;
                        break;
                    }
                    else if (personagem == "N")
                    {
                        N.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        entrou = true;
                        break;
                    }
                    else if (personagem == "O")
                    {
                        O.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        entrou = true;
                        break;
                    }
                    else if (personagem == "P")
                    {
                        P.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        entrou = true;
                        break;
                    }
                }
            }
        }

        private void btn_promover_Click(object sender, EventArgs e)
        {
            string personagem = lblpromover.Text;
            promover(personagem);
        }



        /*public void atualizaTabuleiro(string tabuleiro)
        {
           string[] personagem;
           tabuleiro = verificavez.Split('\n');
           if (tabuleiro.Length > 2)
           {
               for (int i = 0; i < tabuleiro.Length - 1; i++)
               {
                   personagem = tabuleiro[i].Split(',');
                   if (cbPersonagens.Items.Contains(personagem[1]) && atualizacao == true)
                   {
                       atualizaPersonagem(personagem[1], Convert.ToInt32(personagem[0]));
                   }
                   else if (cbPersonagens.Items.Contains(personagem[1]))
                   {
                       moverPersonagem(personagem[1], Convert.ToInt32(personagem[0]), false);
                   }
               }
           }
        }*/
    }
}