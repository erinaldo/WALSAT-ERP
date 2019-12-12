using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.UTILS
{
    public enum Modo
    {
        Cadastrar,
        Alterar
    }

    public enum Modelo
    {
        NFe = 55,
        NFCe = 65,
        NFSe,
        CTe = 57,
        MDFe
    }

    public enum Ambiente
    {
        Producao = 1,
        Homologacao = 2
    }

    public enum Movimento
    {
        Entrada = 0,
        Saida = 1
    }

    public enum Operacao
    {
        Interna = 1, // 1=Operação interna;
        Interestadual = 2, // 2=Operação interestadual;
        Internacional = 3 // 3=Operação com exterior
    }

    public enum ImpressaoDANFE
    {
        Sem = 0, // 0=Sem geração de DANFE;
        NormalRetrato = 1, // 1=DANFE normal, Retrato;
        NormalPaisagem = 2, // 2=DANFE normal, Paisagem;
        Simplificado = 3, // 3=DANFE Simplificado;
        NFCe = 4, // 4=DANFE NFC-e;
        NFCeEmail = 5 // 5=DANFE NFC-e em mensagem eletrônica (o envio de mensagem eletrônica pode ser feita de forma simultânea com a impressão do DANFE; usar o tpImp = 5 quando esta for a única forma de disponibilização do DANFE).
    }

    public enum Emissao
    {
        Normal = 1, // 1=Emissão normal (não em contingência);
        ContigenciaFSIA = 2, // 2=Contingência FS-IA, com impressão do DANFE em formulário de segurança;
        ContigenciaSCAN = 3, // 3=Contingência SCAN (Sistema de Contingência do Ambiente Nacional);
        ContigenciaDPEC = 4, // 4=Contingência DPEC (Declaração Prévia da Emissão em Contingência);
        ContigenciaFSDA = 5, // 5=Contingência FS-DA, com impressão do DANFE e formulário de segurança;
        ContigenciaSVCAN = 6, // 6=Contingência SVC-AN (SEFAZ Virtual de Contingência do AN);
        ContigenciaSVCRS = 7, // 7=Contingência SVC-RS (SEFAZ Virtual de Contingência do RS);
        ContigenciaOFFLINE = 9, // 9=Contingência off-line da NFC-e (as demais opções de contingência são válidas também para a NFC-e). Para a NFC-e somente estão disponíveis e são válidas as opções de contingência 5 e 9
    }

    public enum Finalidade
    {
        Normal = 1, // 1=NF-e normal;
        Complementar = 2, // 2=NF-e complementar;
        Ajuste = 3, // 3=NF-e de ajuste;
        Devolucao = 4 // 4=Devolução de mercadoria
    }

    public enum OperacaoPresencial
    {
        NaoSeAplica = 0, // 0=Não se aplica (por exemplo, Nota Fiscal complementar ou de ajuste);
        Presencial = 1, // 1=Operação presencial;
        Internet = 2, // 2=Operação não presencial, pela Internet;
        Teleatendimento = 3, // 3=Operação não presencial, Teleatendimento;
        EntregaDomicilio = 4, // 4=NFC-e em operação com entrega a domicílio;
        Outros = 9 // 9=Operação não presencial, outros.
    }

    public enum Pagamento
    {
        Avista = 0,
        Aprazo = 1,
        Outros = 2
    }

    public enum Simbologia
    {
        Nenhuma,
        Moeda,
        Porcentagem
    }
}