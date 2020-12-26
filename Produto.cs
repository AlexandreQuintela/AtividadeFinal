using System;
using System.Collections.Generic;
using System.Text;

namespace AtividadeFinal
{
    public class Produto
    {        
        public int Codigo { get { return codigo; } set { codigo = value; } }
        public string Descricao { get { return descricao; } set { descricao = value; } }
        public double Preco { get { return preco; } set { preco = value; } }
        public double Custo { get { return custo; } set { custo = value; } }

        private int codigo;
        private string descricao;
        private double preco;
        private double custo;
    }
}
