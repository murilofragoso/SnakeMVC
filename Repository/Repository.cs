using Snake.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Repository
{
    public class Repository
    {
        private readonly ConexaoDB _conexao;

        public Repository()
        {
            _conexao = new ConexaoDB();
        }

        public string VerificarUsuario(string nomeJogador)
        {
            string senha = "";
            _conexao.Connect();
            _conexao.ExecutarProcedure("Buscar_Jogador");
            _conexao.AddParametro("@nome_jogador",nomeJogador);
            SqlDataReader leitor = _conexao.ExecutarReader();
            try
            {
                while (leitor.Read())
                {
                    senha = leitor.GetString(leitor.GetOrdinal("Senha_Jogador"));
                }
                _conexao.Disconect();
                return senha;
            }
            catch
            {
                _conexao.Disconect();
                return "usuario incorreto";
            }
        }

        public void NovoUsuario(string nomeJogador, string senhaJogador)
        {
            _conexao.Connect();
            _conexao.ExecutarProcedure("Novo_Jogador");
            _conexao.AddParametro("@nome_Jogador",nomeJogador);
            _conexao.AddParametro("@senha_Jogador", senhaJogador);
            try
            {
                _conexao.Executar();
            }
            catch (Exception)
            {
                throw;
            }
            _conexao.Disconect();
        }

        public bool ValidarNovoUsuario(string nomeJogador)
        {
            _conexao.Connect();
            _conexao.ExecutarProcedure("VerificarUsuario");
            _conexao.AddParametro("@nome_jogador",nomeJogador);
            SqlDataReader leitor = _conexao.ExecutarReader();
            if (leitor.Read())
            {
                _conexao.Disconect();
                return false;
            }
            else
            {
                _conexao.Disconect();
                return true;
            }

        }

        public List<PontuacaoDoJogador> RetornaTabela()
        {
            List<PontuacaoDoJogador> lista = new List<PontuacaoDoJogador>();
            _conexao.Connect();
            _conexao.ExecutarProcedure("ExibirTabela");
            SqlDataReader leitor = _conexao.ExecutarReader();
            while (leitor.Read())
            {
                PontuacaoDoJogador pontosAtuais = new PontuacaoDoJogador(
                    leitor.GetString(leitor.GetOrdinal("Nome_Jogador")),
                    leitor.GetInt32(leitor.GetOrdinal("Pontuacao"))
                    );

                lista.Add(pontosAtuais);
            }
            _conexao.Disconect();
            return lista;
        }

        public void SalvarPontos(string nome, int pontos)
        {
            _conexao.Connect();
            _conexao.ExecutarProcedure("SalvarPontos");
            _conexao.AddParametro("@nome_jogador",nome);
            _conexao.AddParametro("@pontuacao",pontos);
            try
            {
                _conexao.Executar();
            }
            catch (Exception)
            {
                throw;
            }
            _conexao.Disconect();
        }

    }
}
