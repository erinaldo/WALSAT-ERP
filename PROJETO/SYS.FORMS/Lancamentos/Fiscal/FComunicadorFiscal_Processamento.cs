using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SYS.UTILS;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Security.Cryptography.Xml;
using System.Linq;
using SYS.QUERYS;
using SYS.QUERYS.Lancamentos.Comercial;
using SYS.QUERYS.Cadastros.Fiscal;
using SYS.QUERYS.Cadastros.Relacionamento;
using SYS.QUERYS.Cadastros.Estoque;
using System.Data.Linq;

namespace SYS.FORMS.Lancamentos.Fiscal
{
    public partial class FComunicadorFiscal_Processamento : SYS.FORMS.FBase
    {
        public FComunicadorFiscal_Processamento()
        {
            InitializeComponent();
        }

        public List<X509Certificate2> Certificados()
        {
            var store = new X509Store("My");
            store.Open(OpenFlags.ReadOnly);

            var retorno = store.Certificates.Cast<X509Certificate2>().ToList();

            store.Close();

            return retorno;
        }
        
        private int DigitoVerificador(string chave)
        {
            if (!chave.TemValor())
                return 0;

            var soma = 0; // Vai guardar a Soma
            var mod = -1; // Vai guardar o Resto da divisão
            var dv = -1;  // Vai guardar o DigitoVerificador
            var pesso = 2; // vai guardar o pesso de multiplicacao

            for (int i = chave.Length - 1; i != -1; i--)
            {
                soma += Convert.ToInt32(chave[i].ToString()) * pesso;

                if (pesso < 9)
                    pesso += 1;
                else
                    pesso = 2;
            }

            mod = soma % 11;

            if (mod == 0 || mod == 1)
                dv = 0;
            else
                dv = 11 - mod;

            return dv;
        }

        public void Assinar(ref string xml, string tag, string numeroCertificado)
        {
            if (!numeroCertificado.TemValor())
                throw new SYSException(Mensagens.Necessario("um certificado digital válido!"));

            var store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

            var certificados = store.Certificates.Find(X509FindType.FindByTimeValid, Conexao.DataHora, true).Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, true).Find(X509FindType.FindBySerialNumber, numeroCertificado, true);

            if (certificados.Count == 0)
                throw new SYSException(Mensagens.Necessario("um certificado digital válido!"));

            var certificado = certificados[0];

            store.Close();

            var documento = new XmlDocument();
            documento.PreserveWhitespace = false;
            documento.LoadXml(xml);

            var tags = documento.GetElementsByTagName(tag);
            if (tags.Count == 0)
                throw new SYSException("A tag de assinatura " + tag + " inexiste!");
            if (tags.Count > 1)
                throw new SYSException("A tag de assinatura " + tag + " não é unica!");

            var documentoAssinado = new SignedXml(documento);
            documentoAssinado.SigningKey = certificado.PrivateKey;

            var reference = new Reference();
            reference.Uri = "#" + tags.Item(0).Attributes.Cast<XmlAttribute>().FirstOrDefault(a => a.Name == "Id").InnerText;
            reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
            reference.AddTransform(new XmlDsigC14NTransform());

            documentoAssinado.AddReference(reference);

            var keyInfo = new KeyInfo();
            keyInfo.AddClause(new KeyInfoX509Data(certificado));

            documentoAssinado.KeyInfo = keyInfo;
            documentoAssinado.ComputeSignature();

            documento.DocumentElement.AppendChild(documento.ImportNode(documentoAssinado.GetXml(), true));

            xml = documento.OuterXml;
        }

        public void Enviar(int id_documento, int id_empresa, Modelo modelo, Ambiente ambiente, Emissao emissao, ImpressaoDANFE impressao, string ds_motivoContingencia = "APLICATIVO EMISSOR EM PROCESSO DE AUTORIZAÇÃO COMO EMISSOR FISCAL")
        {
            try
            {
                var certificado = Conexao.BancoDados.TB_FIS_CERTIFICADODIGITALs.FirstOrDefault(a => a.ID_EMPRESA == id_empresa);

                if (certificado == null)
                    throw new SYSException(Mensagens.Necessario("o certificado digital para a empresa " + id_empresa + "!", "cadastrar"));

                if (modelo == Modelo.NFe || modelo == Modelo.NFCe)
                {
                    #region Declarações

                    var dados = (from a in Conexao.BancoDados.TB_FAT_NOTA_X_DOCUMENTOs
                                 where a.ID_DOCUMENTO == id_documento && a.ID_EMPRESA == a.ID_EMPRESA

                                 let nota = a.TB_FAT_NOTA
                                 let notaItens = nota.TB_FAT_NOTA_ITEMs.Take(990).Select(aa => aa)
                                 let pedido = Conexao.BancoDados.TB_COM_PEDIDO_X_NOTAs.Where(aa => aa.ID_NOTA == nota.ID_NOTA && aa.ID_EMPRESA == a.ID_EMPRESA).Select(aa => aa.TB_COM_PEDIDO).FirstOrDefault()

                                 let ultimoLote = Conexao.BancoDados.TB_FIS_DOCUMENTOELETRONICOs.Where(aa => aa.ID_EMPRESA == a.ID_EMPRESA).Max(aa => aa.ID_LOTE) + 1

                                 let emitente = Conexao.BancoDados.TB_CON_EMPRESAs.Where(aa => aa.ID_EMPRESA == a.ID_EMPRESA).Select(aa => aa.TB_REL_CLIFOR).FirstOrDefault()
                                 let emitenteEndereco = emitente.TB_REL_CLIFOR_X_ENDERECOs.FirstOrDefault().TB_REL_ENDERECO 
                                 let emitenteContato = emitente.TB_REL_CLIFOR_CONTATOs.FirstOrDefault()

                                 let destinatario = Conexao.BancoDados.TB_REL_CLIFORs.FirstOrDefault(aa => aa.ID_CLIFOR == nota.ID_CLIFOR)
                                 let destinatarioEndereco = destinatario.TB_REL_CLIFOR_X_ENDERECOs.FirstOrDefault().TB_REL_ENDERECO
                                 let destinatarioContato = destinatario.TB_REL_CLIFOR_CONTATOs.FirstOrDefault()

                                 let transportadora = Conexao.BancoDados.TB_FRO_TRANSPORTADORAs.FirstOrDefault(aa => aa.ID_TRANSPORTADORA == nota.ID_TRANSPORTADORA)
                                 let transportadoraEndereco = Conexao.BancoDados.TB_REL_CLIFOR_X_ENDERECOs.FirstOrDefault(aa => aa.ID_CLIFOR == transportadora.ID_CLIFOR).TB_REL_ENDERECO
                                 let transportadoraContato = Conexao.BancoDados.TB_REL_CLIFOR_CONTATOs.FirstOrDefault(aa => aa.ID_CLIFOR == transportadora.ID_CLIFOR)
                                 let transportadoraVeiculo = Conexao.BancoDados.TB_FRO_VEICULOs.FirstOrDefault(aa => aa.ID_VEICULO == nota.ID_VEICULO)

                                 let configuracao = a.TB_FAT_NOTA.TB_FIS_CONFIGURACAO

                                 let duplicata = Conexao.BancoDados.TB_FIN_DUPLICATA_X_NOTAs.FirstOrDefault(aa => aa.ID_EMPRESA == nota.ID_EMPRESA && aa.ID_NOTA == nota.ID_NOTA).TB_FIN_DUPLICATA
                                 let duplicataParcelas = Conexao.BancoDados.TB_FIN_PARCELAs.Where(aa => aa.ID_EMPRESA == duplicata.ID_EMPRESA && aa.ID_DUPLICATA == duplicata.ID_DUPLICATA)
                                 let duplicataParcelasLiquidadas = duplicataParcelas.Select(aa => aa.TB_FIN_LIQUIDACAOs)

                                 select new
                                 {
                                     CONFIGURACAO_LOTE = ultimoLote,
                                     CONFIGURACAO_SINCRONO = 1,
                                     CONFIGURACAO_VERSAO = "3.10",
                                     CONFIGURACAO_PROCESSO = 0,

                                     CONFIGURACAO_MOVIMENTO = nota.TP_MOVIMENTO,
                                     CONFIGURACAO_MODELO = configuracao.ID_MODELO,
                                     CONFIGURACAO_SERIE = configuracao.ID_SERIE,
                                     CONFIGURACAO_CFOP = (configuracao.TP_OPERACAO== 0?
                                                         ((emitenteEndereco.ID_UNIDADEFEDERATIVA == destinatarioEndereco.ID_UNIDADEFEDERATIVA && emitenteEndereco.ID_PAIS == destinatarioEndereco.ID_PAIS) ? "5" :
                                                         ((emitenteEndereco.ID_UNIDADEFEDERATIVA != destinatarioEndereco.ID_UNIDADEFEDERATIVA && emitenteEndereco.ID_PAIS == destinatarioEndereco.ID_PAIS) ? "6" : "7")) :
                                                         ((emitenteEndereco.ID_UNIDADEFEDERATIVA == destinatarioEndereco.ID_UNIDADEFEDERATIVA && emitenteEndereco.ID_PAIS == destinatarioEndereco.ID_PAIS) ? "1" :
                                                         ((emitenteEndereco.ID_UNIDADEFEDERATIVA != destinatarioEndereco.ID_UNIDADEFEDERATIVA && emitenteEndereco.ID_PAIS == destinatarioEndereco.ID_PAIS) ? "2" : "3"))) +
                                                         configuracao.ID_CFOP.ToString(),
                                     CONFIGURACAO_MODALIDADEFRETE = configuracao.ID_MODALIDADEFRETE,

                                     CONFIGURACAO_FORMAPAGAMENTO = configuracao.ID_FORMAPAGAMENTO,
                                     CONFIGURACAO_DESTINOOPERACAO = configuracao.ID_DESTINOOPERACAO,
                                     CONFIGURACAO_FINALIDADE = configuracao.ID_FINALIDADE,
                                     CONFIGURACAO_CONSUMIDOROPERACAO = configuracao.ID_CONSUMIDOROPERACAO,
                                     CONFIGURACAO_CONSUMIDORPRESENCA = configuracao.ID_CONSUMIDORPRESENCA,

                                     EMITENTE_CNPJ = emitente.CNPJ,
                                     EMITENTE_CPF = emitente.CPF,
                                     EMITENTE_NM = emitente.NM,
                                     EMITENTE_NMFANTASIA = emitente.NM_FANTASIA,
                                     EMITENTE_IE = emitente.IE,
                                     EMITENTE_IEST = emitente.IE_SUBSTITUTOTRIBUTARIO,
                                     EMITENTE_IM = emitente.IM,
                                     EMITENTE_CNAE = emitente.CNAE,
                                     EMITENTE_REGIMETRIBUTARIO = emitente.ID_REGIMETRIBUTARIO,
                                     EMITENTE_REGIMETRIBUTARIOESPECIAL = emitente.ID_REGIMETRIBUTARIO_ESPECIAL,

                                     EMITENTE_ENDERECO_CEP = emitenteEndereco.CEP,
                                     EMITENTE_ENDERECO_NR = emitenteEndereco.NR,
                                     EMITENTE_ENDERECO_NMRUA = emitenteEndereco.NM_RUA,
                                     EMITENTE_ENDERECO_NMBAIRRO = emitenteEndereco.NM_BAIRRO,
                                     EMITENTE_ENDERECO_COMPLEMENTO = emitenteEndereco.DS_COMPLEMENTO,
                                     EMITENTE_ENDERECO_CIDADE = emitenteEndereco.ID_CIDADE,
                                     EMITENTE_ENDERECO_UF = emitenteEndereco.ID_UNIDADEFEDERATIVA,
                                     EMITENTE_ENDERECO_PAIS = emitenteEndereco.ID_PAIS,

                                     EMITENTE_CONTATO_TELEFONE = emitenteContato.TELEFONE,
                                     EMITENTE_CONTATO_EMAIL = emitenteContato.EMAIL,

                                     DESTINATARIO_CNPJ = destinatario.CNPJ,
                                     DESTINATARIO_CPF = destinatario.CPF,
                                     DESTINATARIO_NM = destinatario.NM,
                                     DESTINATARIO_ESTRANGEIRO = destinatario.ST_ESTRANGEIRO,
                                     DESTINATARIO_RG = destinatario.RG,
                                     DESTINATARIO_IM = destinatario.IM,
                                     DESTINATARIO_IE = destinatario.IE,
                                     DESTINATARIO_IE_INDICADOR = destinatario.IE_INDICADOR,
                                     DESTINATARIO_IS = destinatario.IS,

                                     DESTINATARIO_ENDERECO_CEP = destinatarioEndereco.CEP,
                                     DESTINATARIO_ENDERECO_NR = destinatarioEndereco.NR,
                                     DESTINATARIO_ENDERECO_NMRUA = destinatarioEndereco.NM_RUA,
                                     DESTINATARIO_ENDERECO_NMBAIRRO = destinatarioEndereco.NM_BAIRRO,
                                     DESTINATARIO_ENDERECO_COMPLEMENTO = destinatarioEndereco.DS_COMPLEMENTO,
                                     DESTINATARIO_ENDERECO_CIDADE = destinatarioEndereco.ID_CIDADE,
                                     DESTINATARIO_ENDERECO_UF = destinatarioEndereco.ID_UNIDADEFEDERATIVA,
                                     DESTINATARIO_ENDERECO_PAIS = destinatarioEndereco.ID_PAIS,

                                     DESTINATARIO_CONTATO_TELEFONE = destinatarioContato.TELEFONE,
                                     DESTINATARIO_CONTATO_EMAIL = destinatarioContato.EMAIL,

                                     TRANSPORTADORA_CNPJ = transportadora.TB_REL_CLIFOR.CNPJ,
                                     TRANSPORTADORA_CPF = transportadora.TB_REL_CLIFOR.CPF,
                                     TRANSPORTADORA_NOME = transportadora.TB_REL_CLIFOR.NM,
                                     TRANSPORTADORA_IE = transportadora.TB_REL_CLIFOR.IE,

                                     TRANSPORTADORA_ENDERECO_CEP = transportadoraEndereco.CEP,
                                     TRANSPORTADORA_ENDERECO_NR = transportadoraEndereco.NR,
                                     TRANSPORTADORA_ENDERECO_NMRUA = transportadoraEndereco.NM_RUA,
                                     TRANSPORTADORA_ENDERECO_NMBAIRRO = transportadoraEndereco.NM_BAIRRO,
                                     TRANSPORTADORA_ENDERECO_COMPLEMENTO = transportadoraEndereco.DS_COMPLEMENTO,
                                     TRANSPORTADORA_ENDERECO_CIDADE = transportadoraEndereco.ID_CIDADE,
                                     TRANSPORTADORA_ENDERECO_UF = transportadoraEndereco.ID_UNIDADEFEDERATIVA,
                                     TRANSPORTADORA_ENDERECO_PAIS = transportadoraEndereco.ID_PAIS,

                                     TRANSPORTADORA_CONTATO_TELEFONE = transportadoraContato.TELEFONE,
                                     TRANSPORTADORA_CONTATO_EMAIL = transportadoraContato.EMAIL,

                                     TRANSPORTADORA_VEICULO = transportadoraVeiculo,

                                     NOTA_ID = nota.ID_NOTA,
                                     NOTA_IDREFERENCIA = nota.ID_NOTA_REFERENCIA,
                                     NOTA_EMISSAO = nota.DT_EMISSAO,
                                     NOTA_ITENS = notaItens,

                                     PEDIDO = pedido,

                                     DUPLICATA = duplicata,
                                     DUPLICATA_PARCELAS = duplicataParcelas,
                                     DUPLICATA_PARCELAS_LIQUIDADAS = duplicataParcelasLiquidadas
                                 }).ToList().FirstOrDefault();

                    var dataEntradaSaida = Conexao.DataHora;

                    var notaExistente = Conexao.BancoDados.TB_FAT_NOTAs.Where(a => a.ID_NOTA == dados.NOTA_ID && a.ID_EMPRESA == id_empresa).FirstOrDefault();
                    notaExistente.DT_ENTRADASAIDA = dataEntradaSaida;
                    Conexao.Enviar();

                    var paisesUFsCidades = new QPaisUFCidade();

                    #endregion


                    #region Validações

                    if (!dados.EMITENTE_ENDERECO_UF.TemValor())
                        throw new SYSException(Mensagens.Necessario("o identificador da unidade federativa do endereço do emitente!"));

                    if (!dados.NOTA_EMISSAO.TemValor())
                        throw new SYSException(Mensagens.Necessario("a data de emissão da nota!"));

                    if (!dados.EMITENTE_CNPJ.TemValor())
                        throw new SYSException(Mensagens.Necessario("o CNPJ do emitente!"));

                    if (!dados.CONFIGURACAO_MODELO.TemValor())
                        throw new SYSException(Mensagens.Necessario("o modelo da nota!"));

                    if (!dados.CONFIGURACAO_SERIE.TemValor())
                        throw new SYSException(Mensagens.Necessario("a série da nota!"));

                    if (!dados.NOTA_IDREFERENCIA.TemValor())
                        throw new SYSException(Mensagens.Necessario("o número da nota!"));

                    #endregion

                    #region Cabeçalho

                    var chaveAcesso = dados.EMITENTE_ENDERECO_UF.ToString().PadLeft(2, '0') +
                                      dados.NOTA_EMISSAO.Padrao().ToString("yyMM") +
                                      dados.EMITENTE_CNPJ.PadLeft(14, '0') +
                                      dados.CONFIGURACAO_MODELO.PadLeft(2, '0') +
                                      dados.CONFIGURACAO_SERIE.PadLeft(3, '0') +
                                      dados.NOTA_IDREFERENCIA.Padrao().ToString().Validar(false, 9).PadLeft(9, '0') +
                                      Convert.ToInt32(emissao).ToString().Validar(false, 1) +
                                      dados.NOTA_ID.ToString().Validar(false, 8).PadLeft(8, '0');

                    var digitoVerificador = DigitoVerificador(chaveAcesso).ToString();

                    chaveAcesso += digitoVerificador;

                    // Builder do XML final.
                    var xml = new StringBuilder().AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");

                    //Tipo Pedido de Concessão de Autorização da Nota Fiscal Eletrônica.
                    xml.AppendLine("<enviNFe versao=\"" + dados.CONFIGURACAO_VERSAO + "\"xmlns=\"http://www.portalfiscal.inf.br/nfe\">");

                    // Lote do XML.
                    // Em um lote pode enviar até no máximo 50 NFes.
                    xml.AppendLine("    <idLote>" + dados.CONFIGURACAO_LOTE + "</idLote>");

                    // Indicador de processamento síncrono:
                    // 0 = NÃO;
                    // 1 = SIM = Síncrono.
                    xml.AppendLine("    <indSinc>" + dados.CONFIGURACAO_SINCRONO + "</indSinc>");

                    // LEMBRETE: Quando for utilizar um lote para várias NFes, colocar um foreach aqui! Limite de 50 registros.

                    // Tipo Nota Fiscal Eletrônica.
                    xml.AppendLine("    <NFe xmlns=\"http://www.portalfiscal.inf.br/nfe\">");

                    // Informações da Nota Fiscal eletrônica.
                    xml.AppendLine("        <infNFe Id=\"NFe" + chaveAcesso + "\" versao=\"" + dados.CONFIGURACAO_VERSAO + "\">");

                    #endregion

                    #region Identificação da NF-e

                    // Identificação da NF-e.
                    xml.AppendLine("            <ide>");

                    // Código da UF do emitente do Documento Fiscal.
                    // Utilizar a Tabela do IBGE.
                    xml.AppendLine("                <cUF>" + dados.EMITENTE_ENDERECO_UF + "</cUF>");

                    // Código numérico que compõe a Chave de Acesso.
                    // Número aleatório gerado pelo emitente para cada NF-e.
                    xml.AppendLine("                <cNF>" + dados.NOTA_ID + "</cNF>");

                    // Descrição da Natureza da Operação.
                    // Descrição do CFOP.
                    xml.AppendLine("                <natOp>" + new QCFOP().CFOPs.FirstOrDefault(a => a.ID == dados.CONFIGURACAO_CFOP.Substring(1, 3).ToInt32()).NM.Validar(false, 60) + "</natOp>");

                    //Indicador da forma de pagamento:
                    //0 = Pagamento à vista;
                    //1 = Pagamento à prazo;
                    xml.AppendLine("                <indPag>" + dados.CONFIGURACAO_FORMAPAGAMENTO + "</indPag>");

                    // Código do modelo do Documento Fiscal:
                    // 55 = NF-e;
                    // 65 = NFC-e.
                    xml.AppendLine("                <mod>" + dados.CONFIGURACAO_MODELO + "</mod>");

                    // Série do Documento Fiscal:
                    // Normal = 0-889;
                    // Avulsa Fisco = 890-899;
                    // SCAN = 900-999.
                    xml.AppendLine("                <serie>" + dados.CONFIGURACAO_SERIE + "</serie>");

                    // Número do Documento Fiscal.
                    xml.AppendLine("                <nNF>" + dados.NOTA_IDREFERENCIA + "</nNF>");

                    // Data e Hora de emissão do Documento Fiscal.
                    // Formato: AAAA-MM-DDThh:mm:ssTZD.
                    // Exemplo: 2012-09-01T13:00:00-03:00.
                    xml.AppendLine("                <dhEmi>" + dados.NOTA_EMISSAO.Padrao().ToUniversalTime().ToString("yyyy-MM-ddTHH\\:mm\\:ssTZD") + " </dhEmi>");

                    // Data e Hora da saída ou de entrada da mercadoria / produto.
                    // Formato: AAAA-MM-DDTHH:mm:ssTZD.
                    // Exemplo: 2012-09-01T13:00:00-03:00.
                    xml.AppendLine("                <dhSaiEnt>" + dataEntradaSaida.ToUniversalTime().ToString("yyyy-MM-ddTHH\\:mm\\:ssTZD") + "</dhSaiEnt>");

                    // Tipo do Documento Fiscal:
                    // 0 = Entrada;
                    // 1 = Saída.
                    xml.AppendLine("                <tpNF>" + dados.CONFIGURACAO_MOVIMENTO + "</tpNF>");

                    // Identificador de Local de destino da operação:
                    // 1 = Interna;
                    // 2 = Interestadual;
                    // 3 = Exterior.
                    xml.AppendLine("                <idDest>" + dados.CONFIGURACAO_DESTINOOPERACAO + "</idDest>");

                    // Código do Município de Ocorrência do Fato Gerador.
                    // Utilizar a tabela do IBGE.
                    xml.AppendLine("                <cMunFG>" + dados.EMITENTE_ENDERECO_UF + "</cMunFG>");

                    // Formato de impressão do DANFE:
                    // 0 = Sem DANFE;
                    // 1 = DANFe Retrato;
                    // 2 = DANFe Paisagem;
                    // 3 = DANFe Simplificado;
                    // 4 = DANFe NFC-e;
                    // 5 = DANFe NFC-e em mensagem eletrônica.
                    xml.AppendLine("                <tpImp>" + impressao + "</tpImp>");

                    // Forma de emissão da NF-e:
                    // 1 = Normal;
                    // 2 = Contingência FS;
                    // 3 = Contingência SCAN;
                    // 4 = Contingência DPEC;
                    // 5 = Contingência FSDA;
                    // 6 = Contingência SVC-AN;
                    // 7 = Contingência SVC-RS;
                    // 9 = Contingência off-line NFC-e.
                    xml.AppendLine("                <tpEmis>" + emissao + "</tpEmis>");

                    // Digito Verificador da Chave de Acesso da NF-e.
                    xml.AppendLine("                <cDV>" + digitoVerificador + "</cDV>");

                    // Identificação do Ambiente:
                    // 1 = Produção;
                    // 2 = Homologação.
                    var tpAmb = ambiente == Ambiente.Producao ? "1" : "2";
                    xml.AppendLine("                <tpAmb>" + tpAmb + "</tpAmb>");

                    // Finalidade da emissão da NF-e:
                    // 1 = NFe normal;
                    // 2 = NFe complementar;
                    // 3 = NFe de ajuste;
                    // 4 = Devolução / Retorno.
                    xml.AppendLine("                <finNFe>" + dados.CONFIGURACAO_FINALIDADE.Validar(true, 1) + "</finNFe>");

                    // Indica operação com consumidor final:
                    // 0 = Não;
                    // 1 = Consumidor Final.
                    xml.AppendLine("                <indFinal>" + dados.CONFIGURACAO_CONSUMIDOROPERACAO.Validar(true, 1) + "</indFinal>");

                    // Indicador de presença do comprador no estabelecimento comercial no momento da operação:
                    // 0 = Não se aplica (Exemplo: Nota Fiscal complementar ou de ajuste); 
                    // 1 = Operação presencial; 
                    // 2 = Não presencial, internet;
                    // 3 = Não presencial, teleatendimento;
                    // 4 = NFC-e entrega em domicílio;
                    // 9 - Não presencial, outros.
                    xml.AppendLine("                <indPres>" + dados.CONFIGURACAO_CONSUMIDORPRESENCA.Validar(true, 1) + "</indPres>");

                    // Processo de emissão utilizado com a seguinte codificação:
                    // 0 = Emissão de NF-e com aplicativo do contribuinte;
                    // 1 = Emissão de NF-e avulsa pelo Fisco;
                    // 2 = Emissão de NF-e avulsa, pelo contribuinte com seu certificado digital, através do site do Fisco;
                    // 3 = Emissão de NF-e pelo contribuinte com aplicativo fornecido pelo Fisco.
                    xml.AppendLine("                <procEmi>" + dados.CONFIGURACAO_PROCESSO.ToString().Validar(true, 1) + "</procEmi>");

                    // Versão do aplicativo utilizado no processo de emissão.
                    xml.AppendLine("                <verProc>" + Parametros.Versao + "</verProc>");

                    if (emissao != Emissao.Normal)
                    {
                        // Informar a data e hora de entrada em contingência contingência. 
                        // Formato: AAAA-MM-DDTHH:mm:ssTZD.
                        // Exemplo: 2012-09-01T13:00:00-03:00.
                        xml.AppendLine("                <dhCont>" + dataEntradaSaida.ToUniversalTime().ToString("yyyy-MM-ddTHH\\:mm\\:ssTZD") + "</dhCont>");

                        // Informar a Justificativa da entrada.
                        // Tamanho mínimo de 15 caracteres e máximo de 256.
                        xml.AppendLine("                <xJust>" + ds_motivoContingencia.Validar(true, 256) + "</xJust>");
                    }

                    // LEMBRETE: Quando for utilizar notas fiscais referenciadas, colocar um foreach aqui! Limite de até 500 documentos referenciados!
                    //if (false)
                    //{
                    //    xml.AppendLine("                    <NFref>");

                    //    #region NF-e

                    //    // Chave de acesso das NF-e referenciadas.
                    //    // Chave de acesso compostas por Código da UF (tabela do IBGE) + AAMM da emissão + CNPJ do Emitente + modelo, série e número da NF-e Referenciada + Código Numérico + DV.
                    //    xml.AppendLine("                        <refNFe>" + "</refNFe>");

                    //    #endregion

                    //    #region Nota Fiscal (modelo 1/1A)

                    //    // Dados da NF modelo 1/1A referenciada.
                    //    xml.AppendLine("                        <refNF>");

                    //    // Código da UF do emitente do Documento Fiscal.
                    //    // Utilizar a Tabela do IBGE.
                    //    xml.AppendLine("                            <cUF>" + "</cUF>");

                    //    // AAMM da emissão.
                    //    xml.AppendLine("                            <AAMM>" + "</AAMM>");

                    //    // CNPJ do emitente do documento fiscal referenciado.
                    //    xml.AppendLine("                            <CNPJ>" + "</CNPJ>");

                    //    // Código do modelo do Documento Fiscal.
                    //    // Utilizar 01 para NF modelo 1/1A.
                    //    xml.AppendLine("                            <mod>" + "</mod>");

                    //    // Série do Documento Fiscal, informar zero se inexistente.
                    //    xml.AppendLine("                            <serie>" + "</serie>");

                    //    // Número do Documento Fiscal.
                    //    xml.AppendLine("                            <nNF>" + "</nNF>");

                    //    xml.AppendLine("                        </refNF>");

                    //    #endregion

                    //    #region Nota Fiscal do produtor

                    //    // Grupo com as informações NF de produtor referenciada.
                    //    xml.AppendLine("                        <refNFP>");

                    //    // Código da UF do emitente do Documento Fiscal.
                    //    // Utilizar a Tabela do IBGE (Anexo IV - Tabela de UF, Município e País).
                    //    xml.AppendLine("                            <cUF>" + "</cUF>");

                    //    // AAMM da emissão da NF de produtor.
                    //    xml.AppendLine("                            <AAMM>" + "</AAMM>");

                    //    // CNPJ do emitente da NF de produtor.
                    //    xml.AppendLine("                            <CNPJ>" + "</CNPJ>");

                    //    // CPF do emitente da NF de produtor.
                    //    xml.AppendLine("                            <CPF>" + "</CPF>");

                    //    // IE do emitente da NF de Produtor.
                    //    xml.AppendLine("                            <IE>" + "</IE>");

                    //    // Código do modelo do Documento Fiscal:
                    //    // 04 = NF de produtor;
                    //    // 01 = NF Avulsa.
                    //    xml.AppendLine("                            <mod>" + "</mod>");

                    //    // Série do Documento Fiscal, informar zero se inexistente.
                    //    xml.AppendLine("                            <serie>" + "</serie>");

                    //    // Número do Documento Fiscal:
                    //    // 1 – 999999999
                    //    xml.AppendLine("                            <nNF>" + "</nNF>");

                    //    xml.AppendLine("                        </refNFP>");

                    //    #endregion

                    //    #region CT-e

                    //    //Utilizar esta TAG para referenciar um CT-e emitido anteriormente, vinculada a NF-e atual
                    //    xml.AppendLine("                        <refCTe>" + "</refCTe>");

                    //    #endregion

                    //    #region Cupom Fiscal

                    //    // Grupo do Cupom Fiscal vinculado à NF-e.
                    //    xml.AppendLine("                        <refECF>");

                    //    // Código do modelo do Documento Fiscal:
                    //    // "2B", quando se tratar de Cupom Fiscal emitido por máquina registradora (não ECF);
                    //    // "2C", quando se tratar de Cupom Fiscal PDV;
                    //    // "2D", quando se tratar de Cupom Fiscal (emitido por ECF)
                    //    xml.AppendLine("                            <mod>" + "</mod>");

                    //    // Informar o número de ordem seqüencial do ECF que emitiu o Cupom Fiscal vinculado à NF-e.
                    //    xml.AppendLine("                            <nECF>" + "</nECF>");

                    //    // Informar o Número do Contador de Ordem de Operação - COO vinculado à NF-e.
                    //    xml.AppendLine("                            <nCOO>" + "</nCOO>");

                    //    xml.AppendLine("                        </refECF>");

                    //    #endregion

                    //    xml.AppendLine("                    </NFref>");
                    //}


                    xml.AppendLine("            </ide>");

                    #endregion

                    #region Emitente

                    // Identificação da NF-e.
                    xml.AppendLine("            <emit>");

                    // Número do CNPJ do emitente.
                    xml.AppendLine("                <CNPJ>" + dados.EMITENTE_CNPJ + "</CNPJ>");

                    // Número do CPF do emitente.
                    //xml.AppendLine("                <CPF>" + cpfEmitente + "</CPF>");

                    // Razão Social ou Nome do emitente
                    // Tamanho mínimo: 2
                    // Tamanho máximo: 60
                    xml.AppendLine("                <xNome>" + dados.EMITENTE_NM.Validar(true, 60) + "</xNome>");

                    // Nome fantasia
                    // Tamanho mínimo: 2
                    // Tamanho máximo: 60
                    xml.AppendLine("                <xFant>" + dados.EMITENTE_NMFANTASIA.Validar(true, 60) + "</xFant>");

                    #region Endereço do emitente

                    // Endereço do emitente
                    xml.AppendLine("                <enderEmit>");

                    // Logradouro
                    // Minimo 2
                    // Maximo 60
                    xml.AppendLine("                    <xLg>" + dados.EMITENTE_ENDERECO_NMRUA.Validar(true, 60) + "</xLg>");

                    // Número
                    // Minimo 1
                    // Maximo 60
                    xml.AppendLine("                    <nro>" + dados.EMITENTE_ENDERECO_NR.Validar(true, 60) + "</nro>");

                    // Complemento
                    // Minimo 1
                    // Maximo 60
                    xml.AppendLine("                    <xCpl>" + dados.EMITENTE_ENDERECO_COMPLEMENTO.Validar(true, 60) + "</xCpl>");

                    // Bairro
                    // Minimo 2
                    // Maximo 60
                    xml.AppendLine("                    <xBairro>" + dados.EMITENTE_ENDERECO_NMBAIRRO.Validar(true, 60) + "</xBairro>");

                    // Código do município
                    xml.AppendLine("                    <cMun>" + dados.EMITENTE_ENDERECO_CIDADE + "</cMun>");

                    // Nome do município
                    // Minimo 2
                    // Maximo 60
                    xml.AppendLine("                    <xMun>" + paisesUFsCidades.Cidades.FirstOrDefault(a => a.ID_CIDADE == dados.EMITENTE_ENDERECO_CIDADE).NM.Validar(true, 60) + "</xMun>");

                    // Sigla da UF
                    xml.AppendLine("                    <UF>" + paisesUFsCidades.UFs.FirstOrDefault(a => a.ID_UF == dados.EMITENTE_ENDERECO_UF).SIGLA + "</UF>");

                    // CEP - NT 2011/004
                    // [0-9]{8}
                    xml.AppendLine("                    <CEP>" + dados.EMITENTE_ENDERECO_CEP.Validar(true, 8) + "</CEP>");

                    // Código do país
                    xml.AppendLine("                    <cPais>" + dados.EMITENTE_ENDERECO_PAIS + "</cPais>");

                    // Nome do país
                    xml.AppendLine("                    <xPais>" + paisesUFsCidades.Paises.FirstOrDefault(a => a.ID_PAIS == dados.EMITENTE_ENDERECO_PAIS).NM + "</xPais>");

                    // Telefone, preencher com Código DDD + número do telefone , nas operações com exterior é permtido informar o código do país + código da localidade + número do telefone
                    // [0-9]{6,14}
                    xml.AppendLine("                    <fone>" + dados.EMITENTE_CONTATO_TELEFONE.Padrao().ToString().Validar(true, 14) + "</fone>");


                    xml.AppendLine("                </enderEmit>");

                    #endregion

                    // Inscrição Estadual do Emitente
                    // [0-9]{2,14}|ISENTO
                    xml.AppendLine("                <IE>" + dados.EMITENTE_IE.Validar(true, 14) + "</IE>");

                    // Inscricao Estadual do Substituto Tributário
                    // [0-9]{2,14}
                    xml.AppendLine("                <IEST>" + dados.EMITENTE_IEST.Validar(true, 14) + "</IEST>");

                    // Inscrição Municipal
                    xml.AppendLine("                <IM>" + dados.EMITENTE_IM.Validar(true, 15) + "</IM>");

                    // CNAE Fiscal
                    //[0-9]{7}
                    xml.AppendLine("                <IM>" + dados.EMITENTE_CNAE.Validar(true, 7) + "</IM>");

                    // Código de Regime Tributário.
                    // Este campo será obrigatoriamente preenchido com:
                    // 1 = Simples Nacional;
                    // 2 = Simples Nacional – excesso de sublimite de receita bruta;
                    // 3 = Regime Normal.
                    xml.AppendLine("                <CRT>" + dados.EMITENTE_REGIMETRIBUTARIO.Validar(true, 1) + "</CRT>");

                    xml.AppendLine("            </emit>");

                    #endregion

                    #region Emissão de avulsa, informar os dados do Fisco emitente

                    // Não será emitido avulsa

                    // Emissão de avulsa, informar os dados do Fisco emitente
                    //xml.AppendLine("            <avulsa>");

                    // CNPJ do Órgão emissor
                    //xml.AppendLine("                <CNPJ>" + cnpjOrgaoEmissor + "</CNPJ>");

                    // Órgão emitente
                    // Tamanho mínimo: 1
                    // Tamanho máximo: 60
                    //xml.AppendLine("                <xOrgao>" + orgaoEmissor + "</xOrgao>");

                    // Matrícula do agente
                    // Tamanho mínimo: 1
                    // Tamanho máximo: 60
                    //xml.AppendLine("                <matr>" + matriculaAgente + "</matr>");

                    // Nome do agente
                    // Tamanho mínimo: 1
                    // Tamanho máximo: 60
                    //xml.AppendLine("                <xAgente>" + nomeAgente + "</xAgente>");

                    // Telefone
                    //xml.AppendLine("                <fone>" + telefone + "</fone>");

                    // Sigla da Unidade da Federação
                    //xml.AppendLine("                <UF>" + uf + "</UF>");

                    // Número do Documento de Arrecadação de Receita
                    // Tamanho mínimo: 1
                    // Tamanho máximo: 60
                    //xml.AppendLine("                <nDAR>" + dar + "</nDAR>");

                    // Data de emissão do DAR (AAAA-MM-DD)
                    //xml.AppendLine("                <dEmi>" + darData + "</dEmi>");

                    // Valor Total constante no DAR
                    //xml.AppendLine("                <darValor>" + darValor + "</darValor>");

                    // Repartição Fiscal emitente
                    // Tamanho mínimo: 1
                    // Tamanho máximo: 60
                    //xml.AppendLine("                <repEmi>" + reparticaoFiscal + "</repEmi>");

                    // Data de pagamento do DAR (AAAA-MM-DD)
                    //xml.AppendLine("                <dPag>" + darDataPagamento + "</dPag>");

                    //xml.AppendLine("            </avulsa>");

                    #endregion

                    #region Destinatário

                    // Identificação do Destinatário
                    xml.AppendLine("            <dest>");

                    if (dados.DESTINATARIO_CNPJ.TemValor())
                        // Número do CNPJ
                        xml.AppendLine("                <CNPJ>" + dados.DESTINATARIO_CNPJ + "</CNPJ>");
                    else
                        // Número do CPF
                        xml.AppendLine("                <CPF>" + dados.DESTINATARIO_CPF + "</CPF>");

                    // Identificador do destinatário, em caso de comprador estrangeiro
                    if (dados.DESTINATARIO_ESTRANGEIRO.Padrao())
                        xml.AppendLine("                <idEstrangeiro>" + dados.DESTINATARIO_RG + "</idEstrangeiro>");

                    // Razão Social ou nome do destinatário
                    // Tamanho mínimo: 2
                    // Tamanho máximo: 60
                    xml.AppendLine("                <xNome>" + dados.DESTINATARIO_NM.Validar(true, 60) + "</xNome>");

                    #region Dados do endereço

                    // Dados do endereço
                    xml.AppendLine("                <enderDest>");

                    // Logradouro
                    // Minimo 2
                    // Maximo 60
                    xml.AppendLine("                    <xLg>" + dados.DESTINATARIO_ENDERECO_NMRUA.Validar(true, 60) + "</xLg>");

                    // Número
                    // Minimo 1
                    // Maximo 60
                    xml.AppendLine("                    <nro>" + dados.DESTINATARIO_ENDERECO_NR.Validar(true, 60) + "</nro>");

                    // Complemento
                    // Minimo 1
                    // Maximo 60
                    xml.AppendLine("                    <xCpl>" + dados.DESTINATARIO_ENDERECO_COMPLEMENTO.Validar(true, 60) + "</xCpl>");

                    // Bairro
                    // Minimo 2
                    // Maximo 60
                    xml.AppendLine("                    <xBairro>" + dados.DESTINATARIO_ENDERECO_NMBAIRRO.Validar(true, 60) + "</xBairro>");

                    // Código do município
                    xml.AppendLine("                    <cMun>" + dados.DESTINATARIO_ENDERECO_CIDADE + "</cMun>");

                    // Nome do município
                    // Minimo 2
                    // Maximo 60
                    xml.AppendLine("                    <xMun>" + paisesUFsCidades.Cidades.FirstOrDefault(a => a.ID_CIDADE == dados.DESTINATARIO_ENDERECO_CIDADE).NM.Validar(true, 60) + "</xMun>");

                    // Sigla da UF
                    xml.AppendLine("                    <UF>" + paisesUFsCidades.UFs.FirstOrDefault(a => a.ID_UF == dados.DESTINATARIO_ENDERECO_UF).SIGLA + "</UF>");

                    // CEP - NT 2011/004
                    // [0-9]{8}
                    xml.AppendLine("                    <CEP>" + dados.DESTINATARIO_ENDERECO_CEP.Validar(true, 8) + "</CEP>");

                    // Código do país
                    xml.AppendLine("                    <cPais>" + dados.DESTINATARIO_ENDERECO_PAIS + "</cPais>");

                    // Nome do país
                    xml.AppendLine("                    <xPais>" + paisesUFsCidades.Paises.FirstOrDefault(a => a.ID_PAIS == dados.DESTINATARIO_ENDERECO_PAIS).NM + "</xPais>");

                    // Telefone, preencher com Código DDD + número do telefone , nas operações com exterior é permtido informar o código do país + código da localidade + número do telefone
                    // [0-9]{6,14}
                    xml.AppendLine("                    <fone>" + dados.DESTINATARIO_CONTATO_TELEFONE.Padrao().ToString().Validar(true, 14) + "</fone>");

                    xml.AppendLine("                </enderDest>");

                    #endregion

                    // Indicador da IE do destinatário:
                    // 1 = Contribuinte ICMS pagamento à vista;
                    // 2 = Contribuinte isento de inscrição;
                    // 9 = Não Contribuinte.
                    xml.AppendLine("                <indIEDest>" + dados.DESTINATARIO_IE_INDICADOR.Validar(true, 1) + "</indIEDest>");

                    // Inscrição Estadual (obrigatório nas operações com contribuintes do ICMS).
                    // [0-9]{2,14}
                    xml.AppendLine("                <IE>" + dados.DESTINATARIO_IE.Validar(true, 14) + "</IE>");

                    // Inscrição na SUFRAMA (Obrigatório nas operações com as áreas com benefícios de incentivos fiscais sob controle da SUFRAMA)
                    // PL_005d - 11/08/09 - alterado para aceitar 8 ou 9 dígitos
                    // [0-9]{8,9}
                    xml.AppendLine("                <ISUF>" + dados.DESTINATARIO_IS.Validar(true, 9) + "</ISUF>");

                    // Inscrição Municipal do tomador do serviço
                    xml.AppendLine("                <IM>" + dados.DESTINATARIO_IM.Validar(true, 15) + "</IM>");

                    // Informar o e-mail do destinatário. O campo pode ser utilizado para informar o e-mail de recepção da NF-e indicada pelo destinatário
                    xml.AppendLine("                <email>" + dados.DESTINATARIO_CONTATO_EMAIL + "</email>");

                    xml.AppendLine("            </dest>");

                    #endregion

                    #region Identificação do Local de Retirada (informar apenas quando for diferente do endereço do remetente)

                    // Identificação do Local de Retirada (informar apenas quando for diferente do endereço do remetente)
                    if (dados.CONFIGURACAO_CONSUMIDORPRESENCA.ToInt32() == 1)
                    {
                        xml.AppendLine("            <retirada>");

                        if (dados.EMITENTE_CNPJ.TemValor())
                            // CNPJ
                            xml.AppendLine("                <CNPJ>" + dados.EMITENTE_CNPJ + "</CNPJ>");
                        else
                            // CPF
                            xml.AppendLine("                <CPF>" + dados.EMITENTE_CPF + "</CPF>");

                        // Logradouro
                        // Minimo 2
                        // Maximo 60
                        xml.AppendLine("                <xLgr>" + dados.EMITENTE_ENDERECO_NMRUA.Validar(true, 60) + "</xLgr>");

                        // Número
                        // Minimo 1
                        // Maximo 60
                        xml.AppendLine("                <nro>" + dados.EMITENTE_ENDERECO_NR.Validar(true, 60) + "</nro>");

                        // Complemento
                        // Minimo 1
                        // Maximo 60
                        xml.AppendLine("                <xCpl>" + dados.EMITENTE_ENDERECO_COMPLEMENTO.Validar(true, 60) + "</xCpl>");

                        // Bairro
                        // Minimo 2
                        // Maximo 60
                        xml.AppendLine("                <xBairro>" + dados.EMITENTE_ENDERECO_NMBAIRRO.Validar(true, 60) + "</xBairro>");

                        // Código do município (utilizar a tabela do IBGE)
                        xml.AppendLine("                <cMun>" + dados.EMITENTE_ENDERECO_CIDADE + "</cMun>");

                        // Nome do município
                        // Minimo 2
                        // Maximo 60
                        xml.AppendLine("                <xMun>" + paisesUFsCidades.Cidades.FirstOrDefault(a => a.ID_CIDADE == dados.EMITENTE_ENDERECO_CIDADE).NM.Validar(true, 60) + "</xMun>");

                        // Sigla da UF
                        xml.AppendLine("                <UF>" + paisesUFsCidades.UFs.FirstOrDefault(a => a.ID_UF == dados.EMITENTE_ENDERECO_UF).SIGLA + "</UF>");

                        xml.AppendLine("            </retirada>");
                    }

                    #endregion

                    #region Identificação do Local de Entrega (informar apenas quando for diferente do endereço do destinatário)

                    if (dados.CONFIGURACAO_CONSUMIDORPRESENCA.ToInt32() != 0 && dados.CONFIGURACAO_CONSUMIDORPRESENCA.ToInt32() != 1)
                    {
                        // Identificação do Local de Entrega (informar apenas quando for diferente do endereço do destinatário)
                        xml.AppendLine("            <entrega>");

                        if (dados.DESTINATARIO_CNPJ.TemValor())
                            // CNPJ
                            xml.AppendLine("                <CNPJ>" + dados.DESTINATARIO_CNPJ + "</CNPJ>");
                        else
                            // CPF
                            xml.AppendLine("                <CPF>" + dados.DESTINATARIO_CPF + "</CPF>");

                        // Logradouro
                        // Minimo 2
                        // Maximo 60
                        xml.AppendLine("                <xLgr>" + dados.DESTINATARIO_ENDERECO_NMRUA.Validar(true, 60) + "</xLgr>");

                        // Número
                        // Minimo 1
                        // Maximo 60
                        xml.AppendLine("                <nro>" + dados.DESTINATARIO_ENDERECO_NR.Validar(true, 60) + "</nro>");

                        // Complemento
                        // Minimo 1
                        // Maximo 60
                        xml.AppendLine("                <xCpl>" + dados.DESTINATARIO_ENDERECO_COMPLEMENTO.Validar(true, 60) + "</xCpl>");

                        // Bairro
                        // Minimo 2
                        // Maximo 60
                        xml.AppendLine("                <xBairro>" + dados.DESTINATARIO_ENDERECO_NMBAIRRO.Validar(true, 60) + "</xBairro>");

                        // Código do município (utilizar a tabela do IBGE)
                        xml.AppendLine("                <cMun>" + dados.DESTINATARIO_ENDERECO_CIDADE + "</cMun>");

                        // Nome do município
                        // Minimo 2
                        // Maximo 60
                        xml.AppendLine("                <xMun>" + paisesUFsCidades.Cidades.FirstOrDefault(a => a.ID_CIDADE == dados.DESTINATARIO_ENDERECO_CIDADE).NM.Validar(true, 60) + "</xMun>");

                        // Sigla da UF
                        xml.AppendLine("                <UF>" + paisesUFsCidades.UFs.FirstOrDefault(a => a.ID_UF == dados.DESTINATARIO_ENDERECO_UF).SIGLA + "</UF>");

                        xml.AppendLine("            </entrega>");
                    }

                    #endregion

                    #region Pessoas autorizadas para o download do XML da NF-e

                    // Utilizar Foreach! Maximo 10!

                    #region Identificação do Emissor

                    // Identificação do Emissor
                    xml.AppendLine("            <autXML>");

                    // CNPJ Autorizado
                    if (dados.EMITENTE_CNPJ.TemValor())
                        xml.AppendLine("                <CNPJ>" + dados.EMITENTE_CNPJ + "</CNPJ>");
                    else
                        // CPF Autorizado
                        xml.AppendLine("                <CPF>" + dados.EMITENTE_CPF + "</CPF>");

                    xml.AppendLine("            </autXML>");

                    #endregion

                    #region Identificação do Destinatário

                    // Identificação do Destinatário
                    xml.AppendLine("            <autXML>");

                    // CNPJ Autorizado
                    if (dados.DESTINATARIO_CNPJ.TemValor())
                        xml.AppendLine("                <CNPJ>" + dados.DESTINATARIO_CNPJ + "</CNPJ>");
                    else
                        // CPF Autorizado
                        xml.AppendLine("                <CPF>" + dados.DESTINATARIO_CPF + "</CPF>");

                    xml.AppendLine("            </autXML>");

                    #endregion

                    #endregion

                    #region Dados dos detalhes da NF-e

                    var bcICMS_Total = 0m;
                    var vlICMS_Total = 0m;
                    var vlICMSDesonerado_Total = 0m;
                    var vlICMSRetido_Total = 0m;
                    var vlICMSSTRetido_Total = 0m;
                    var pcICMSRetido_Total = 0m;

                    var bcICMSRetido_Total = 0m;

                    var bcICMSST_Total = 0m;
                    var vlICMSST_Total = 0m;
                    var vlItem_Total = 0m;
                    var vlFrete_Total = 0m;
                    var vlSeguro_Total = 0m;
                    var vlDesconto_Total = 0m;
                    var vlII_Total = 0m;
                    var vlIPI_Total = 0m;
                    var vlPIS_Total = 0m;
                    var vlCOFINS_Total = 0m;
                    var vlAcrescimo_Total = 0m;
                    var vlSubtotal_Total = 0m;

                    var vlEstimadoImpostos = 0m;

                    var bcISS_Total = 0m;
                    var vlISS_Total = 0m;
                    var vlPISSS_Total = 0m;
                    var vlCOFINSSS_Total = 0m;
                    var dtPrestacaoServico = (DateTime?)null;
                    var vlDeducaoReducaoBC_Total = 0m;
                    var vlOutrasRetencoes_Total = 0m;
                    var vlDescontoIncondicionado_Total = 0m;
                    var vlDescontoCondicionado_Total = 0m;
                    var vlISSRetencao_Total = 0m;

                    var vlPISRetido_Total = 0m;
                    var vlCOFINSRetido_Total = 0m;
                    var vlCSLLRetido_Total = 0m;
                    var bcIRRFRetido_Total = 0m;
                    var vlIRRFRedito_Total = 0m;
                    var bcPrevidenciaSocialRetido_Total = 0m;
                    var vlPrevidenciaSocialRetido_Total = 0m;

                    foreach (var item in dados.NOTA_ITENS)
                    {
                        // Dados dos detalhes da NF-e
                        xml.AppendLine("            <det nItem=\"" + item.ID_ITEM + "\">");

                        #region Dados dos detalhes da NF-e

                        // Dados dos detalhes da NF-e
                        xml.AppendLine("                <prod>");

                        // Código do produto ou serviço. 
                        // Preencher com CFOP caso se trate de itens não relacionados com mercadorias/produto e que o contribuinte não possua codificação própria. Formato ”CFOP9999”.
                        xml.AppendLine("                    <cProd>" + item.ID_PRODUTO + "</cProd>");

                        // GTIN (Global Trade Item Number) do produto, antigo código EAN ou código de barras.
                        xml.AppendLine("                    <cEAN>" + item.TB_EST_PRODUTO.TB_EST_PRODUTO_BARRAs.OrderByDescending(a => a.ID_BARRA).FirstOrDefault().ID_BARRA_REFERENCIA + "</cEAN>");

                        // Descrição do produto ou serviço
                        // Mínimo 1;
                        // Máximo 120.
                        xml.AppendLine("                    <xProd>" + item.TB_EST_PRODUTO.NM.Validar(true, 120) + "</xProd>");

                        // Código NCM (8 posições), será permitida a informação do gênero (posição do capítulo do NCM) quando a operação não for de comércio exterior (importação/exportação) ou o produto não seja tributado pelo IPI. Em caso de item de serviço ou item que não tenham produto (Ex. transferência de crédito, crédito do ativo imobilizado, etc.), informar o código 00 (zeros) (v2.0)
                        xml.AppendLine("                    <NCM>" + item.TB_EST_PRODUTO.ID_NCM + "</NCM>");

                        // Nomenclatura de Valor aduaneio e Estatístico
                        //xml.AppendLine("                    <NVE>" + "</NVE>");

                        // Código EX TIPI (3 posições)
                        //xml.AppendLine("                    <EXTIPI>" + "</EXTIPI>");

                        // Código Fiscal de Operações e Prestações
                        xml.AppendLine("                    <CFOP>" + dados.CONFIGURACAO_CFOP + "</CFOP>");

                        // Unidade comercial
                        // Mínimo 1;
                        // Máximo 6.
                        xml.AppendLine("                    <uCom>" + new QUnidade().Unidades.FirstOrDefault(a => a.ID == item.TB_EST_PRODUTO.ID_UNIDADE).NM.Validar(true, 6) + "</uCom>");

                        // Quantidade Comercial  do produto, alterado para aceitar de 0 a 4 casas decimais e 11 inteiros.
                        xml.AppendLine("                    <qCom>" + item.QT.Padrao().ENUS(4) + "</qCom>");

                        // Valor unitário de comercialização  - alterado para aceitar 0 a 10 casas decimais e 11 inteiros 
                        xml.AppendLine("                    <vUnCom>" + item.VL_UNITARIO.Padrao().ENUS(10) + "</vUnCom>");

                        // Valor bruto do produto ou serviço.
                        var vProd = item.VL_SUBTOTAL.Padrao();
                        vlItem_Total += vProd;
                        xml.AppendLine("                    <vProd>" + vProd.ENUS(10) + "</vProd>");

                        // GTIN (Global Trade Item Number) da unidade tributável, antigo código EAN ou código de barras
                        xml.AppendLine("                    <cEANTrib>" + item.TB_EST_PRODUTO.TB_EST_PRODUTO_BARRAs.OrderByDescending(a => a.ID_BARRA).FirstOrDefault().ID_BARRA_REFERENCIA + "</cEANTrib>");

                        // Unidade Tributável
                        // Mínimo 1;
                        // Máximo 6.
                        xml.AppendLine("                    <uTrib>" + new QUnidade().Unidades.FirstOrDefault(a => a.ID == item.TB_EST_PRODUTO.ID_UNIDADE).NM.Validar(true, 6) + "</uTrib>");

                        // Quantidade Tributável - alterado para aceitar de 0 a 4 casas decimais e 11 inteiros
                        xml.AppendLine("                    <qTrib>" + item.QT.Padrao().ENUS(4) + "</uTrib>");

                        // Valor unitário de tributação - - alterado para aceitar 0 a 10 casas decimais e 11 inteiros 
                        xml.AppendLine("                    <vUnTrib>" + item.VL_UNITARIO.Padrao().ENUS(10) + "</vUnTrib>");

                        // Valor Total do Frete
                        var vFrete = item.VL_FRETE.Padrao();
                        vlFrete_Total += vFrete;
                        xml.AppendLine("                    <vFrete>" + vFrete.ENUS(5) + "</vFrete>");

                        // Valor Total do Seguro
                        var vSeg = item.VL_SEGURO.Padrao();
                        vlSeguro_Total += vSeg;
                        xml.AppendLine("                    <vSeg>" + vSeg.ENUS(5) + "</vSeg>");

                        // Valor do Desconto
                        var vDesc = item.VL_DESCONTO.Padrao();
                        vlDesconto_Total += vDesc;
                        xml.AppendLine("                    <vDesc>" + vDesc.ENUS(5) + "</vDesc>");

                        // Outras despesas acessórias
                        var vOutro = item.VL_ACRESCIMO.Padrao();
                        vlAcrescimo_Total += vOutro;
                        xml.AppendLine("                    <vOutro>" + vOutro.ENUS(5) + "</vOutro>");

                        vlSubtotal_Total += vProd + vFrete + vSeg - vDesc + vOutro;

                        if (item.TB_EST_PRODUTO.ST_SERVICO.Padrao())
                            dtPrestacaoServico = dados.NOTA_EMISSAO.Padrao();

                        // Este campo deverá ser preenchido com:
                        // 0 = O valor do item(vProd) não compõe o valor total da NF-e(vProd)
                        // 1 = O valor do item(vProd) compõe o valor total da NF - e(vProd)
                        xml.AppendLine("                    <indTot>" + (item.ST_COMPOE.Padrao() ? 1 : 0) + "</indTot>");

                        #region Declaração de Importação

                        // Foreach de até 100 DIs

                        // Declaração de Importação
                        //xml.AppendLine("                    <DI>");

                        // Numero do Documento de Importação DI/DSI/DA/DRI-E (DI/DSI/DA/DRI-E) (NT2011/004)
                        //xml.AppendLine("                        <nDI>" + "</nDI>");

                        // Data de registro da DI/DSI/DA (AAAA-MM-DD)
                        //xml.AppendLine("                        <dDI>" + "</dDI>");

                        // Local do desembaraço aduaneiro
                        // Mínimo 1;
                        // Máximo 60.
                        //xml.AppendLine("                        <xLocDesemb>" + "</xLocDesemb>");

                        // UF onde ocorreu o desembaraço aduaneiro
                        //xml.AppendLine("                        <UFDesemb>" + "</UFDesemb>");

                        // Data do desembaraço aduaneiro (AAAA-MM-DD)
                        //xml.AppendLine("                        <dDesemb>" + "</dDesemb>");

                        // Via de transporte internacional informada na DI
                        // 1 = Maritima; 
                        // 2 = Fluvial; 
                        // 3 = Lacustre; 
                        // 4 = Aerea; 
                        // 5 = Postal; 
                        // 6 = Ferroviaria; 
                        // 7 = Rodoviaria; 
                        // 8 = Conduto; 
                        // 9 = Meios Proprios; 
                        // 10 = Entrada / Saida Ficta.
                        //xml.AppendLine("                        <tpViaTransp>" + "</tpViaTransp>");

                        // Valor Adicional ao frete para renovação de marinha mercante
                        //xml.AppendLine("                        <vAFRMM>" + "</vAFRMM>");

                        // Forma de Importação quanto a intermediação 
                        // 1 = por conta propria; 
                        // 2 = por conta e ordem;
                        // 3 = encomenda
                        //xml.AppendLine("                        <tpIntermedio>" + "</tpIntermedio>");

                        // CNPJ do adquirente ou do encomendante
                        //xml.AppendLine("                        <CNPJ>" + "</CNPJ>");

                        // Sigla da UF do adquirente ou do encomendante
                        //xml.AppendLine("                        <UFTerceiro>" + "</UFTerceiro>");

                        // Código do exportador (usado nos sistemas internos de informação do emitente da NF-e)
                        // Mínimo 1;
                        // Máximo 60.
                        //xml.AppendLine("                        <cExportador>" + "</cExportador>");

                        #region Adições

                        //Adições (NT 2011/004)
                        //xml.AppendLine("                        <adi>");

                        //Número da Adição
                        //xml.AppendLine("                            <nAdicao>" + "<nAdicao>");

                        //Número seqüencial do item dentro da Adição
                        //xml.AppendLine("                            <nSeqAdic>" + "<nSeqAdic>");

                        // Código do fabricante estrangeiro (usado nos sistemas internos de informação do emitente da NF-e)
                        // Mínimo 1;
                        // Máximo 60.
                        //xml.AppendLine("                            <cFabricante>" + "<cFabricante>");

                        // Valor do desconto do item da DI – adição
                        //xml.AppendLine("                            <vDescDI>" + "<vDescDI>");

                        // Número do ato concessório de Drawback
                        //xml.AppendLine("                            <nDraw>" + "<nDraw>");

                        //xml.AppendLine("                        </adi>");

                        #endregion


                        //xml.AppendLine("                    </DI>");

                        #endregion

                        #region Detalhe da exportação

                        // Foreach de até 500 registros!

                        // Declaração de Importação
                        //xml.AppendLine("                    <detExport>");

                        // Número do ato concessório de Drawback
                        //xml.AppendLine("                        <nDraw>" + "<nDraw>");

                        #region Exportação indireta

                        //xml.AppendLine("                        <exportInd>");

                        // Registro de exportação
                        //xml.AppendLine("                            <nRE>" + "<nRE>");

                        // Chave de acesso da NF-e recebida para exportação
                        //xml.AppendLine("                            <chNFe>" + "<chNFe>");

                        // Quantidade do item efetivamente exportado
                        //xml.AppendLine("                            <qExport>" + "<qExport>");

                        //xml.AppendLine("                        </exportInd>");

                        #endregion

                        //xml.AppendLine("                    </detExport>");

                        #endregion

                        // Pedido de compra - Informação de interesse do emissor para controle do B2B.
                        // Minimo 1;
                        // Máximo 15.
                        xml.AppendLine("                    <xPed>" + dados.PEDIDO.ID_PEDIDO + "</xPed>");

                        // Número do Item do Pedido de Compra - Identificação do número do item do pedido de Compra
                        // O ID do item da nota é o mesmo ID do item do pedido!
                        xml.AppendLine("                    <nItemPed>" + item.ID_ITEM + "</nItemPed>");

                        // Número de controle da FCI - Ficha de Conteúdo de Importação.
                        xml.AppendLine("                    <nFCI>" + item.TB_EST_PRODUTO.ID_FCI + "</nFCI>");

                        #region Veículos novos

                        // Veículos novos
                        //xml.AppendLine("                        <veicProd>");

                        // Tipo da Operação:
                        // 1 = Venda concessionária; 
                        // 2 = Faturamento direto;
                        // 3 = Venda direta;
                        // 0 = Outros.
                        //xml.AppendLine("                            <tpOp>" + "</tpOp>");

                        // Chassi do veículo - VIN (código-identificação-veículo)
                        // Tamanho 17
                        // [A-Z0-9]+
                        //xml.AppendLine("                            <chassi>" + "</chassi>");

                        // Cor do veículo (código de cada montadora)
                        // Minimo 1
                        // Maximo 9999
                        //xml.AppendLine("                            <cCor>" + "</cCor>");

                        // Descrição da cor
                        // Minimo 1;
                        // Máximo 40.
                        //xml.AppendLine("                            <xCor>" + "</xCor>");

                        // Potência máxima do motor do veículo em cavalo vapor (CV). (potência-veículo)
                        // Minimo 1
                        // Maximo 9999
                        //xml.AppendLine("                            <pot>" + "</pot>");

                        // Capacidade voluntária do motor expressa em centímetros cúbicos (CC). (cilindradas)
                        // Minimo 1
                        // Maximo 9999
                        //xml.AppendLine("                            <cilin>" + "</cilin>");

                        // Peso líquido
                        // Minimo 1
                        // Máximo 9999
                        //xml.AppendLine("                            <pesoL>" + "</pesoL>");

                        // Peso bruto
                        // Minimo 1
                        // Máximo 9999
                        //xml.AppendLine("                            <pesoB>" + "</pesoB>");

                        // Serial (série)
                        // Minimo 1
                        // Máximo 9999
                        //xml.AppendLine("                            <nSerie>" + "</nSerie>");

                        // Tipo de combustível-Tabela RENAVAM: 
                        // 01 = Álcool;
                        // 02 = Gasolina;
                        // 03 = Diesel;
                        // 16 = Álcool/Gas.;
                        // 17 = Gas./Álcool/GNV; 
                        // 18 = Gasolina/Elétrico
                        //xml.AppendLine("                            <tpComb>" + "</tpComb>");

                        // Número do motor
                        // Minimo 1 caracter
                        // Máximo 21 caracteres
                        //xml.AppendLine("                            <nMotor>" + "</nMotor>");

                        // CMT-Capacidade Máxima de Tração - em Toneladas 4 casas decimais
                        // Minimo 1
                        // Máximo 9 caracteres
                        //xml.AppendLine("                            <CMT>" + "</CMT>");

                        // Distância entre eixos
                        // Minimo 1
                        // Máximo 9999
                        //xml.AppendLine("                            <dist>" + "</dist>");

                        // Ano Modelo de Fabricação
                        // 4 caracteres, sendo números
                        // [0-9]{4}
                        //xml.AppendLine("                            <anoMod>" + "</anoMod>");

                        // Ano de Fabricação
                        // 4 caracteres, sendo números
                        // [0-9]{4}
                        //xml.AppendLine("                            <anoFab>" + "</anoFab>");

                        // Tipo de pintura
                        // 1 caracter
                        //xml.AppendLine("                            <tpPint>" + "</tpPint>");

                        // Tipo de veículo (utilizar tabela RENAVAM)
                        // [0-9]{1,2}
                        //xml.AppendLine("                            <tpVeic>" + "</tpVeic>");

                        // Espécie de veículo (utilizar tabela RENAVAM)
                        // [0-9]{1}
                        //xml.AppendLine("                            <espVeic>" + "</espVeic>");

                        // Informa-se o veículo tem VIN (chassi) remarcado.
                        // R = Remarcado
                        // N = Normal
                        //xml.AppendLine("                            <VIN>" + "</VIN>");

                        // Condição do veículo
                        // 1 = acabado; 
                        // 2 = inacabado; 
                        // 3 = semi-acabado
                        //xml.AppendLine("                            <condVeic>" + "</condVeic>");

                        // Código Marca Modelo (utilizar tabela RENAVAM)
                        // [0-9]{1,6}
                        //xml.AppendLine("                            <cMod>" + "</cMod>");

                        // Código da Cor Segundo as regras de pré-cadastro do DENATRAN:
                        // 01 = AMARELO;
                        // 02 = AZUL;
                        // 03 = BEGE;
                        // 04 = BRANCA;
                        // 05 = CINZA;
                        // 06 = DOURADA;
                        // 07 = GRENA;
                        // 08 = LARANJA;
                        // 09 = MARROM; 
                        // 10 = PRATA; 
                        // 11 = PRETA; 
                        // 12 = ROSA; 
                        // 13 = ROXA;
                        // 14 = VERDE;
                        // 15 = VERMELHA;
                        // 16 = FANTASIA
                        //xml.AppendLine("                            <cCorDENATRAN>" + "</cCorDENATRAN>");

                        // Quantidade máxima de permitida de passageiros sentados, inclusive motorista.
                        // Minimo 1
                        // Maximo 999
                        // [0-9]{1,3}
                        //xml.AppendLine("                            <lota>" + "</lota>");

                        // Restrição
                        // 0 = Não há;
                        // 1 = Alienação Fiduciária;
                        // 2 = Arrendamento Mercantil;
                        // 3 = Reserva de Domínio;
                        // 4 = Penhor de Veículos;
                        // 9 = outras.
                        //xml.AppendLine("                            <tpRest>" + "</tpRest>");

                        //xml.AppendLine("                        </veicProd>");

                        #endregion

                        #region Grupo do detalhamento de Medicamentos e de matérias-primas farmacêuticas

                        // Utilizar foreach!!! Máximo 500 

                        // Grupo do detalhamento de Medicamentos e de matérias-primas farmacêuticas
                        //xml.AppendLine("                        <med>");

                        // Número do lote do medicamento
                        // Mínimo 1 caracter;
                        // Maximo 20 caracteres.
                        //xml.AppendLine("                                <nLote>" + "</nLote>");

                        // Quantidade de produtos no lote
                        //xml.AppendLine("                                <qLote>" + "</qLote>");

                        // Data de Fabricação do medicamento (AAAA-MM-DD)
                        //xml.AppendLine("                                <dFab>" + "</dFab>");

                        // Data de validade do medicamento (AAAA-MM-DD)
                        //xml.AppendLine("                                <dVal>" + "</dVal>");

                        // Preço Máximo ao Consumidor
                        //xml.AppendLine("                                <vPMC>" + "</vPMC>");

                        //xml.AppendLine("                        <med>");

                        #endregion

                        #region Armamentos

                        // Utilizar foreach!!! Máximo 500 

                        // Armamentos
                        //xml.AppendLine("                        <arma>");

                        // Indicador do tipo de arma de fogo
                        // 0 = Uso permitido;
                        // 1 = Uso restrito.
                        //xml.AppendLine("                            <tpArma>" + "</tpArma>");

                        // Número de série da arma
                        // Minimo 1
                        // Maximo 15
                        //xml.AppendLine("                            <nSerie>" + "</nSerie>");

                        // Número de série do cano
                        // Minimo 1
                        // Maximo 15
                        //xml.AppendLine("                            <nCano>" + "</nCano>");

                        // Descrição completa da arma, compreendendo: calibre, marca, capacidade, tipo de funcionamento, comprimento e demais elementos que permitam a sua perfeita identificação.
                        // Minimo 1
                        // Maximo 256
                        //xml.AppendLine("                            <descr>" + "</descr>");


                        //xml.AppendLine("                        </arma>");

                        #endregion

                        #region Informar apenas para operações com combustíveis líquidos

                        // Informar apenas para operações com combustíveis líquidos
                        //xml.AppendLine("                        <comb>");

                        // Código de produto da ANP. codificação de produtos do SIMP (http://www.anp.gov.br)
                        //xml.AppendLine("                            <cProdANP>" + "</cProdANP>");

                        // Percentual de gas natural para o produto GLP
                        //xml.AppendLine("                            <pMixGN>" + "</pMixGN>");

                        // Código de autorização / registro do CODIF. Informar apenas quando a UF utilizar o CODIF (Sistema de Controle do 			Diferimento do Imposto nas Operações com AEAC - Álcool Etílico Anidro Combustível).
                        // [0-9]{1,21}
                        //xml.AppendLine("                            <CODIF>" + "</CODIF>");

                        // Quantidade de combustível faturada à temperatura ambiente. Informar quando a quantidade faturada informada no campo qCom(I10) tiver sido ajustada para uma temperatura diferente da ambiente.
                        //xml.AppendLine("                            <qTemp>" + "</qTemp>");

                        // Sigla da UF de Consumo
                        //xml.AppendLine("                            <UFCons>" + "</UFCons>");

                        #region CIDE Combustíveis

                        // CIDE Combustíveis
                        //xml.AppendLine("                            <CIDE>");

                        // BC do CIDE ( Quantidade comercializada) 
                        //xml.AppendLine("                                <qBCProd>" + "</qBCProd>");

                        // Alíquota do CIDE  (em reais) 
                        //xml.AppendLine("                                <vAliqProd>" + "</vAliqProd>");

                        // Valor do CIDE
                        //xml.AppendLine("                                <vCIDE>" + "</vCIDE>");

                        // Sigla da UF de Consumo
                        //xml.AppendLine("                            </CIDE>");

                        #endregion

                        #region Informações do grupo de "encerrante"

                        // Informações do grupo de "encerrante"
                        //xml.AppendLine("                            <encerrante>");

                        // Numero de identificação do Bico utilizado no abastecimento
                        // [0-9]{1,3}
                        //xml.AppendLine("                                <nBico>" + "</nBico>");

                        // Numero de identificação da bomba ao qual o bico está interligado
                        // [0-9]{1,3}
                        //xml.AppendLine("                                <nBomba>" + "</nBomba>");

                        // Numero de identificação do tanque ao qual o bico está interligado
                        // [0-9]{1,3}
                        //xml.AppendLine("                                <nTanque>" + "</nTanque>");

                        // Valor do Encerrante no ínicio do abastecimento
                        // [0-9]{1,15}
                        //xml.AppendLine("                                <vEncIni>" + "</vEncIni>");

                        // Valor do Encerrante no final do abastecimento
                        // [0-9]{1,15}
                        //xml.AppendLine("                                <vEncFin>" + "</vEncFin>");

                        // Sigla da UF de Consumo
                        //xml.AppendLine("                            </encerrante>");

                        #endregion

                        //xml.AppendLine("                        </comb>");

                        #endregion

                        // Número do RECOPI
                        //[0-9]{20}
                        //xml.AppendLine("                    <nRECOPI>" + "</nRECOPI>");

                        xml.AppendLine("                </prod>");

                        #endregion

                        #region Tributos incidentes nos produtos ou serviços da NF-e

                        // Tributos incidentes nos produtos ou serviços da NF-e
                        xml.AppendLine("                <imposto>");

                        var vTotTrib = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Sum(a => a.VL.Padrao());
                        vlEstimadoImpostos += vTotTrib;

                        // Valor estimado total de impostos federais, estaduais e municipais
                        xml.AppendLine("                    <vTotTrib>" + vTotTrib.ENUS(10) + "</vTotTrib>");

                        // Origem da mercadoria. Se baseia no primeiro número do código da situação tributária.
                        if (!item.TB_EST_PRODUTO.ID_CST.TemValor())
                            throw new SYSException(Mensagens.Necessario("o cst do produto " + item.ID_PRODUTO + " - " + item.TB_EST_PRODUTO.NM.Validar()));

                        var origem = item.TB_EST_PRODUTO.ID_CST[0];

                        // Código da situação tributária. Se baseia no segundo e terceiro número do mesmo.
                        var cst = (item.TB_EST_PRODUTO.ID_CST[1] + item.TB_EST_PRODUTO.ID_CST[2]).ToString();

                        // Código de Situação da Operação no Simples Nacional
                        var csosn = item.TB_EST_PRODUTO.ID_CSOSN.Padrao();

                        #region ICMS

                        #region Bases de calculo

                        var temp = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao();
                        bcICMS_Total += temp;
                        var bcICMS = temp.ENUS(5);

                        temp = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao() &&
                                                                                 a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao();
                        bcICMSST_Total += temp;
                        var bcICMSST = temp.ENUS(5);

                        var bcICMSDesonerado = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao() &&
                                                                                         !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                          a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                         !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                         !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                         !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        var bcICMSDiferido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        var bcICMSSTRetido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao() &&
                                                                                     a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                     a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        temp = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                     a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao();
                        bcICMSRetido_Total += temp;
                        var bcICMSRetido = temp.ENUS(5);

                        var bcICMSSTReducao = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao() &&
                                                                                        a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        #endregion

                        #region Porcentagem de aliquotas

                        var pcICMS = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        var pcICMSST = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao() &&
                                                                                 a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        var pcICMSDesonerado = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                         a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        var pcICMSDiferido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.TB_FIS_TRIBUTO.PC_DIFERIDO).Padrao().ENUS(5);

                        var pcICMSSTRetido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao() &&
                                                                                     a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                     a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        temp = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao();
                        pcICMSRetido_Total += temp;
                        var pcICMSRetido = temp.ENUS(5);

                        var pcICMSReducao = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao() &&
                                                                                     !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                     !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                     !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                     !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);


                        var pcICMSSTReducao = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao() &&
                                                                                        a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        #endregion

                        #region Valores

                        temp = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao() &&
                                                                               !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                               !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                               !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                               !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                               !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao();
                        vlICMS_Total += temp;
                        var vlICMS = temp.ENUS(5);

                        temp = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao() &&
                                                                                 a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao();
                        vlICMSST_Total += temp;
                        var vlICMSST = temp.ENUS(5);


                        temp = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                         a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao();
                        vlICMSDesonerado_Total += temp;
                        var vlICMSDesonerado = temp.ENUS(5);

                        var vlICMSDiferido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao().ENUS(5);

                        temp = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao();
                        vlICMSSTRetido_Total += temp;
                        var vlICMSSTRetido = temp.ENUS(5);

                        temp = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao();
                        vlICMSRetido_Total += temp;
                        var vlICMSRetido = temp.ENUS(5);

                        var vlICMSReducao = (item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => (a.PC_ALIQUOTA.Padrao() / 100) * item.QT.Padrao() - (a.TB_FIS_TRIBUTO.PC_REDUCAO.Padrao() / 100) * item.QT.Padrao())).ENUS(5);

                        #endregion

                        // Modalidade do ICMS.
                        var modalidadeICMS = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao()).FirstOrDefault().TB_FIS_TRIBUTO.ID_MODALIDADE.Padrao();

                        // Modalidade do ICMS com substituição tributária.
                        var modalidadeICMSST = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao()).FirstOrDefault().TB_FIS_TRIBUTO.ID_MODALIDADE_SUBSTITUICAOTRIBUTARIA.Padrao();

                        var pcBCICMSReducao = ((pcICMSReducao.ToDecimal().Padrao() / 100) * vlICMS.ToDecimal().Padrao()).ENUS(10);
                        var pcBCICMSSTReducao = ((pcICMSSTReducao.ToDecimal().Padrao() / 100) * vlICMSST.ToDecimal().Padrao()).ENUS(10);
                        var pcMVASTICMS = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao()).Sum(a => a.TB_FIS_TRIBUTO.PC_MVA_SUBSTITUICAOTRIBUTARIA.Padrao()).ENUS(10);
                        var pcCreditoICMS = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao()).Sum(a => a.TB_FIS_TRIBUTO.PC_ALIQUOTA_CALCULOCREDITO.Padrao()).ENUS(10);
                        var desoneracaoICMS = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I08.Padrao()).FirstOrDefault().TB_FIS_TRIBUTO.ID_DESONERACAO;

                        // Dados do ICMS Normal e ST
                        xml.AppendLine("                    <ICMS>");

                        #region 00 - Tributada integralmente

                        if (dados.EMITENTE_REGIMETRIBUTARIO != "1" && cst == "00")
                        {
                            // 00 - Tributada integralmente
                            xml.AppendLine("                        <ICMS00>");

                            // Origem da mercadoria:
                            // 0 = Nacional 
                            // 1 = Estrangeira - Importação direta
                            // 2 = Estrangeira - Adquirida no mercado interno
                            xml.AppendLine("                            <orig>" + origem + "</orig>");

                            // Tributação pelo ICMS
                            // 00 - Tributada integralmente
                            xml.AppendLine("                            <CST>" + cst + "</CST>");

                            // Modalidade de determinação da BC do ICMS:
                            // 0 = Margem Valor Agregado (%);
                            // 1 = Pauta(valor);
                            // 2 = Preço Tabelado Máximo (valor);
                            // 3 = Valor da Operação.
                            xml.AppendLine("                            <modBC>" + modalidadeICMS + "</modBC>");

                            // Valor da BC do ICMS
                            xml.AppendLine("                            <vBC>" + bcICMS + "</vBC>");

                            // Alíquota do ICMS
                            xml.AppendLine("                            <pICMS>" + pcICMS + "</pICMS>");

                            // Valor do ICMS
                            xml.AppendLine("                            <vICMS>" + vlICMS + "</vICMS>");

                            xml.AppendLine("                        </ICMS00>");
                        }

                        #endregion

                        #region 10 - Tributada e com cobrança do ICMS por substituição tributária

                        if (dados.EMITENTE_REGIMETRIBUTARIO != "1" && cst == "10")
                        {
                            // 10 - Tributada e com cobrança do ICMS por substituição tributária
                            xml.AppendLine("                        <ICMS10>");

                            // Origem da mercadoria:
                            // 0 = Nacional 
                            // 1 = Estrangeira - Importação direta
                            // 2 = Estrangeira - Adquirida no mercado interno
                            xml.AppendLine("                            <orig>" + origem + "</orig>");

                            // Tributação pelo ICMS
                            // 10 - Tributada e com cobrança do ICMS por substituição tributária 
                            xml.AppendLine("                            <CST>" + cst + "</CST>");

                            // Modalidade de determinação da BC do ICMS:
                            // 0 = Margem Valor Agregado (%);
                            // 1 = Pauta(valor);
                            // 2 = Preço Tabelado Máximo (valor);
                            // 3 = Valor da Operação.
                            xml.AppendLine("                            <modBC>" + modalidadeICMS + "</modBC>");

                            // Valor da BC do ICMS
                            xml.AppendLine("                            <vBC>" + bcICMS + "</vBC>");

                            // Alíquota do ICMS
                            xml.AppendLine("                            <pICMS>" + pcICMS + "</pICMS>");

                            // Valor do ICMS
                            xml.AppendLine("                            <vICMS>" + vlICMS + "</vICMS>");

                            // Modalidade de determinação da BC do ICMS ST:
                            // 0 = Preço tabelado ou máximo  sugerido;
                            // 1 = Lista Negativa(valor);
                            // 2 = Lista Positiva(valor);
                            // 3 = Lista Neutra(valor);
                            // 4 = Margem Valor Agregado (%);
                            // 5 = Pauta(valor);
                            xml.AppendLine("                            <modBCST>" + modalidadeICMSST + "</modBCST>");

                            // Percentual da Margem de Valor Adicionado ICMS ST
                            xml.AppendLine("                            <pMVAST>" + pcMVASTICMS + "</pMVAST>");

                            // Percentual de redução da BC ICMS ST
                            xml.AppendLine("                            <pRedBCST>" + pcBCICMSSTReducao + "</pRedBCST>");

                            // Valor da BC do ICMS ST
                            xml.AppendLine("                            <vBCST>" + bcICMSST + "</vBCST>");

                            // Alíquota do ICMS ST
                            xml.AppendLine("                            <pICMSST>" + pcICMSST + "</pICMSST>");

                            // Valor do ICMS ST
                            xml.AppendLine("                            <vICMSST>" + vlICMSST + "</vICMSST>");

                            xml.AppendLine("                        </ICMS10>");
                        }

                        #endregion

                        #region 20 - Com redução de base de cálculo

                        if (dados.EMITENTE_REGIMETRIBUTARIO != "1" && cst == "20")
                        {
                            // 20 - Com redução de base de cálculo
                            xml.AppendLine("                        <ICMS20>");

                            // Origem da mercadoria:
                            // 0 = Nacional 
                            // 1 = Estrangeira - Importação direta
                            // 2 = Estrangeira - Adquirida no mercado interno
                            xml.AppendLine("                            <orig>" + origem + "</orig>");

                            // Tributação pelo ICMS
                            // 20 - Com redução de base de cálculo
                            xml.AppendLine("                            <CST>" + cst + "</CST>");

                            // Modalidade de determinação da BC do ICMS:
                            // 0 = Margem Valor Agregado (%);
                            // 1 = Pauta(valor);
                            // 2 = Preço Tabelado Máximo (valor);
                            // 3 = Valor da Operação.
                            xml.AppendLine("                            <modBC>" + modalidadeICMS + "</modBC>");

                            // Percentual de redução da BC
                            xml.AppendLine("                            <pRedBC>" + "</pRedBC>");

                            // Valor da BC do ICMS
                            xml.AppendLine("                            <vBC>" + bcICMS + "</vBC>");

                            // Alíquota do ICMS
                            xml.AppendLine("                            <pICMS>" + pcICMS + "</pICMS>");

                            // Valor do ICMS
                            xml.AppendLine("                            <vICMS>" + vlICMS + "</vICMS>");

                            // Valor do ICMS de desoneração
                            xml.AppendLine("                            <vICMSDeson>" + vlICMSDesonerado + "</vICMSDeson>");

                            // Motivo da desoneração do ICMS:
                            // 3 = Uso na agropecuária;
                            // 9 = Outros;
                            // 12 = Fomento agropecuário
                            xml.AppendLine("                            <motDesICMS>" + desoneracaoICMS + "</motDesICMS>");

                            xml.AppendLine("                        </ICMS20>");
                        }

                        #endregion

                        #region 30 - Isenta ou não tributada e com cobrança do ICMS por substituição tributária

                        if (dados.EMITENTE_REGIMETRIBUTARIO != "1" && cst == "30")
                        {
                            // 30 - Isenta ou não tributada e com cobrança do ICMS por substituição tributária
                            xml.AppendLine("                        <ICMS30>");

                            // Origem da mercadoria:
                            // 0 = Nacional 
                            // 1 = Estrangeira - Importação direta
                            // 2 = Estrangeira - Adquirida no mercado interno
                            xml.AppendLine("                            <orig>" + origem + "</orig>");

                            // Tributação pelo ICMS
                            // 30 - Isenta ou não tributada e com cobrança do ICMS por substituição tributária
                            xml.AppendLine("                            <CST>" + cst + "</CST>");

                            // Modalidade de determinação da BC do ICMS:
                            // 0 – Preço tabelado ou máximo  sugerido;
                            // 1 - Lista Negativa(valor);
                            // 2 - Lista Positiva(valor);
                            // 3 - Lista Neutra(valor);
                            // 4 - Margem Valor Agregado (%);
                            // 5 - Pauta(valor).
                            xml.AppendLine("                            <modBC>" + modalidadeICMS + "</modBC>");

                            // Percentual da Margem de Valor Adicionado ICMS ST
                            xml.AppendLine("                            <pMVAST>" + pcMVASTICMS + "</pMVAST>");

                            // Percentual de redução da BC ICMS ST
                            xml.AppendLine("                            <pRedBCST>" + pcBCICMSSTReducao + "</pRedBCST>");

                            // Valor da BC do ICMS ST
                            xml.AppendLine("                            <vBCST>" + bcICMSST + "</vBCST>");

                            // Alíquota do ICMS ST
                            xml.AppendLine("                            <pICMSST>" + pcICMSST + "</pICMSST>");

                            // Valor do ICMS ST
                            xml.AppendLine("                            <vICMSST>" + vlICMSST + "</vICMSST>");

                            // Valor do ICMS de desoneração
                            xml.AppendLine("                            <vICMSDeson>" + vlICMSDesonerado + "</vICMSDeson>");

                            // Motivo da desoneração do ICMS:
                            // 6 -Utilitários Motocicleta AÁrea Livre;
                            // 7 -SUFRAMA;
                            // 9 -Outros
                            xml.AppendLine("                            <motDesICMS>" + desoneracaoICMS + "</motDesICMS>");

                            xml.AppendLine("                        </ICMS30>");
                        }

                        #endregion

                        #region 40 - Isenta / 41 - Não tributada / 50 - Suspensão

                        if (dados.EMITENTE_REGIMETRIBUTARIO != "1" && (cst == "40" || cst == "41" || cst == "50"))
                        {
                            // Tributação pelo ICMS 40 - Isenta / 41 - Não tributada / 50 - Suspensão
                            xml.AppendLine("                        <ICMS40>");

                            // Origem da mercadoria:
                            // 0 = Nacional 
                            // 1 = Estrangeira - Importação direta
                            // 2 = Estrangeira - Adquirida no mercado interno
                            xml.AppendLine("                            <orig>" + origem + "</orig>");

                            // Tributação pelo ICMS 
                            // 40 - Isenta
                            // 41 - Não tributada
                            // 50 - Suspensão
                            // 51 - Diferimento
                            xml.AppendLine("                            <CST>" + cst + "</CST>");

                            // O valor do ICMS será informado apenas nas operações com veículos beneficiados com a desoneração condicional do ICMS.
                            xml.AppendLine("                            <vICMSDeson>" + vlICMSDesonerado + "</vICMSDeson>");

                            // Este campo será preenchido quando o campo anterior estiver preenchido.
                            // Informar o motivo da desoneração:
                            // 1 – Táxi;
                            // 3 – Produtor Agropecuário;
                            // 4 – Frotista / Locadora;
                            // 5 – Diplomático / Consular;
                            // 6 – Utilitários e Motocicletas da Amazônia Ocidental e Áreas de Livre Comércio(Resolução 714 / 88 e 790 / 94 – CONTRAN e suas alterações);
                            // 7 – SUFRAMA;
                            // 8 - Venda a órgão Público;
                            // 9 – Outros
                            // 10 - Deficiente Condutor
                            // 11 - Deficiente não condutor
                            // 16 - Olimpíadas Rio 2016
                            xml.AppendLine("                            <motDesICMS>" + desoneracaoICMS + "</motDesICMS>");

                            xml.AppendLine("                        </ICMS40>");
                        }

                        #endregion

                        #region 51 - Diferimento

                        if (dados.EMITENTE_REGIMETRIBUTARIO != "1" && cst == "51")
                        {
                            // 51 - Diferimento
                            xml.AppendLine("                        <ICMS51>");

                            // Origem da mercadoria:
                            // 0 = Nacional 
                            // 1 = Estrangeira - Importação direta
                            // 2 = Estrangeira - Adquirida no mercado interno
                            xml.AppendLine("                            <orig>" + origem + "</orig>");

                            // Tributação pelo ICMS
                            // 51 - Diferimento
                            xml.AppendLine("                            <CST>" + cst + "</CST>");

                            // Modalidade de determinação da BC do ICMS:
                            // 0 - Margem Valor Agregado (%);
                            // 1 - Pauta(valor);
                            // 2 - Preço Tabelado Máximo (valor);
                            // 3 - Valor da Operação.
                            xml.AppendLine("                            <modBC>" + modalidadeICMS + "</modBC>");

                            // Percentual de redução da BC
                            xml.AppendLine("                            <pRedBC>" + pcBCICMSReducao + "</pRedBC>");

                            // Valor da BC do ICMS
                            xml.AppendLine("                            <vBC>" + ((pcICMSReducao.ToDecimal().Padrao() / 100) * bcICMS.ToDecimal().Padrao()).ENUS(10) + "</vBC>");

                            // Alíquota do ICMS
                            xml.AppendLine("                            <pICMS>" + pcICMS + "</pICMS>");

                            // Valor do ICMS da Operação
                            xml.AppendLine("                            <vICMSOp>" + ((pcICMS.ToDecimal().Padrao() / 100) * ((pcICMSReducao.ToDecimal().Padrao() / 100) * bcICMS.ToDecimal().Padrao())).ENUS(10) + "</vICMSOp>");

                            // Percentual do diferemento
                            xml.AppendLine("                            <pDif>" + pcICMSDiferido + "</pDif>");

                            // Valor do ICMS da diferido
                            xml.AppendLine("                            <vICMSDif>" + ((pcICMSDiferido.ToDecimal().Padrao() / 100) * ((pcICMS.ToDecimal().Padrao() / 100) * ((pcICMSReducao.ToDecimal().Padrao() / 100) * bcICMS.ToDecimal().Padrao()))).ENUS(10) + "</vICMSDif>");

                            // Valor do ICMS
                            xml.AppendLine("                            <vICMS>" + (((pcICMS.ToDecimal().Padrao() / 100) * ((pcICMSReducao.ToDecimal().Padrao() / 100) * bcICMS.ToDecimal().Padrao())) - ((pcICMSDiferido.ToDecimal().Padrao() / 100) * ((pcICMS.ToDecimal().Padrao() / 100) * ((pcICMSReducao.ToDecimal().Padrao() / 100) * bcICMS.ToDecimal().Padrao())))).ENUS(10) + "</vICMS>");

                            xml.AppendLine("                        </ICMS51>");
                        }

                        #endregion

                        #region 60 - ICMS cobrado anteriormente por substituição tributária

                        if (dados.EMITENTE_REGIMETRIBUTARIO != "1" && cst == "60")
                        {
                            // 60 - ICMS cobrado anteriormente por substituição tributária
                            xml.AppendLine("                        <ICMS60>");

                            // Origem da mercadoria:
                            // 0 = Nacional 
                            // 1 = Estrangeira - Importação direta
                            // 2 = Estrangeira - Adquirida no mercado interno
                            xml.AppendLine("                            <orig>" + origem + "</orig>");

                            // Tributação pelo ICMS
                            // 60 - ICMS cobrado anteriormente por substituição tributária
                            xml.AppendLine("                            <CST>" + cst + "</CST>");

                            // Valor da BC do ICMS ST retido anteriormente
                            xml.AppendLine("                            <vBCSTRet>" + bcICMSSTRetido + "</vBCSTRet>");

                            // Valor do ICMS ST retido anteriormente
                            xml.AppendLine("                            <vICMSSTRet>" + vlICMSSTRetido + "</vICMSSTRet>");

                            xml.AppendLine("                        </ICMS60>");
                        }

                        #endregion

                        #region 70 - Com redução de base de cálculo e cobrança do ICMS por substituição tributária

                        if (dados.EMITENTE_REGIMETRIBUTARIO != "1" && cst == "70")
                        {
                            // 70 - Com redução de base de cálculo e cobrança do ICMS por substituição tributária
                            xml.AppendLine("                        <ICMS70>");

                            // Origem da mercadoria:
                            // 0 = Nacional 
                            // 1 = Estrangeira - Importação direta
                            // 2 = Estrangeira - Adquirida no mercado interno
                            xml.AppendLine("                            <orig>" + origem + "</orig>");

                            // Tributação pelo ICMS
                            // 70 - Com redução de base de cálculo e cobrança do ICMS por substituição tributária
                            xml.AppendLine("                            <CST>" + cst + "</CST>");

                            // Valor da BC do ICMS ST retido anteriormente
                            xml.AppendLine("                            <vBCSTRet>" + bcICMSSTRetido + "</vBCSTRet>");

                            // Modalidade de determinação da BC do ICMS:
                            // 0 - Margem Valor Agregado (%);
                            // 1 - Pauta(valor);
                            // 2 - Preço Tabelado Máximo (valor);
                            // 3 - Valor da Operação.
                            xml.AppendLine("                            <modBC>" + modalidadeICMS + "</modBC>");

                            // Percentual de redução da BC
                            xml.AppendLine("                            <pRedBC>" + pcBCICMSReducao + "</pRedBC>");

                            // Valor da BC do ICMS
                            xml.AppendLine("                            <vBC>" + bcICMS + "</vBC>");

                            // Alíquota do ICMS
                            xml.AppendLine("                            <pICMS>" + pcICMS + "</pICMS>");

                            // Valor do ICMS
                            xml.AppendLine("                            <vICMS>" + vlICMS + "</vICMS>");

                            // Modalidade de determinação da BC do ICMS ST:
                            // 0 – Preço tabelado ou máximo  sugerido;
                            // 1 - Lista Negativa(valor);
                            // 2 - Lista Positiva(valor);
                            // 3 - Lista Neutra(valor);
                            // 4 - Margem Valor Agregado (%);
                            // 5 - Pauta(valor).
                            xml.AppendLine("                            <modBCST>" + modalidadeICMSST + "</modBCST>");

                            // Percentual da Margem de Valor Adicionado ICMS ST
                            xml.AppendLine("                            <pMVAST>" + pcMVASTICMS + "</pMVAST>");

                            // Percentual de redução da BC ICMS ST 
                            xml.AppendLine("                            <pRedBCST>" + pcBCICMSSTReducao + "</pRedBCST>");

                            // Valor da BC do ICMS ST
                            xml.AppendLine("                            <vBCST>" + bcICMSST + "</vBCST>");

                            // Alíquota do ICMS ST
                            xml.AppendLine("                            <pICMSST>" + pcICMSST + "</pICMSST>");

                            // Valor do ICMS ST
                            xml.AppendLine("                            <vICMSST>" + vlICMSST + "</vICMSST>");

                            // Grupo desoneração
                            xml.AppendLine("                            <vICMSDeson>" + vlICMSDesonerado + "</vICMSDeson>");

                            // Motivo da desoneração do ICMS:
                            // 3 -Uso na agropecuária;
                            // 9 -Outros;
                            // 12 -Fomento agropecuário
                            xml.AppendLine("                            <motDesICMS>" + desoneracaoICMS + "</motDesICMS>");

                            xml.AppendLine("                        </ICMS70>");
                        }

                        #endregion

                        #region 90 - Outras

                        if (dados.EMITENTE_REGIMETRIBUTARIO != "1" && cst == "90")
                        {
                            // 90 - Outras
                            xml.AppendLine("                        <ICMS90>");

                            // Origem da mercadoria:
                            // 0 = Nacional 
                            // 1 = Estrangeira - Importação direta
                            // 2 = Estrangeira - Adquirida no mercado interno
                            xml.AppendLine("                            <orig>" + origem + "</orig>");

                            // Tributação pelo ICMS
                            // 90 - Outras
                            xml.AppendLine("                            <CST>" + cst + "</CST>");

                            // Valor da BC do ICMS ST retido anteriormente
                            // 0 - Margem Valor Agregado (%);
                            // 1 - Pauta(valor);
                            // 2 - Preço Tabelado Máximo (valor);
                            // 3 - Valor da Operação.
                            xml.AppendLine("                            <modBC>" + modalidadeICMSST + "</modBC>");

                            // Valor da BC do ICMS
                            xml.AppendLine("                            <vBC>" + bcICMS + "</vBC>");

                            // Percentual de redução da BC
                            xml.AppendLine("                            <pRedBC>" + pcBCICMSReducao + "</pRedBC>");

                            // Alíquota do ICMS
                            xml.AppendLine("                            <pICMS>" + pcICMS + "</pICMS>");

                            // Valor do ICMS
                            xml.AppendLine("                            <vICMS>" + vlICMS + "</vICMS>");

                            // Modalidade de determinação da BC do ICMS ST:
                            // 0 – Preço tabelado ou máximo  sugerido;
                            // 1 - Lista Negativa(valor);
                            // 2 - Lista Positiva(valor);
                            // 3 - Lista Neutra(valor);
                            // 4 - Margem Valor Agregado (%);
                            // 5 - Pauta(valor).
                            xml.AppendLine("                            <modBCST>" + modalidadeICMSST + "</modBCST>");

                            // Percentual da Margem de Valor Adicionado ICMS ST
                            xml.AppendLine("                            <pMVAST>" + pcMVASTICMS + "</pMVAST>");

                            // Percentual de redução da BC ICMS ST
                            xml.AppendLine("                            <pRedBCST>" + pcBCICMSSTReducao + "</pRedBCST>");

                            // Valor da BC do ICMS ST
                            xml.AppendLine("                            <vBCST>" + bcICMSST + "</vBCST>");

                            // Alíquota do ICMS ST
                            xml.AppendLine("                            <pICMSST>" + pcICMSST + "</pICMSST>");

                            // Valor do ICMS ST
                            xml.AppendLine("                            <vICMSST>" + vlICMSST + "</vICMSST>");

                            // Valor do ICMS de desoneração
                            xml.AppendLine("                            <vICMSDeson>" + vlICMSDesonerado + "</vICMSDeson>");

                            // Motivo da desoneração do ICMS:
                            // 3 -Uso na agropecuária;
                            // 9 -Outros;
                            // 12 -Fomento agropecuário
                            xml.AppendLine("                            <motDesICMS>" + desoneracaoICMS + "</motDesICMS>");

                            xml.AppendLine("                        </ICMS90>");
                        }

                        #endregion

                        // Verificar! Essa tag é utilizada quando a operação é interestadual e gera ICMS parcial para ambas UFs.

                        #region Partilha do ICMS entre a UF de origem e UF de destino ou a UF definida na legislação Operação interestadual para consumidor final com partilha do ICMS devido na operação entre a UF de origem e a UF do destinatário ou ou a UF definida na legislação. (Ex.UF da concessionária de entrega do veículos)

                        // Partilha do ICMS entre a UF de origem e UF de destino ou a UF definida na legislação Operação interestadual para consumidor final com partilha do ICMS devido na operação entre a UF de origem e a UF do destinatário ou ou a UF definida na legislação. (Ex.UF da concessionária de entrega do veículos)
                        //xml.AppendLine("                        <ICMSPart>");

                        // Origem da mercadoria:
                        // 0 = Nacional 
                        // 1 = Estrangeira - Importação direta
                        // 2 = Estrangeira - Adquirida no mercado interno
                        //xml.AppendLine("                            <orig>" + "</orig>");

                        // Tributação pelo ICMS
                        // 10 - Tributada e com cobrança do ICMS por substituição tributária;
                        // 90 – Outros
                        //xml.AppendLine("                            <CST>" + "</CST>");

                        // Modalidade de determinação da BC do ICMS: 
                        // 0 - Margem Valor Agregado (%);
                        // 1 - Pauta(valor);
                        // 2 - Preço Tabelado Máximo (valor);
                        // 3 - Valor da Operação.
                        //xml.AppendLine("                            <modBC>" + "</modBC>");

                        // Valor da BC do ICMS
                        //xml.AppendLine("                            <vBC>" + "</vBC>");

                        // Percentual de redução da BC
                        //xml.AppendLine("                            <pRedBC>" + "</pRedBC>");

                        // Alíquota do ICMS
                        //xml.AppendLine("                            <pICMS>" + "</pICMS>");

                        // Valor do ICMS
                        //xml.AppendLine("                            <vICMS>" + "</vICMS>");

                        // Modalidade de determinação da BC do ICMS ST:
                        // 0 – Preço tabelado ou máximo  sugerido;
                        // 1 - Lista Negativa(valor);
                        // 2 - Lista Positiva(valor);
                        // 3 - Lista Neutra(valor);
                        // 4 - Margem Valor Agregado (%);
                        // 5 - Pauta(valor).
                        //xml.AppendLine("                            <modBCST>" + "</modBCST>");

                        // Percentual da Margem de Valor Adicionado ICMS ST
                        //xml.AppendLine("                            <pMVAST>" + "</pMVAST>");

                        // Percentual de redução da BC ICMS ST
                        //xml.AppendLine("                            <pRedBCST>" + "</pRedBCST>");

                        // Valor da BC do ICMS ST
                        //xml.AppendLine("                            <vBCST>" + "</vBCST>");

                        // Alíquota do ICMS ST
                        //xml.AppendLine("                            <pICMSST>" + "</pICMSST>");

                        // Valor do ICMS ST
                        //xml.AppendLine("                            <vICMSST>" + "</vICMSST>");

                        // Percentual para determinação do valor  da Base de Cálculo da operação própria.
                        //xml.AppendLine("                            <pBCOp>" + "</pBCOp>");

                        // Sigla da UF para qual é devido o ICMS ST da operação.
                        //xml.AppendLine("                            <UFST>" + "</UFST>");

                        //xml.AppendLine("                        </ICMSPart>");

                        #endregion

                        #region Grupo de informação do ICMSST devido para a UF de destino, nas operações interestaduais de produtos que tiveram retenção antecipada de ICMS por ST na UF do remetente. Repasse via Substituto Tributário.

                        // Grupo de informação do ICMSST devido para a UF de destino, nas operações interestaduais de produtos que tiveram retenção antecipada de ICMS por ST na UF do remetente. Repasse via Substituto Tributário.
                        //xml.AppendLine("                        <ICMSST>");

                        // Origem da mercadoria:
                        // 0 = Nacional 
                        // 1 = Estrangeira - Importação direta
                        // 2 = Estrangeira - Adquirida no mercado interno
                        //xml.AppendLine("                            <orig>" + "</orig>");

                        // Tributação pelo ICMS
                        // 10 - Tributada e com cobrança do ICMS por substituição tributária;
                        // 90 – Outros
                        //xml.AppendLine("                            <CST>41</CST>");

                        // Informar o valor da BC do ICMS ST retido na UF remetente
                        //xml.AppendLine("                            <vBCSTRet>" + "</vBCSTRet>");

                        // Informar o valor do ICMS ST retido na UF remetente (iv2.0))
                        //xml.AppendLine("                            <vICMSSTRet>" + "</vICMSSTRet>");

                        // Informar o valor da BC do ICMS ST da UF destino
                        //xml.AppendLine("                            <vBCSTDest>" + "</vBCSTDest>");

                        // Informar o valor da BC do ICMS ST da UF destino (v2.0)
                        //xml.AppendLine("                            <vICMSSTDest>" + "</vICMSSTDest>");

                        //xml.AppendLine("                        </ICMSST>");

                        #endregion

                        #region Tributação do ICMS pelo SIMPLES NACIONAL e CSOSN=101 (v.2.0)

                        if (dados.EMITENTE_REGIMETRIBUTARIO == "1" && csosn == "101")
                        {
                            // Tributação do ICMS pelo SIMPLES NACIONAL e CSOSN=101 (v.2.0)
                            xml.AppendLine("                        <ICMSSN101>");

                            // Origem da mercadoria:
                            // 0 = Nacional 
                            // 1 = Estrangeira - Importação direta
                            // 2 = Estrangeira - Adquirida no mercado interno
                            xml.AppendLine("                            <orig>" + origem + "</orig>");

                            // 101- Tributada pelo Simples Nacional com permissão de crédito. (v.2.0)
                            xml.AppendLine("                            <CSOSN>" + csosn + "</CSOSN>");

                            // Alíquota aplicável de cálculo do crédito (Simples Nacional). (v2.0)
                            xml.AppendLine("                            <pCredSN>" + pcCreditoICMS + "</pCredSN>");

                            // Valor crédito do ICMS que pode ser aproveitado nos termos do art. 23 da LC 123 (Simples Nacional) (v2.0)
                            xml.AppendLine("                            <vCredICMSSN>" + vlICMS + "</vCredICMSSN>");

                            xml.AppendLine("                        </ICMSSN101>");
                        }

                        #endregion

                        #region Tributação do ICMS pelo SIMPLES NACIONAL e CSOSN=102, 103, 300 ou 400 (v.2.0))

                        if (dados.EMITENTE_REGIMETRIBUTARIO == "1" && (csosn == "101" || csosn == "103" || csosn == "300" || csosn == "400"))
                        {
                            // Tributação do ICMS pelo SIMPLES NACIONAL e CSOSN=102, 103, 300 ou 400 (v.2.0))
                            xml.AppendLine("                        <ICMSSN102>");

                            // Origem da mercadoria:
                            // 0 = Nacional 
                            // 1 = Estrangeira - Importação direta
                            // 2 = Estrangeira - Adquirida no mercado interno
                            xml.AppendLine("                            <orig>" + origem + "</orig>");

                            // 102 - Tributada pelo Simples Nacional sem permissão de crédito. 
                            // 103 – Isenção do ICMS no Simples Nacional para faixa de receita bruta.
                            // 300 – Imune.
                            // 400 – Não tributda pelo Simples Nacional(v.2.0)(v.2.0)
                            xml.AppendLine("                            <CSOSN>" + csosn + "</CSOSN>");

                            xml.AppendLine("                        </ICMSSN102>");
                        }

                        #endregion

                        #region Tributação do ICMS pelo SIMPLES NACIONAL e CSOSN=201 (v.2.0)

                        if (dados.EMITENTE_REGIMETRIBUTARIO == "1" && csosn == "201")
                        {
                            // Tributação do ICMS pelo SIMPLES NACIONAL e CSOSN=201 (v.2.0)
                            xml.AppendLine("                        <ICMSSN201>");

                            // Origem da mercadoria:
                            // 0 = Nacional 
                            // 1 = Estrangeira - Importação direta
                            // 2 = Estrangeira - Adquirida no mercado interno
                            xml.AppendLine("                            <orig>" + origem + "</orig>");

                            // 201- Tributada pelo Simples Nacional com permissão de crédito e com cobrança do ICMS por Substituição Tributária (v.2.0)
                            xml.AppendLine("                            <CSOSN>" + csosn + "</CSOSN>");

                            // Modalidade de determinação da BC do ICMS ST:
                            // 0 – Preço tabelado ou máximo  sugerido;
                            // 1 - Lista Negativa(valor);
                            // 2 - Lista Positiva(valor);
                            // 3 - Lista Neutra(valor);
                            // 4 - Margem Valor Agregado (%);
                            // 5 - Pauta(valor). (v2.0)
                            xml.AppendLine("                            <modBCST>" + modalidadeICMSST + "</modBCST>");

                            // Percentual da Margem de Valor Adicionado ICMS ST (v2.0)
                            xml.AppendLine("                            <pMVAST>" + pcMVASTICMS + "</pMVAST>");

                            // Percentual de redução da BC ICMS ST  (v2.0)
                            xml.AppendLine("                            <pRedBCST>" + pcBCICMSSTReducao + "</pRedBCST>");

                            // Valor da BC do ICMS ST (v2.0)
                            xml.AppendLine("                            <vBCST>" + bcICMSST + "</vBCST>");

                            // Alíquota do ICMS ST (v2.0)
                            xml.AppendLine("                            <pICMSST>" + pcICMSST + "</pICMSST>");

                            // Valor do ICMS ST (v2.0)
                            xml.AppendLine("                            <vICMSST>" + vlICMSST + "</vICMSST>");

                            // Alíquota aplicável de cálculo do crédito (Simples Nacional). (v2.0)
                            xml.AppendLine("                            <pCredSN>" + pcCreditoICMS + "</pCredSN>");

                            // Valor crédito do ICMS que pode ser aproveitado nos termos do art. 23 da LC 123 (Simples Nacional) (v2.0)
                            xml.AppendLine("                            <vCredICMSSN>" + vlICMS + "</vCredICMSSN>");

                            xml.AppendLine("                        </ICMSSN201>");
                        }

                        #endregion

                        #region Tributação do ICMS pelo SIMPLES NACIONAL e CSOSN=202 ou 203 (v.2.0)

                        if (dados.EMITENTE_REGIMETRIBUTARIO == "1" && (csosn == "202" || csosn == "203"))
                        {
                            // Tributação do ICMS pelo SIMPLES NACIONAL e CSOSN=202 ou 203 (v.2.0)
                            xml.AppendLine("                        <ICMSSN202>");

                            // Origem da mercadoria:
                            // 0 = Nacional 
                            // 1 = Estrangeira - Importação direta
                            // 2 = Estrangeira - Adquirida no mercado interno
                            xml.AppendLine("                            <orig>" + origem + "</orig>");

                            // 202 - Tributada pelo Simples Nacional sem permissão de crédito e com cobrança do ICMS por Substituição Tributária;
                            // 203 - Isenção do ICMS nos Simples Nacional para faixa de receita bruta e com cobrança do ICMS por Substituição Tributária(v.2.0)
                            xml.AppendLine("                            <CSOSN>" + csosn + "</CSOSN>");

                            // Modalidade de determinação da BC do ICMS ST:
                            // 0 – Preço tabelado ou máximo  sugerido;
                            // 1 - Lista Negativa(valor);
                            // 2 - Lista Positiva(valor);
                            // 3 - Lista Neutra(valor);
                            // 4 - Margem Valor Agregado (%);
                            // 5 - Pauta(valor). (v2.0)
                            xml.AppendLine("                            <modBCST>" + modalidadeICMSST + "</modBCST>");

                            // Percentual da Margem de Valor Adicionado ICMS ST (v2.0)
                            xml.AppendLine("                            <pMVAST>" + pcMVASTICMS + "</pMVAST>");

                            // Percentual de redução da BC ICMS ST  (v2.0)
                            xml.AppendLine("                            <pRedBCST>" + pcBCICMSReducao + "</pRedBCST>");

                            // Valor da BC do ICMS ST (v2.0)
                            xml.AppendLine("                            <vBCST>" + bcICMSST + "</vBCST>");

                            // Alíquota do ICMS ST (v2.0)
                            xml.AppendLine("                            <pICMSST>" + pcICMSST + "</pICMSST>");

                            // Valor do ICMS ST (v2.0)
                            xml.AppendLine("                            <vICMSST>" + vlICMSST + "</vICMSST>");

                            xml.AppendLine("                        </ICMSSN202>");
                        }

                        #endregion

                        #region Tributação do ICMS pelo SIMPLES NACIONAL,CRT=1 – Simples Nacional e CSOSN=500 (v.2.0)

                        if (dados.EMITENTE_REGIMETRIBUTARIO == "1" && csosn == "500")
                        {
                            // Tributação do ICMS pelo SIMPLES NACIONAL,CRT=1 – Simples Nacional e CSOSN=500 (v.2.0)
                            xml.AppendLine("                        <ICMSSN500>");

                            // Origem da mercadoria:
                            // 0 = Nacional 
                            // 1 = Estrangeira - Importação direta
                            // 2 = Estrangeira - Adquirida no mercado interno
                            xml.AppendLine("                            <orig>" + origem + "</orig>");

                            // 500 – ICMS cobrado anterirmente por substituição tributária (substituído) ou por antecipação (v.2.0)
                            xml.AppendLine("                            <CSOSN>" + csosn + "</CSOSN>");

                            // Valor da BC do ICMS ST retido anteriormente (v2.0)
                            xml.AppendLine("                            <vBCSTRet>" + bcICMSSTRetido + "</vBCSTRet>");

                            // Valor do ICMS ST retido anteriormente  (v2.0)
                            xml.AppendLine("                            <vICMSSTRet>" + vlICMSSTRetido + "</vICMSSTRet>");

                            xml.AppendLine("                        </ICMSSN500>");
                        }
                        #endregion

                        #region Tributação do ICMS pelo SIMPLES NACIONAL, CRT=1 – Simples Nacional e CSOSN=900 (v2.0)

                        if (dados.EMITENTE_REGIMETRIBUTARIO == "1" && csosn == "500")
                        {
                            // Tributação do ICMS pelo SIMPLES NACIONAL, CRT=1 – Simples Nacional e CSOSN=900 (v2.0)
                            xml.AppendLine("                        <ICMSSN900>");

                            // Origem da mercadoria:
                            // 0 = Nacional 
                            // 1 = Estrangeira - Importação direta
                            // 2 = Estrangeira - Adquirida no mercado interno
                            xml.AppendLine("                            <orig>" + origem + "</orig>");

                            // Tributação pelo ICMS 900 - Outros(v2.0)
                            xml.AppendLine("                            <CSOSN>" + csosn + "</CSOSN>");

                            // Modalidade de determinação da BC do ICMS: 
                            // 0 - Margem Valor Agregado (%);
                            // 1 - Pauta(valor);
                            // 2 - Preço Tabelado Máximo (valor);
                            // 3 - Valor da Operação.
                            xml.AppendLine("                            <modBC>" + modalidadeICMS + "</modBC>");

                            // Valor da BC do ICMS
                            xml.AppendLine("                            <vBC>" + bcICMS + "</vBC>");

                            // Percentual de redução da BC
                            xml.AppendLine("                            <pRedBC>" + pcBCICMSReducao + "</pRedBC>");

                            // Alíquota do ICMS
                            xml.AppendLine("                            <pICMS>" + pcICMS + "</pICMS>");

                            // Valor do ICMS
                            xml.AppendLine("                            <vICMS>" + (((pcICMS.ToDecimal().Padrao() / 100) * bcICMS.ToDecimal().Padrao()) - ((pcBCICMSReducao.ToDecimal().Padrao() / 100) * bcICMS.ToDecimal().Padrao())) + "</vICMS>");

                            // Modalidade de determinação da BC do ICMS ST:
                            // 0 – Preço tabelado ou máximo  sugerido;
                            // 1 - Lista Negativa(valor);
                            // 2 - Lista Positiva(valor);
                            // 3 - Lista Neutra(valor);
                            // 4 - Margem Valor Agregado (%);
                            // 5 - Pauta(valor).
                            xml.AppendLine("                            <modBCST>" + modalidadeICMSST + "</modBCST>");

                            // Percentual da Margem de Valor Adicionado ICMS ST
                            xml.AppendLine("                            <pMVAST>" + pcMVASTICMS + "</pMVAST>");

                            // Percentual de redução da BC ICMS ST
                            xml.AppendLine("                            <pRedBCST>" + pcBCICMSSTReducao + "</pRedBCST>");

                            // Valor da BC do ICMS ST
                            xml.AppendLine("                            <vBCST>" + bcICMSST + "</vBCST>");

                            // Alíquota do ICMS ST
                            xml.AppendLine("                            <pICMSST>" + pcICMSST + "</pICMSST>");

                            // Valor do ICMS ST
                            var vICMSST = (((pcICMSST.ToDecimal().Padrao() / 100) * bcICMSST.ToDecimal().Padrao()) - ((pcBCICMSSTReducao.ToDecimal().Padrao() / 100) * bcICMSST.ToDecimal().Padrao()));
                            xml.AppendLine("                            <vICMSST>" + vICMSST.ENUS(10) + "</vICMSST>");

                            // Alíquota aplicável de cálculo do crédito (Simples Nacional). (v2.0)
                            xml.AppendLine("                            <pCredSN>" + pcCreditoICMS + "</pCredSN>");

                            // Valor crédito do ICMS que pode ser aproveitado nos termos do art. 23 da LC 123 (Simples Nacional) (v2.0)
                            xml.AppendLine("                            <vCredICMSSN>" + (vICMSST / pcCreditoICMS.ToDecimal().Padrao()).ENUS(10) + "</vCredICMSSN>");

                            xml.AppendLine("                        </ICMSSN900>");
                        }

                        #endregion

                        xml.AppendLine("                    </ICMS>");

                        #endregion

                        #region IPI

                        #region Bases de calculo

                        var bcIPI = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        var bcIPIST = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                 a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        var bcIPIDesonerado = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                         a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        var bcIPIDiferido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        var bcIPISTRetido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                     a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                     a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        var bcIPISTReducao = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                        a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        #endregion

                        #region Porcentagem de aliquotas

                        var pcIPI = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        var pcIPIST = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                 a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        var pcIPIDesonerado = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                         a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        var pcIPIDiferido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.TB_FIS_TRIBUTO.PC_DIFERIDO).Padrao().ENUS(5);

                        var pcIPISTRetido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                     a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                     a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        var pcIPIReducao = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                     !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                     !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                     !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                     !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);


                        var pcIPISTReducao = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                        a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        #endregion

                        #region Valores

                        temp = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao();
                        vlIPI_Total += temp;
                        var vlIPI = temp.ENUS(5);

                        var vlIPIST = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                 a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao().ENUS(5);

                        var vlIPIDesonerado = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                         a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao().ENUS(5);

                        var vlIPIDiferido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao().ENUS(5);

                        var vlIPISTRetido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao().ENUS(5);

                        var vlIPIReducao = (item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => (a.PC_ALIQUOTA.Padrao() / 100) * item.QT.Padrao() - (a.TB_FIS_TRIBUTO.PC_REDUCAO.Padrao() / 100) * item.QT.Padrao())).ENUS(5);

                        #endregion

                        // Modalidade do IPI.
                        var modalidadeIPI = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao()).FirstOrDefault().TB_FIS_TRIBUTO.ID_MODALIDADE.Padrao();

                        // Modalidade do IPI com substituição tributária.
                        var modalidadeIPIST = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao()).FirstOrDefault().TB_FIS_TRIBUTO.ID_MODALIDADE_SUBSTITUICAOTRIBUTARIA.Padrao();

                        var pcBCIPIReducao = ((pcIPIReducao.ToDecimal().Padrao() / 100) * vlIPI.ToDecimal().Padrao()).ENUS(10);
                        var pcBCIPISTReducao = ((pcIPISTReducao.ToDecimal().Padrao() / 100) * vlIPIST.ToDecimal().Padrao()).ENUS(10);
                        var pcMVASTIPI = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao()).Sum(a => a.TB_FIS_TRIBUTO.PC_MVA_SUBSTITUICAOTRIBUTARIA.Padrao()).ENUS(10);
                        var pcCreditoIPI = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao()).Sum(a => a.TB_FIS_TRIBUTO.PC_ALIQUOTA_CALCULOCREDITO.Padrao()).ENUS(10);
                        var desoneracaoIPI = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao()).FirstOrDefault().TB_FIS_TRIBUTO.ID_DESONERACAO;


                        // Dados do Imposto de Importação
                        xml.AppendLine("                    <IPI>");

                        // Classe de Enquadramento do IPI para Cigarros e Bebidas
                        // Minimo 1
                        // Maximo 5
                        //xml.AppendLine("                        <clEnq>" + "</clEnq>");

                        // CNPJ do produtor da mercadoria, quando diferente do emitente. Somente para os casos de exportação direta ou indireta.
                        //xml.AppendLine("                        <CNPJProd>" + "</CNPJProd>");

                        // Código do selo de controle do IPI
                        // Minimo 1
                        // Maximo 60
                        //xml.AppendLine("                        <cSelo>" + "</cSelo>");

                        // Quantidade de selo de controle do IPI
                        // [0-9]{1,12}
                        //xml.AppendLine("                        <qSelo>" + "</qSelo>");

                        // Código de Enquadramento Legal do IPI (tabela a ser criada pela RFB)
                        // Minimo 1
                        // Maximo 3
                        //xml.AppendLine("                        <cEnq>" + "</cEnq>");

                        if (cst == "00" || cst == "49" || cst == "50" || cst == "99")
                        {
                            xml.AppendLine("                        <IPITrib>");

                            // Código da Situação Tributária do IPI:
                            // 00 - Entrada com recuperação de crédito
                            // 49 - Outras entradas
                            // 50 - Saída tributada
                            // 99 - Outras saídas
                            xml.AppendLine("                            <CST>" + cst + "</CST>");

                            // Valor da BC do IPI
                            xml.AppendLine("                            <vBC>" + bcIPI + "</vBC>");

                            // Alíquota do IPI
                            xml.AppendLine("                            <pIPI>" + pcIPI + "</pIPI>");

                            // Quantidade total na unidade padrão para tributação
                            xml.AppendLine("                            <qUnid>" + item.QT.Padrao().ENUS(10) + "</qUnid>");

                            // Valor por Unidade Tributável. Informar o valor do imposto Pauta por unidade de medida.
                            xml.AppendLine("                            <vUnid>" + item.VL_UNITARIO.Padrao().ENUS(10) + "</vUnid>");

                            // Valor do IPI
                            xml.AppendLine("                            <vIPI>" + vlIPI + "</vIPI>");

                            xml.AppendLine("                        </IPITrib>");
                        }
                        else if (cst == "01" || cst == "02" || cst == "03" || cst == "04" || cst == "05" ||
                                 cst == "51" || cst == "52" || cst == "53" || cst == "54" || cst == "55")
                        {
                            xml.AppendLine("                        <IPINT>");

                            // Código da Situação Tributária do IPI:
                            // 01 - Entrada tributada com alíquota zero
                            // 02 - Entrada isenta
                            // 03 - Entrada não - tributada
                            // 04 - Entrada imune
                            // 05 - Entrada com suspensão
                            // 51 - Saída tributada com alíquota zero
                            // 52 - Saída isenta
                            // 53 - Saída não - tributada
                            // 54 - Saída imune
                            // 55 - Saída com suspensão
                            xml.AppendLine("                            <CST>" + cst + "</CST>");

                            xml.AppendLine("                        </IPINT>");
                        }

                        xml.AppendLine("                    </IPI>");

                        #endregion

                        #region II

                        #region Bases de calculo

                        var bcII = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        var bcIIST = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                 a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        var bcIIDesonerado = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                         a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        var bcIIDiferido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        var bcIISTRetido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                     a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                     a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        var bcIISTReducao = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                        a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        #endregion

                        #region Porcentagem de aliquotas

                        var pcII = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        var pcIIST = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                 a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        var pcIIDesonerado = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                         a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        var pcIIDiferido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.TB_FIS_TRIBUTO.PC_DIFERIDO).Padrao().ENUS(5);

                        var pcIISTRetido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                     a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                     a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        var pcIIReducao = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                     !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                     !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                     !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                     !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);


                        var pcIISTReducao = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                        a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        #endregion

                        #region Valores

                        temp = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao();
                        vlII_Total += temp;
                        var vlII = temp.ENUS(5);

                        var vlIIST = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                 a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao().ENUS(5);

                        var vlIIDesonerado = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                         a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao().ENUS(5);

                        var vlIIDiferido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao().ENUS(5);

                        var vlIISTRetido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao().ENUS(5);

                        var vlIIReducao = (item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => (a.PC_ALIQUOTA.Padrao() / 100) * item.QT.Padrao() - (a.TB_FIS_TRIBUTO.PC_REDUCAO.Padrao() / 100) * item.QT.Padrao())).ENUS(5);

                        #endregion

                        // Modalidade do II.
                        var modalidadeII = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao()).FirstOrDefault().TB_FIS_TRIBUTO.ID_MODALIDADE.Padrao();

                        // Modalidade do II com substituição tributária.
                        var modalidadeIIST = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao()).FirstOrDefault().TB_FIS_TRIBUTO.ID_MODALIDADE_SUBSTITUICAOTRIBUTARIA.Padrao();

                        var pcBCIIReducao = ((pcIIReducao.ToDecimal().Padrao() / 100) * vlII.ToDecimal().Padrao()).ENUS(10);
                        var pcBCIISTReducao = ((pcIISTReducao.ToDecimal().Padrao() / 100) * vlIIST.ToDecimal().Padrao()).ENUS(10);
                        var pcMVASTII = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao()).Sum(a => a.TB_FIS_TRIBUTO.PC_MVA_SUBSTITUICAOTRIBUTARIA.Padrao()).ENUS(10);
                        var pcCreditoII = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao()).Sum(a => a.TB_FIS_TRIBUTO.PC_ALIQUOTA_CALCULOCREDITO.Padrao()).ENUS(10);
                        var desoneracaoII = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I04.Padrao()).FirstOrDefault().TB_FIS_TRIBUTO.ID_DESONERACAO;



                        // Dados do Imposto de Importação
                        xml.AppendLine("                    <II>");

                        // Base da BC do Imposto de Importação
                        xml.AppendLine("                        <vBC>" + item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I01.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5) + "</vBC>");

                        // Valor das despesas aduaneiras
                        xml.AppendLine("                        <vDespAdu>" + "</vDespAdu>");

                        // Valor do Imposto de Importação
                        xml.AppendLine("                        <vII>" + item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I01.Padrao()).Sum(a => a.VL).Padrao().ENUS(5) + "</vII>");

                        // Valor do Imposto sobre Operações Financeiras
                        xml.AppendLine("                        <vIOF>" + item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I05.Padrao()).Sum(a => a.VL).Padrao().ENUS(5) + "</vIOF>");

                        xml.AppendLine("                    </II>");

                        #endregion

                        #region CSLL

                        vlCSLLRetido_Total += item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C08.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                         a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao();

                        #endregion

                        #region IRRF

                        #region CSLL

                        bcIRRFRetido_Total += item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I03.Padrao() &&
                                                                                         !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                         !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                         !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                          a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                         !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao();


                        vlIRRFRedito_Total += item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I03.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                         a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao();

                        #endregion

                        #endregion

                        #region ISSQN / ISS

                        #region Bases de calculo

                        temp = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao();
                        bcISS_Total += temp;
                        var bcISSQN = temp.ENUS(5);

                        var bcISSQNST = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao() &&
                                                                                 a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        var bcISSQNDesonerado = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                         a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        var bcISSQNDiferido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        var bcISSQNSTRetido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao() &&
                                                                                     a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                     a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        var bcISSQNSTReducao = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao() &&
                                                                                        a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        #endregion

                        #region Porcentagem de aliquotas

                        var pcISSQN = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        var pcISSQNST = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao() &&
                                                                                 a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        var pcISSQNDesonerado = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                         a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        var pcISSQNDiferido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.TB_FIS_TRIBUTO.PC_DIFERIDO).Padrao().ENUS(5);

                        var pcISSQNSTRetido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao() &&
                                                                                     a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                     a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        var pcISSQNReducao = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao() &&
                                                                                     !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                     !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                     !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                     !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);


                        var pcISSQNSTReducao = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao() &&
                                                                                        a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        #endregion

                        #region Valores

                        temp = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao() &&
                                                                               !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                               !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                               !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                               !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                               !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao();
                        vlISS_Total += temp;
                        var vlISSQN = temp.ENUS(5);

                        var vlISSQNST = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao() &&
                                                                                 a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao().ENUS(5);

                        var vlISSQNDesonerado = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                         a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao().ENUS(5);

                        var vlISSQNDiferido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao().ENUS(5);


                        temp = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao() &&
                                                                                         a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                         a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao();
                        vlISSRetencao_Total += temp;
                        var vlISSQNSTRetido = temp.ENUS(5);

                        var vlISSQNReducao = (item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => (a.PC_ALIQUOTA.Padrao() / 100) * item.QT.Padrao() - (a.TB_FIS_TRIBUTO.PC_REDUCAO.Padrao() / 100) * item.QT.Padrao())).ENUS(5);

                        #endregion

                        // Modalidade do ISSQN.
                        var modalidadeISSQN = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao()).FirstOrDefault().TB_FIS_TRIBUTO.ID_MODALIDADE.Padrao();

                        // Modalidade do ISSQN com substituição tributária.
                        var modalidadeISSQNST = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao()).FirstOrDefault().TB_FIS_TRIBUTO.ID_MODALIDADE_SUBSTITUICAOTRIBUTARIA.Padrao();

                        var pcBCISSQNReducao = ((pcISSQNReducao.ToDecimal().Padrao() / 100) * vlISSQN.ToDecimal().Padrao()).ENUS(10);
                        var pcBCISSQNSTReducao = ((pcISSQNSTReducao.ToDecimal().Padrao() / 100) * vlISSQNST.ToDecimal().Padrao()).ENUS(10);
                        var pcMVASTISSQN = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao()).Sum(a => a.TB_FIS_TRIBUTO.PC_MVA_SUBSTITUICAOTRIBUTARIA.Padrao()).ENUS(10);
                        var pcCreditoISSQN = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao()).Sum(a => a.TB_FIS_TRIBUTO.PC_ALIQUOTA_CALCULOCREDITO.Padrao()).ENUS(10);
                        var desoneracaoISSQN = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao()).FirstOrDefault().TB_FIS_TRIBUTO.ID_DESONERACAO;

                        if (item.TB_EST_PRODUTO.ST_SERVICO.Padrao())
                        {
                            // ISSQN
                            xml.AppendLine("                    <ISSQN>");

                            // Valor da BC do ISSQN
                            xml.AppendLine("                        <vBC>" + item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5) + "</vBC>");

                            // Alíquota do ISSQN
                            xml.AppendLine("                        <vAliq>" + item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5) + "</vAliq>");

                            // Valor da do ISSQN
                            xml.AppendLine("                        <vISSQN>" + item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_IMPOSTO.I13.Padrao()).Sum(a => a.VL).Padrao().ENUS(5) + "</vISSQN>");

                            // Informar o município de ocorrência do fato gerador do ISSQN. Utilizar a Tabela do IBGE (Anexo VII - Tabela de UF, Município e País). “Atenção, não vincular com os campos B12, C10 ou E10” v2.0
                            xml.AppendLine("                        <cMunFG>" + dados.EMITENTE_ENDERECO_CIDADE + "</cMunFG>");

                            // Informar o Item da lista de serviços da LC 116/03 em que se classifica o serviço.
                            //xml.AppendLine("                        <cListServ>" + "</cListServ>");

                            // Valor dedução para redução da base de cálculo
                            // vlDeducaoReducaoBC +=temp;
                            //xml.AppendLine("                        <vDeducao>" + "</vDeducao>");

                            // Valor outras retenções
                            // vlOutrasRetencoes+=temp;
                            //xml.AppendLine("                        <vOutro>" + "</vOutro>");

                            // Valor desconto incondicionado
                            // vlDescontoIncondicionado +=temp;
                            //xml.AppendLine("                        <vDescIncond>" + "</vDescIncond>");

                            // Valor desconto condicionado
                            // vlDescontoCondicionado +=temp;
                            //xml.AppendLine("                        <vDescCond>" + "</vDescCond>");

                            // Valor Retenção ISS
                            xml.AppendLine("                        <vISSRet>" + vlISSRetencao_Total.ENUS(10) + "</vISSRet>");

                            // Exibilidade do ISS:
                            // 1 -Exigível;
                            // 2 -Não incidente;
                            // 3 -Isenção;
                            // 4 -Exportação;
                            // 5 -Imunidade;
                            // 6 -Exig.Susp. Judicial;
                            // 7 -Exig.Susp. ADM
                            //xml.AppendLine("                        <indISS>" + "</indISS>");

                            // Código do serviço prestado dentro do município
                            // Minimo 1
                            // Maximo 20
                            //xml.AppendLine("                        <cServico>" + "</cServico>");

                            // Código do Município de Incidência do Imposto
                            //xml.AppendLine("                        <cMun>" + "</cMun>");

                            // Código do país onde o serviço foi prestado
                            //xml.AppendLine("                        <cPais>" + "</cPais>");

                            // Número do Processo administrativo ou judicial de suspenção do processo
                            // Minimo 1
                            // Maximo 30
                            //xml.AppendLine("                        <nProcesso>" + "</nProcesso>");

                            // Indicador de Incentivo Fiscal.
                            // 1 =Sim;
                            // 2 =Não
                            //xml.AppendLine("                        <indIncentivo>" + "</indIncentivo>");


                            xml.AppendLine("                    </ISSQN>");
                        }

                        #endregion

                        #region PIS

                        #region Bases de calculo

                        var bcPIS = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C06.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        var bcPISST = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C06.Padrao() &&
                                                                                 a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        var bcPISDesonerado = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C06.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                         a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        var bcPISDiferido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C06.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        var bcPISSTRetido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C06.Padrao() &&
                                                                                     a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                     a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        var bcPISSTReducao = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C06.Padrao() &&
                                                                                        a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        #endregion

                        #region Porcentagem de aliquotas

                        var pcPIS = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C06.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        var pcPISST = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C06.Padrao() &&
                                                                                 a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        var pcPISDesonerado = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C06.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                         a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        var pcPISDiferido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C06.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.TB_FIS_TRIBUTO.PC_DIFERIDO).Padrao().ENUS(5);

                        var pcPISSTRetido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C06.Padrao() &&
                                                                                     a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                     a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        var pcPISReducao = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C06.Padrao() &&
                                                                                     !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                     !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                     !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                     !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);


                        var pcPISSTReducao = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C06.Padrao() &&
                                                                                        a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        #endregion

                        #region Valores

                        temp = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C06.Padrao() &&
                                                                               !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                               !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                               !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                               !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                               !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao();
                        vlPIS_Total += temp;
                        if (item.TB_EST_PRODUTO.ST_SERVICO.Padrao())
                            vlPISSS_Total += temp;

                        var vlPIS = temp.ENUS(5);

                        var vlPISST = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C06.Padrao() &&
                                                                                 a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao().ENUS(5);

                        var vlPISDesonerado = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C06.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                         a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao().ENUS(5);

                        var vlPISDiferido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C06.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao().ENUS(5);

                        temp = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C06.Padrao() &&
                                                                                         a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                         a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao();
                        vlPISRetido_Total += temp;
                        var vlPISSTRetido = temp.ENUS(5);

                        var vlPISReducao = (item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C06.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => (a.PC_ALIQUOTA.Padrao() / 100) * item.QT.Padrao() - (a.TB_FIS_TRIBUTO.PC_REDUCAO.Padrao() / 100) * item.QT.Padrao())).ENUS(5);

                        #endregion

                        // Modalidade do PIS.
                        var modalidadePIS = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C06.Padrao()).FirstOrDefault().TB_FIS_TRIBUTO.ID_MODALIDADE.Padrao();

                        // Modalidade do PIS com substituição tributária.
                        var modalidadePISST = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C06.Padrao()).FirstOrDefault().TB_FIS_TRIBUTO.ID_MODALIDADE_SUBSTITUICAOTRIBUTARIA.Padrao();

                        var pcBCPISReducao = ((pcPISReducao.ToDecimal().Padrao() / 100) * vlPIS.ToDecimal().Padrao()).ENUS(10);
                        var pcBCPISSTReducao = ((pcPISSTReducao.ToDecimal().Padrao() / 100) * vlPISST.ToDecimal().Padrao()).ENUS(10);
                        var pcMVASTPIS = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C06.Padrao()).Sum(a => a.TB_FIS_TRIBUTO.PC_MVA_SUBSTITUICAOTRIBUTARIA.Padrao()).ENUS(10);
                        var pcCreditoPIS = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C06.Padrao()).Sum(a => a.TB_FIS_TRIBUTO.PC_ALIQUOTA_CALCULOCREDITO.Padrao()).ENUS(10);
                        var desoneracaoPIS = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C06.Padrao()).FirstOrDefault().TB_FIS_TRIBUTO.ID_DESONERACAO;

                        // PIS
                        xml.AppendLine("                    <PIS>");

                        #region Aliquota

                        if (cst == "01" || cst == "02")
                        {
                            xml.AppendLine("                        <PISAliq>");

                            //Código de Situação Tributária do PIS.
                            //01 – Operação Tributável -Base de Cálculo = Valor da Operação Alíquota Normal (Cumulativo / Não Cumulativo);
                            //02 - Operação Tributável - Base de Calculo = Valor da Operação (Alíquota Diferenciada);
                            xml.AppendLine("                            <CST>" + cst + "</CST>");

                            // Valor da BC do PIS
                            xml.AppendLine("                            <vBC>" + bcPIS + "</vBC>");

                            // Alíquota do PIS (em percentual)
                            xml.AppendLine("                            <pPIS>" + pcPIS + "</pPIS>");

                            // Valor do PIS
                            xml.AppendLine("                            <vPIS>" + vlPIS + "</vPIS>");

                            xml.AppendLine("                        </PISAliq>");
                        }

                        #endregion

                        #region Quantidade

                        if (cst == "03")
                        {
                            xml.AppendLine("                        <PISQtde>");

                            // Código de Situação Tributária do PIS.
                            // 03 - Operação Tributável - Base de Calculo = Quantidade Vendida x Alíquota por Unidade de Produto;
                            xml.AppendLine("                            <CST>" + cst + "</CST>");

                            // Quantidade Vendida  (NT2011/004)
                            xml.AppendLine("                            <qBCProd>" + item.QT.Padrao().ENUS(10) + "</qBCProd>");

                            // Alíquota do PIS (em reais) (NT2011/004)
                            xml.AppendLine("                            <vAliqProd>" + (item.QT.Padrao() * pcIPI.ToDecimal().Padrao()).ENUS(10) + "</vAliqProd>");

                            // Valor do PIS
                            xml.AppendLine("                            <vPIS>" + vlPIS + "</vPIS>");

                            xml.AppendLine("                        </PISQtde>");
                        }

                        #endregion

                        #region Natureza Tributavel

                        if (cst == "04" || cst == "06" || cst == "07" || cst == "08" || cst == "09")
                        {
                            xml.AppendLine("                        <PISNT>");

                            // Código de Situação Tributária do PIS.
                            // 04 - Operação Tributável - Tributação Monofásica - (Alíquota Zero);
                            // 06 - Operação Tributável - Alíquota Zero;
                            // 07 - Operação Isenta da contribuição;
                            // 08 - Operação Sem Incidência da contribuição;
                            // 09 - Operação com suspensão da contribuição;
                            xml.AppendLine("                            <CST>" + cst + "</CST>");

                            xml.AppendLine("                        </PISNT>");
                        }

                        #endregion

                        #region Outros

                        if (dados.EMITENTE_REGIMETRIBUTARIO == "1" && (cst == "49" || cst == "50" || cst == "51" || cst == "52" || cst == "53" || cst == "54" || cst == "55" || cst == "56" || cst == "60" || cst == "61" || cst == "62" || cst == "63" || cst == "64" || cst == "65" || cst == "66" || cst == "67" || cst == "70" || cst == "71" || cst == "72" || cst == "73" || cst == "74" || cst == "75" || cst == "98" || cst == "99"))
                        {
                            xml.AppendLine("                        <PISOutr>");

                            // Código de Situação Tributária do PIS.
                            //49
                            //50
                            //51
                            //52
                            //53
                            //54
                            //55
                            //56
                            //60
                            //61
                            //62
                            //63
                            //64
                            //65
                            //66
                            //67
                            //70
                            //71
                            //72
                            //73
                            //74
                            //75
                            //98
                            //99
                            xml.AppendLine("                            <CST>" + cst + "</CST>");

                            // Valor da BC do PIS
                            xml.AppendLine("                            <vBC>" + bcPIS + "</vBC>");

                            // Alíquota do PIS (em percentual)
                            xml.AppendLine("                            <pPIS>" + pcPIS + "</pPIS>");

                            // Quantidade Vendida (NT2011/004) 
                            //xml.AppendLine("                            <qBCProd>" + "</qBCProd>");

                            // Alíquota do PIS (em reais) (NT2011/004)
                            //xml.AppendLine("                            <vAliqProd>" + "</vAliqProd>");

                            // Valor do PIS
                            xml.AppendLine("                            <vPIS>" + vlPIS + "</vPIS>");

                            xml.AppendLine("                        </PISOutr>");
                        }

                        #endregion

                        xml.AppendLine("                    </PIS>");

                        #endregion

                        #region PIS Substituição Tributária


                        // PIS Substituição Tributária
                        xml.AppendLine("                    <PISST>");

                        // Valor da BC do PIS ST
                        xml.AppendLine("                        <vBC>" + "</vBC>");

                        // Alíquota do PIS ST (em percentual)
                        //xml.AppendLine("                        <pPIS>" + "</pPIS>");

                        // Quantidade Vendida
                        //xml.AppendLine("                        <qBCProd>" + "</qBCProd>");

                        // Alíquota do PIS ST (em reais)
                        //xml.AppendLine("                        <vAliqProd>" + "</vAliqProd>");

                        // Valor do PIS ST
                        //xml.AppendLine("                        <vPIS>" + "</vPIS>");

                        xml.AppendLine("                    </PISST>");


                        #endregion

                        #region COFINS

                        #region Bases de calculo

                        var bcCOFINS = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C07.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        var bcCOFINSST = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C07.Padrao() &&
                                                                                 a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        var bcCOFINSDesonerado = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C07.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                         a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        var bcCOFINSDiferido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C07.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        var bcCOFINSSTRetido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C07.Padrao() &&
                                                                                     a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                     a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        var bcCOFINSSTReducao = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C07.Padrao() &&
                                                                                        a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL_BASECALCULO).Padrao().ENUS(5);

                        #endregion

                        #region Porcentagem de aliquotas

                        var pcCOFINS = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C07.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                              !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        var pcCOFINSST = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C07.Padrao() &&
                                                                                 a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        var pcCOFINSDesonerado = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C07.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                         a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        var pcCOFINSDiferido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C07.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.TB_FIS_TRIBUTO.PC_DIFERIDO).Padrao().ENUS(5);

                        var pcCOFINSSTRetido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C07.Padrao() &&
                                                                                     a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                     a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                    !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        var pcCOFINSReducao = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C07.Padrao() &&
                                                                                     !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                     !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                     !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                     !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);


                        var pcCOFINSSTReducao = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C07.Padrao() &&
                                                                                        a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                       !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.PC_ALIQUOTA).Padrao().ENUS(5);

                        #endregion

                        #region Valores

                        temp = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C07.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao();
                        vlCOFINS_Total += temp;
                        if (item.TB_EST_PRODUTO.ST_SERVICO.Padrao())
                            vlCOFINSSS_Total += temp;
                        var vlCOFINS = temp.ENUS(5);

                        var vlCOFINSST = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C07.Padrao() &&
                                                                                 a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao().ENUS(5);

                        var vlCOFINSDesonerado = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C07.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                         a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                        !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao().ENUS(5);

                        var vlCOFINSDiferido = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C07.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao().ENUS(5);

                        temp = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C07.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => a.VL).Padrao();
                        vlCOFINSRetido_Total += temp;
                        var vlCOFINSRetido = temp.ENUS(5);

                        var vlCOFINSReducao = (item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C07.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_SUBSTITUICAOTRIBUTARIA.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DESONERADO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_DIFERIDO.Padrao() &&
                                                                                      !a.TB_FIS_TRIBUTO.ST_RETIDO.Padrao() &&
                                                                                       a.TB_FIS_TRIBUTO.ST_REDUCAO.Padrao()).Sum(a => (a.PC_ALIQUOTA.Padrao() / 100) * item.QT.Padrao() - (a.TB_FIS_TRIBUTO.PC_REDUCAO.Padrao() / 100) * item.QT.Padrao())).ENUS(5);

                        #endregion

                        // Modalidade do COFINS.
                        var modalidadeCOFINS = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C07.Padrao()).FirstOrDefault().TB_FIS_TRIBUTO.ID_MODALIDADE.Padrao();

                        // Modalidade do COFINS com substituição tributária.
                        var modalidadeCOFINSST = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C07.Padrao()).FirstOrDefault().TB_FIS_TRIBUTO.ID_MODALIDADE_SUBSTITUICAOTRIBUTARIA.Padrao();

                        var pcBCCOFINSReducao = ((pcCOFINSReducao.ToDecimal().Padrao() / 100) * vlCOFINS.ToDecimal().Padrao()).ENUS(10);
                        var pcBCCOFINSSTReducao = ((pcCOFINSSTReducao.ToDecimal().Padrao() / 100) * vlCOFINSST.ToDecimal().Padrao()).ENUS(10);
                        var pcMVASTCOFINS = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C07.Padrao()).Sum(a => a.TB_FIS_TRIBUTO.PC_MVA_SUBSTITUICAOTRIBUTARIA.Padrao()).ENUS(10);
                        var pcCreditoCOFINS = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C07.Padrao()).Sum(a => a.TB_FIS_TRIBUTO.PC_ALIQUOTA_CALCULOCREDITO.Padrao()).ENUS(10);
                        var desoneracaoCOFINS = item.TB_FAT_NOTA_ITEM_TRIBUTOs.Where(a => a.TB_FIS_TRIBUTO.TB_FIS_CONTRIBUICAO.C07.Padrao()).FirstOrDefault().TB_FIS_TRIBUTO.ID_DESONERACAO;

                        // COFINS
                        xml.AppendLine("                    <COFINS>");

                        #region Aliquota

                        if (cst == "01" || cst == "02")
                        {
                            xml.AppendLine("                        <COFINSAliq>");

                            //Código de Situação Tributária do COFINS.
                            //01 – Operação Tributável -Base de Cálculo = Valor da Operação Alíquota Normal (Cumulativo / Não Cumulativo);
                            //02 - Operação Tributável - Base de Calculo = Valor da Operação (Alíquota Diferenciada);
                            xml.AppendLine("                            <CST>" + cst + "</CST>");

                            // Valor da BC do COFINS
                            xml.AppendLine("                            <vBC>" + bcCOFINS + "</vBC>");

                            // Alíquota do COFINS (em percentual)
                            xml.AppendLine("                            <pCOFINS>" + pcCOFINS + "</pCOFINS>");

                            // Valor do COFINS
                            xml.AppendLine("                            <vCOFINS>" + vlCOFINS + "</vCOFINS>");

                            xml.AppendLine("                        </COFINSAliq>");
                        }

                        #endregion

                        #region Quantidade

                        if (cst == "03")
                        {
                            xml.AppendLine("                        <COFINSQtde>");

                            // Código de Situação Tributária do COFINS.
                            // 03 - Operação Tributável - Base de Calculo = Quantidade Vendida x Alíquota por Unidade de Produto;
                            xml.AppendLine("                            <CST>" + cst + "</CST>");

                            // Quantidade Vendida  (NT2011/004)
                            xml.AppendLine("                            <qBCProd>" + item.QT.Padrao().ENUS(10) + "</qBCProd>");

                            // Alíquota do COFINS (em reais) (NT2011/004)
                            xml.AppendLine("                            <vAliqProd>" + pcCOFINS + "</vAliqProd>");

                            // Valor do COFINS
                            xml.AppendLine("                            <vCOFINS>" + vlCOFINS + "</vCOFINS>");

                            xml.AppendLine("                        </COFINSQtde>");
                        }

                        #endregion

                        #region Natureza Tributavel

                        if (cst == "04" || cst == "05" || cst == "06" || cst == "07" || cst == "08" || cst == "09")
                        {
                            xml.AppendLine("                        <COFINSNT>");

                            // Código de Situação Tributária do COFINS:
                            // 04 - Operação Tributável - Tributação Monofásica - (Alíquota Zero);
                            // 05 - Operação Tributável(ST);
                            // 06 - Operação Tributável - Alíquota Zero;
                            // 07 - Operação Isenta da contribuição;
                            // 08 - Operação Sem Incidência da contribuição;
                            // 09 - Operação com suspensão da contribuição;
                            xml.AppendLine("                            <CST>" + cst + "</CST>");

                            xml.AppendLine("                        </COFINSNT>");
                        }

                        #endregion

                        #region Outros

                        if (dados.EMITENTE_REGIMETRIBUTARIO == "1" &&
                           (cst == "49" || cst == "50" || cst == "51" || cst == "52" ||
                           cst == "53" || cst == "54" || cst == "55" || cst == "56" ||
                           cst == "60" || cst == "61" || cst == "62" || cst == "63" ||
                           cst == "64" || cst == "65" || cst == "66" || cst == "67" ||
                           cst == "70" || cst == "71" || cst == "72" || cst == "73" ||
                           cst == "74" || cst == "75" || cst == "98" || cst == "99"))
                        {
                            xml.AppendLine("                        <COFINSOutr>");

                            // Código de Situação Tributária do COFINS:
                            // 49 - Outras Operações de Saída
                            // 50 - Operação com Direito a Crédito -Vinculada Exclusivamente a Receita Tributada no Mercado Interno
                            // 51 - Operação com Direito a Crédito – Vinculada Exclusivamente a Receita Não Tributada no Mercado Interno
                            // 52 - Operação com Direito a Crédito -Vinculada Exclusivamente a Receita de Exportação
                            // 53 - Operação com Direito a Crédito -Vinculada a Receitas Tributadas e Não - Tributadas no Mercado Interno
                            // 54 - Operação com Direito a Crédito -Vinculada a Receitas Tributadas no Mercado Interno e de Exportação
                            // 55 - Operação com Direito a Crédito -Vinculada a Receitas Não - Tributadas no Mercado Interno e de Exportação
                            // 56 - Operação com Direito a Crédito -Vinculada a Receitas Tributadas e Não - Tributadas no Mercado Interno, e de Exportação
                            // 60 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Tributada no Mercado Interno
                            // 61 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Não - Tributada no Mercado Interno
                            // 62 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita de Exportação
                            // 63 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno
                            // 64 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas no Mercado Interno e de Exportação
                            // 65 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Não-Tributadas no Mercado Interno e de Exportação
                            // 66 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno, e de Exportação
                            // 67 - Crédito Presumido - Outras Operações
                            // 70 - Operação de Aquisição sem Direito a Crédito
                            // 71 - Operação de Aquisição com Isenção
                            // 72 - Operação de Aquisição com Suspensão
                            // 73 - Operação de Aquisição a Alíquota Zero
                            // 74 - Operação de Aquisição sem Incidência da Contribuição
                            // 75 - Operação de Aquisição por Substituição Tributária
                            // 98 - Outras Operações de Entrada
                            // 99 - Outras Operações.
                            xml.AppendLine("                            <CST>" + cst + "</CST>");

                            // Valor da BC do COFINS
                            xml.AppendLine("                            <vBC>" + bcCOFINS + "</vBC>");

                            // Alíquota do COFINS (em percentual)
                            xml.AppendLine("                            <pCOFINS>" + pcCOFINS + "</pCOFINS>");

                            // Quantidade Vendida (NT2011/004) 
                            xml.AppendLine("                            <qBCProd>" + item.QT.Padrao().ENUS(10) + "</qBCProd>");

                            // Alíquota do COFINS (em reais) (NT2011/004)
                            xml.AppendLine("                            <vAliqProd>" + pcCOFINS + "</vAliqProd>");

                            // Valor do COFINS
                            xml.AppendLine("                            <vCOFINS>" + vlCOFINS + "</vCOFINS>");

                            xml.AppendLine("                        </COFINSOutr>");
                        }

                        #endregion

                        xml.AppendLine("                    </COFINS>");

                        #endregion

                        #region COFINS Substituição Tributária

                        // PIS Substituição Tributária
                        xml.AppendLine("                    <COFINSST>");

                        // Valor da BC do COFINS ST
                        xml.AppendLine("                        <vBC>" + bcCOFINSST + "</vBC>");

                        // Alíquota do COFINS ST(em percentual)
                        xml.AppendLine("                        <pCOFINS>" + pcCOFINSST + "</pCOFINS>");

                        // Quantidade Vendida
                        xml.AppendLine("                        <qBCProd>" + item.QT.Padrao().ENUS(10) + "</qBCProd>");

                        // Alíquota do COFINS ST(em reais)
                        xml.AppendLine("                        <vAliqProd>" + pcCOFINSST + "</vAliqProd>");

                        // Valor do COFINS ST
                        xml.AppendLine("                        <vCOFINS>" + vlCOFINS + "</vCOFINS>");

                        xml.AppendLine("                    </COFINSST>");

                        #endregion

                        xml.AppendLine("                </imposto>");

                        #endregion

                        #region Devolução de imposto

                        //xml.AppendLine("                <impostoDevol>");

                        // Percentual de mercadoria devolvida
                        //xml.AppendLine("                    <pDevol>" + "</pDevol>");

                        #region IPI

                        // IPI
                        //xml.AppendLine("                    <IPI>");

                        // Valor do IPI devolvido
                        //xml.AppendLine("                        <vIPIDevol>" + "</vIPIDevol>");

                        //xml.AppendLine("                    </IPI>");

                        #endregion

                        //xml.AppendLine("                </impostoDevol>");

                        #endregion

                        // Informações adicionais do produto (norma referenciada, informações complementares, etc)
                        // Minmo 1
                        // Maximo 500
                        xml.AppendLine("                <infAdProd>" + item.DS_OBSERVACAO.Validar(true, 500) + "</infAdProd>");

                        xml.AppendLine("            </det>");
                    }

                    #endregion

                    #region Dados dos totais da NF-e

                    // Dados dos totais da NF-e
                    xml.AppendLine("            <total>");

                    #region Totais referentes ao ICMS

                    // Totais referentes ao ICMS
                    xml.AppendLine("                <ICMSTot>");

                    // BC do ICMS
                    xml.AppendLine("                    <vBC>" + bcICMS_Total.ENUS(10) + "</vBC>");

                    // Valor Total do ICMS
                    xml.AppendLine("                    <vICMS>" + vlICMS_Total.ENUS(10) + "</vICMS>");

                    // Valor Total do ICMS desonerado
                    xml.AppendLine("                    <vICMSDeson>" + vlICMSDesonerado_Total.ENUS(10) + "</vICMSDeson>");

                    // BC do ICMS ST
                    xml.AppendLine("                    <vBCST>" + bcICMSST_Total.ENUS(10) + "</vBCST>");

                    // Valor Total do ICMS ST
                    xml.AppendLine("                    <vST>" + vlICMSST_Total.ENUS(10) + "</vST>");

                    // Valor Total dos produtos e serviços
                    xml.AppendLine("                    <vProd>" + vlItem_Total.ENUS(10) + "</vProd>");

                    // Valor Total do Frete
                    xml.AppendLine("                    <vFrete>" + vlFrete_Total.ENUS(10) + "</vFrete>");

                    // Valor Total do Seguro
                    xml.AppendLine("                    <vSeg>" + vlSeguro_Total.ENUS(10) + "</vSeg>");

                    // Valor Total do Desconto
                    xml.AppendLine("                    <vDesc>" + vlDesconto_Total.ENUS(10) + "</vDesc>");

                    // Valor Total do II
                    xml.AppendLine("                    <vII>" + vlII_Total.ENUS(10) + "</vII>");

                    // Valor Total do IPI
                    xml.AppendLine("                    <vIPI>" + vlIPI_Total.ENUS(10) + "</vIPI>");

                    // Valor do PIS
                    xml.AppendLine("                    <vPIS>" + vlPIS_Total.ENUS(10) + "</vPIS>");

                    // Valor do COFINS
                    xml.AppendLine("                    <vCOFINS>" + vlCOFINS_Total.ENUS(10) + "</vCOFINS>");

                    // Outras Despesas acessórias
                    xml.AppendLine("                    <vOutro>" + vlAcrescimo_Total.ENUS(10) + "</vOutro>");

                    // Valor Total da NF-e
                    xml.AppendLine("                    <vNF>" + vlSubtotal_Total.ENUS(10) + "</vNF>");

                    // Valor estimado total de impostos federais, estaduais e municipais
                    xml.AppendLine("                    <vTotTrib>" + vlEstimadoImpostos.ENUS(10) + "</vTotTrib>");

                    xml.AppendLine("                </ICMSTot>");

                    #endregion

                    #region Totais referentes ao ISSQN

                    // Totais referentes ao ISSQN
                    xml.AppendLine("                <ISSQNtot>");

                    // Valor Total dos Serviços sob não-incidência ou não tributados pelo ICMS 
                    xml.AppendLine("                    <vServ>" + vlICMSDesonerado_Total.ENUS(10) + "</vServ>");

                    // Base de Cálculo do ISS
                    xml.AppendLine("                    <vBC>" + bcISS_Total.ENUS(10) + "</vBC>");

                    // Valor Total do ISS
                    xml.AppendLine("                    <vISS>" + vlISS_Total.ENUS(10) + "</vISS>");

                    // Valor do PIS sobre serviços
                    xml.AppendLine("                    <vPIS>" + vlPISSS_Total.ENUS(10) + "</vPIS>");

                    // Valor do COFINS sobre serviços
                    xml.AppendLine("                    <vCOFINS>" + vlCOFINSSS_Total.ENUS(10) + "</vCOFINS>");

                    // Data da prestação do serviço  (AAAA-MM-DD)
                    xml.AppendLine("                    <dCompet>" + dtPrestacaoServico.Padrao().ToString("YYYY-MM-DD") + "</dCompet>");

                    // Valor dedução para redução da base de cálculo
                    xml.AppendLine("                    <vDeducao>" + vlDeducaoReducaoBC_Total.ENUS(10) + "</vDeducao>");

                    // Valor outras retenções
                    xml.AppendLine("                    <vOutro>" + vlOutrasRetencoes_Total.ENUS(10) + "</vOutro>");

                    // Valor desconto incondicionado
                    xml.AppendLine("                    <vDescIncond>" + vlDescontoIncondicionado_Total.ENUS(10) + "</vDescIncond>");

                    // Valor desconto condicionado
                    xml.AppendLine("                    <vDescCond>" + vlDescontoCondicionado_Total.ENUS(10) + "</vDescCond>");

                    // Valor Total Retenção ISS
                    xml.AppendLine("                    <vISSRet>" + vlISSRetencao_Total.ENUS(10) + "</vISSRet>");

                    // Código do regime especial de tributação
                    // 1
                    // 2
                    // 3
                    // 4
                    // 5
                    // 6
                    xml.AppendLine("                    <cRegTrib>" + dados.EMITENTE_REGIMETRIBUTARIOESPECIAL + "</cRegTrib>");

                    xml.AppendLine("                </ISSQNtot>");

                    #endregion

                    #region Retenção de Tributos Federais

                    // Retenção de Tributos Federais
                    xml.AppendLine("                <retTrib>");

                    // Valor Retido de PIS
                    xml.AppendLine("                    <vRetPIS>" + vlPISRetido_Total.ENUS(10) + "</vRetPIS>");

                    // Valor Retido de COFINS
                    xml.AppendLine("                    <vRetCOFINS>" + vlCOFINSRetido_Total.ENUS(10) + "</vRetCOFINS>");

                    // Valor Retido de CSLL
                    xml.AppendLine("                    <vRetCSLL>" + vlCSLLRetido_Total.ENUS(10) + "</vRetCSLL>");

                    // Base de Cálculo do IRRF
                    xml.AppendLine("                    <vBCIRRF>" + bcIRRFRetido_Total.ENUS(10) + "</vBCIRRF>");

                    // Valor Retido de IRRF
                    xml.AppendLine("                    <vIRRF>" + vlIRRFRedito_Total.ENUS(10) + "</vIRRF>");

                    // Base de Cálculo da Retenção da Previdêncica Social
                    xml.AppendLine("                    <vBCRetPrev>" + bcPrevidenciaSocialRetido_Total.ENUS(10) + "</vBCRetPrev>");

                    // Valor da Retenção da Previdêncica Social
                    xml.AppendLine("                    <vRetPrev>" + vlPrevidenciaSocialRetido_Total.ENUS(10) + "</vRetPrev>");

                    xml.AppendLine("                </retTrib>");

                    #endregion

                    xml.AppendLine("            </total>");

                    #endregion

                    #region Dados dos transportes da NF-e

                    // Dados dos transportes da NF-e
                    xml.AppendLine("            <transp>");

                    // Modalidade do frete
                    // 0 - Por conta do emitente;
                    // 1 - Por conta do destinatário / remetente;
                    // 2 - Por conta de terceiros;
                    // 9 - Sem frete(v2.0)
                    xml.AppendLine("                <modFrete>" + dados.CONFIGURACAO_MODALIDADEFRETE + "</modFrete>");

                    if (dados.CONFIGURACAO_MODALIDADEFRETE != "9")
                    {
                        #region Dados do transportador

                        // Dados do transportador
                        xml.AppendLine("                <transporta>");

                        if (dados.TRANSPORTADORA_CNPJ != null)
                            // CNPJ do transportador
                            xml.AppendLine("                    <CNPJ>" + dados.TRANSPORTADORA_CNPJ + "</CNPJ>");
                        else
                            // CPF do transportador
                            xml.AppendLine("                    <CPF>" + dados.TRANSPORTADORA_CPF + "</CPF>");

                        // Razão Social ou nome do transportador
                        // Minimo 2
                        // Maximo 60
                        xml.AppendLine("                    <xNome>" + dados.TRANSPORTADORA_NOME.Validar(true, 60) + "</xNome>");

                        // Inscrição Estadual (v2.0)
                        // aceitar vazio ou ISENTO
                        // ISENTO|[0-9]{2,14}
                        xml.AppendLine("                    <IE>" + dados.TRANSPORTADORA_IE + "</IE>");

                        #region Endereço completo

                        // Endereço completo
                        xml.AppendLine("                    <xEnder>");

                        // Logradouro
                        // Minimo 2
                        // Maximo 60
                        xml.AppendLine("                        <xLg>" + dados.TRANSPORTADORA_ENDERECO_NMRUA.Validar(true, 60) + "</xLg>");

                        // Número
                        // Minimo 1
                        // Maximo 60
                        xml.AppendLine("                        <nro>" + dados.TRANSPORTADORA_ENDERECO_NR.Validar(true, 60) + "</nro>");

                        // Complemento
                        // Minimo 1
                        // Maximo 60
                        xml.AppendLine("                        <xCpl>" + dados.TRANSPORTADORA_ENDERECO_COMPLEMENTO.Validar(true, 60) + "</xCpl>");

                        // Bairro
                        // Minimo 2
                        // Maximo 60
                        xml.AppendLine("                        <xBairro>" + dados.TRANSPORTADORA_ENDERECO_NMBAIRRO.Validar(true, 60) + "</xBairro>");

                        // Código do município
                        xml.AppendLine("                        <cMun>" + dados.TRANSPORTADORA_ENDERECO_CIDADE + "</cMun>");

                        // Nome do município
                        // Minimo 2
                        // Maximo 60
                        xml.AppendLine("                        <xMun>" + paisesUFsCidades.Cidades.FirstOrDefault(a => a.ID_CIDADE == dados.TRANSPORTADORA_ENDERECO_CIDADE).NM.Validar(true, 60) + "</xMun>");

                        // Sigla da UF
                        xml.AppendLine("                        <UF>" + dados.TRANSPORTADORA_ENDERECO_UF + "</UF>");

                        // CEP - NT 2011/004
                        // [0-9]{8}
                        xml.AppendLine("                        <CEP>" + dados.TRANSPORTADORA_ENDERECO_CEP + "</CEP>");

                        // Código do país
                        xml.AppendLine("                        <cPais>" + dados.TRANSPORTADORA_ENDERECO_PAIS + "</cPais>");

                        // Nome do país
                        xml.AppendLine("                        <xPais>" + paisesUFsCidades.Paises.FirstOrDefault(a => a.ID_PAIS == dados.TRANSPORTADORA_ENDERECO_PAIS).NM.Validar(true, 60) + "</xPais>");

                        // Telefone, preencher com Código DDD + número do telefone , nas operações com exterior é permtido informar o código do país + código da localidade + número do telefone
                        // [0-9]{6,14}
                        xml.AppendLine("                        <fone>" + dados.TRANSPORTADORA_CONTATO_TELEFONE + "</fone>");

                        xml.AppendLine("                    </xEnder>");

                        #endregion

                        // Nome do munícipio
                        // Minimo 1
                        // Maximo 60
                        xml.AppendLine("                    <xMun>" + paisesUFsCidades.Cidades.FirstOrDefault(a => a.ID_CIDADE == dados.TRANSPORTADORA_ENDERECO_CIDADE).NM.Validar(true, 60) + "</xMun>");

                        // Sigla da UF
                        xml.AppendLine("                    <UF>" + paisesUFsCidades.UFs.FirstOrDefault(a => a.ID_UF == dados.TRANSPORTADORA_ENDERECO_UF).SIGLA + "</UF>");

                        xml.AppendLine("                </transporta>");


                        #endregion

                        #region Dados da retenção  ICMS do Transporte

                        // Dados da retenção  ICMS do Transporte
                        xml.AppendLine("                <retTransp>");

                        // Valor do Serviço
                        xml.AppendLine("                    <vServ>" + dados.NOTA_ITENS.Sum(a => a.VL_FRETE.Padrao()).ENUS(10) + "</vServ>");

                        // BC da Retenção do ICMS
                        xml.AppendLine("                    <vBCRet>" + bcICMSRetido_Total.ENUS(10) + "</vBCRet>");

                        // Alíquota da Retenção
                        xml.AppendLine("                    <pICMSRet>" + pcICMSRetido_Total.ENUS(10) + "</pICMSRet>");

                        // Valor do ICMS Retido
                        xml.AppendLine("                    <vICMSRet>" + vlICMSRetido_Total.ENUS(10) + "</vICMSRet>");

                        // Código Fiscal de Operações e Prestações // PL_006f - alterado para permitir somente CFOP de transportes 
                        xml.AppendLine("                    <CFOP>" + dados.CONFIGURACAO_CFOP + "</CFOP>");

                        // Código do Município de Ocorrência do Fato Gerador (utilizar a tabela do IBGE)
                        xml.AppendLine("                    <cMunFG>" + dados.EMITENTE_ENDERECO_CIDADE + "</cMunFG>");

                        xml.AppendLine("                </retTransp>");

                        #endregion

                        #region Dados do veículo

                        // Dados do veículo
                        xml.AppendLine("                <veicTransp>");

                        // Placa do veículo (NT2011/004)
                        xml.AppendLine("                    <placa>" + dados.TRANSPORTADORA_VEICULO.PLACA.Validar(true, 7) + "</placa>");

                        // Sigla da UF
                        xml.AppendLine("                    <UF>" + paisesUFsCidades.UFs.FirstOrDefault(a => a.ID_UF == dados.TRANSPORTADORA_VEICULO.ID_UF).SIGLA + "</UF>");

                        // Registro Nacional de Transportador de Carga (ANTT)
                        xml.AppendLine("                    <RNTC>" + dados.TRANSPORTADORA_VEICULO.RNTC.Validar(true, 20) + "</RNTC>");

                        xml.AppendLine("                </veicTransp>");

                        #endregion

                        #region Dados do reboque/Dolly (v2.0)

                        var reboque = Conexao.BancoDados.TB_FRO_VEICULOs.FirstOrDefault(a => a.ID_VEICULO_PAI == dados.TRANSPORTADORA_VEICULO.ID_VEICULO && (a.TP == "10" || a.TP == "11"));
                        if (reboque != null)
                        {
                            // Dados do reboque/Dolly (v2.0)
                            xml.AppendLine("                <reboque>");

                            // Placa do veículo (NT2011/004)
                            xml.AppendLine("                    <placa>" + reboque.PLACA.Validar(true, 7) + "</placa>");

                            // Sigla da UF
                            xml.AppendLine("                    <UF>" + paisesUFsCidades.UFs.FirstOrDefault(a => a.ID_UF == reboque.ID_UF).SIGLA + "</UF>");

                            // Registro Nacional de Transportador de Carga (ANTT)
                            xml.AppendLine("                    <RNTC>" + reboque.RNTC.Validar(true, 20) + "</RNTC>");

                            xml.AppendLine("                </reboque>");
                        }

                        #endregion

                        var vagao = Conexao.BancoDados.TB_FRO_VEICULOs.FirstOrDefault(a => a.ID_VEICULO_PAI == dados.TRANSPORTADORA_VEICULO.ID_VEICULO && a.TP == "50");
                        if (vagao != null)
                        {
                            // Identificação do vagão (v2.0)
                            // Minimo 1
                            // Maximo 20
                            xml.AppendLine("                <vagao>" + vagao.PLACA.Validar(true, 20) + "</vagao>");
                        }

                        var balsa = Conexao.BancoDados.TB_FRO_VEICULOs.FirstOrDefault(a => a.ID_VEICULO_PAI == dados.TRANSPORTADORA_VEICULO.ID_VEICULO && a.TP == "51");
                        if (balsa != null)
                        {
                            // Identificação da balsa (v2.0)
                            // Minimo 1
                            // Maximo 20
                            xml.AppendLine("                <balsa>" + balsa.PLACA.Validar(true, 20) + "</balsa>");
                        }

                        #region Dados dos volumes

                        // Utilizar Foreach!!! Máximo 5000 volumes!

                        // Dados do reboque/Dolly (v2.0)
                        //xml.AppendLine("                <vol>");

                        // Quantidade de volumes transportados
                        //xml.AppendLine("                    <qVol>" + "</qVol>");

                        // Espécie dos volumes transportados
                        // Minimo 1
                        // Maximo 60
                        //xml.AppendLine("                    <esp>" + "</esp>");

                        // Marca dos volumes transportados
                        // Minimo 1
                        // Maximo 60
                        //xml.AppendLine("                    <marca>" + "</marca>");

                        // Numeração dos volumes transportados
                        // Minimo 1
                        // Maximo 60
                        //xml.AppendLine("                    <nVol>" + "</nVol>");

                        // Peso líquido (em kg)
                        //xml.AppendLine("                    <pesoL>" + "</pesoL>");

                        // Peso bruto (em kg)
                        //xml.AppendLine("                    <pesoB>" + "</pesoB>");


                        // Utilizar Foreach! Maximo  5000 lacres!
                        //xml.AppendLine("                    <lacres>");

                        // Número dos Lacres
                        // Minimo 1
                        // Maximo 60
                        //xml.AppendLine("                        <nLacre>" + "</nLacre>");

                        //xml.AppendLine("                    </lacres>");

                        //xml.AppendLine("                </vol>");

                        #endregion
                    }

                    xml.AppendLine("            </transp>");


                    #endregion

                    #region Dados da cobrança da NF-e

                    // Dados da cobrança da NF-e
                    xml.AppendLine("            <cobr>");

                    #region Dados da fatura

                    // Fatura aqui é tratado como duplicata!

                    // Dados da fatura
                    xml.AppendLine("                <fat>");

                    // Número da fatura
                    // Minimo 1
                    // Maximo 60
                    xml.AppendLine("                    <nFat>" + dados.DUPLICATA.ID_DOCUMENTO + "</nFat>");

                    // Valor original da fatura
                    xml.AppendLine("                    <vOrig>" + dados.DUPLICATA.VL.Padrao().ENUS(5) + "</vOrig>");

                    // Valor do desconto da fatura
                    xml.AppendLine("                    <vDesc>" + dados.DUPLICATA_PARCELAS.Sum(a => a.VL_DESCONTO.Padrao()).ENUS(5) + "</vDesc>");

                    // Valor líquido da fatura
                    xml.AppendLine("                    <vLiq>" + dados.DUPLICATA_PARCELAS_LIQUIDADAS.Sum(a => a.Sum(aa => aa.VL.Padrao())).ENUS(5) + "</vLiq>");

                    xml.AppendLine("                </fat>");

                    #endregion

                    #region Dados das duplicatas NT 2011/004

                    // Duplicatas aqui é tratado como parcelas!
                    foreach (var parcela in dados.DUPLICATA_PARCELAS)
                    {
                        // Dados das duplicatas NT 2011/004
                        xml.AppendLine("                <dup>");

                        // Número da duplicata
                        // Minimo 1
                        // Maximo 60
                        xml.AppendLine("                    <nDup>" + parcela.ID_PARCELA + "</nDup>");

                        // Data de vencimento da duplicata (AAAA-MM-DD)
                        xml.AppendLine("                    <dVenc>" + parcela.DT_VENCIMENTO.Padrao().ToString("yyyy-MM-dd") + "</dVenc>");

                        // Valor da duplicata
                        xml.AppendLine("                    <vDup>" + parcela.VL.Padrao().ENUS(5) + "</vDup>");

                        xml.AppendLine("                </dup>");
                    }

                    #endregion

                    xml.AppendLine("            </cobr>");

                    #endregion

                    #region Dados de Pagamento. Obrigatório apenas para (NFC-e) NT 2012/004

                    foreach (var liquidacao in dados.DUPLICATA_PARCELAS_LIQUIDADAS)
                        foreach (var liquidacaoParcela in liquidacao)
                        {
                            // Dados de Pagamento. Obrigatório apenas para (NFC-e) NT 2012/004
                            xml.AppendLine("            <pag>");

                            // Forma de Pagamento:
                            // 01 -Dinheiro;
                            // 02 -Cheque;
                            // 03 -Cartão de Crédito;
                            // 04 -Cartão de Débito;
                            // 05 -Crédito Loja;
                            // 10 -Vale Alimentação;
                            // 11 -Vale Refeição;
                            // 12 -Vale Presente;
                            // 13 -Vale Combustível;
                            // 99 - Outros
                            xml.AppendLine("                <tPag>" + liquidacaoParcela.ID_FORMAPAGAMENTO + "</tPag>");

                            // Valor do Pagamento
                            xml.AppendLine("                <vPag>" + liquidacaoParcela.VL.Padrao().ENUS(5) + "</vPag>");

                            #region Grupo de Cartões

                            if (liquidacaoParcela.ID_CARTAO_CLIFORAUTORIZADOR.TemValor())
                            {
                                // Grupo de Cartões
                                xml.AppendLine("                <card>");

                                // Tipo de Integração do processo de pagamento com o sistema de automação da empresa: 
                                // 1 = Pagamento integrado com o sistema de automação da empresa Ex.equipamento TEF, Comercio Eletronico
                                // 2 = Pagamento não integrado com o sistema de automação da empresa Ex: equipamento POS
                                xml.AppendLine("                    <tpIntegra>" + liquidacaoParcela.ID_CARTAO_INTEGRACAO.Validar(true, 1) + "</tpIntegra>");

                                // CNPJ da credenciadora de cartão de crédito/débito
                                xml.AppendLine("                    <CNPJ>" + liquidacaoParcela.TB_REL_CLIFOR.CNPJ.Validar(true) + "</CNPJ>");

                                // Bandeira da operadora de cartão de crédito/débito:
                                // 01–Visa;
                                // 02–Mastercard;
                                // 03–American Express; 
                                // 04–Sorocred;
                                // 99–Outros
                                xml.AppendLine("                    <tBand>" + liquidacaoParcela.ID_CARTAO_BANDEIRA + "</tBand>");

                                // Número de autorização da operação cartão de crédito/débito
                                xml.AppendLine("                    <cAut>" + liquidacaoParcela.ID_CARTAO_AUTORIZACAO + "</cAut>");

                                xml.AppendLine("                <card>");
                            }

                            #endregion
                        }

                    xml.AppendLine("            </pag>");

                    #endregion

                    #region Informações adicionais da NF-e

                    // Informações adicionais da NF-e
                    xml.AppendLine("            <infAdic>");

                    // Informações adicionais de interesse do Fisco (v2.0)
                    var infAdFisco = "";
                    dados.NOTA_ITENS.Select(a => a.DS_OBSERVACAOFISCAL).ToList().ForEach(a => infAdFisco += a + Environment.NewLine);
                    xml.AppendLine("                <infAdFisco>" + infAdFisco + "</infAdFisco>");

                    // Informações complementares de interesse do Contribuinte
                    var infCpl = "";
                    dados.NOTA_ITENS.Select(a => a.DS_OBSERVACAO).ToList().ForEach(a => infCpl += a + Environment.NewLine);
                    xml.AppendLine("                <infCpl>" + infCpl + "</infCpl>");

                    #region Campo de uso livre do contribuinte informar o nome do campo no atributo xCampo e o conteúdo do campo no xTexto

                    // Utilizar Foreach! Maximo 10!

                    // Campo de uso livre do contribuinte informar o nome do campo no atributo xCampo e o conteúdo do campo no xTexto
                    //xml.AppendLine("                <obsCont xCampo=\"" + "\">");

                    // Mínimo 1
                    // Maximo 60
                    //xml.AppendLine("                    <xTexto>" + "</xTexto>");

                    //xml.AppendLine("                </obsCont>");

                    #endregion

                    #region Campo de uso exclusivo do Fisco informar o nome do campo no atributo xCampo e o conteúdo do campo no xTexto

                    // Utilizar Foreach! Maximo 10!

                    // Campo de uso exclusivo do Fisco informar o nome do campo no atributo xCampo e o conteúdo do campo no xTexto
                    //xml.AppendLine("                <obsFisco xCampo=\"" + "\">");

                    // Mínimo 1
                    // Maximo 60
                    //xml.AppendLine("                    <xTexto>" + "</xTexto>");

                    //xml.AppendLine("                </obsFisco>");

                    #endregion

                    #region Grupo de informações do  processo referenciado

                    // Utilizar Foreach! Maximo 100!

                    // Grupo de informações do  processo referenciado
                    //xml.AppendLine("                <procRef");

                    // Indentificador do processo ou ato concessório
                    // Mínimo 1
                    // Maximo 60
                    //xml.AppendLine("                    <nProc>" + "</nProc>");

                    // Origem do processo, informar com:
                    // 0 - SEFAZ;
                    // 1 - Justiça Federal;
                    // 2 - Justiça Estadual;
                    // 3 - Secex / RFB;
                    // 9 - Outros
                    //xml.AppendLine("                    <indProc>" + "</indProc>");

                    //xml.AppendLine("                </procRef>");

                    #endregion

                    xml.AppendLine("            </infAdic>");

                    #endregion

                    #region Informações de exportação

                    if (dados.CONFIGURACAO_DESTINOOPERACAO == "3")
                    {
                        // Informações de exportação
                        xml.AppendLine("            <exporta>");

                        // Sigla da UF de Embarque ou de transposição de fronteira
                        xml.AppendLine("                <UFSaidaPais>" + paisesUFsCidades.UFs.FirstOrDefault(a => a.ID_UF == dados.EMITENTE_ENDERECO_UF).SIGLA + "</UFSaidaPais>");

                        // Local de Embarque ou de transposição de fronteira
                        // Minimo 1
                        // Maximo 60
                        xml.AppendLine("                <xLocExporta>" + dados.EMITENTE_ENDERECO_NMRUA.Validar(true, 60) + "</xLocExporta>");

                        // Descrição do local de despacho
                        // Minimo 1
                        // Maximo 60
                        xml.AppendLine("                <xLocDespacho>" + dados.DESTINATARIO_ENDERECO_NMRUA.Validar(true, 60) + "</xLocDespacho>");

                        xml.AppendLine("            <exporta>");
                    }

                    #endregion

                    #region Informações de compras  (Nota de Empenho, Pedido e Contrato)

                    if (dados.CONFIGURACAO_MOVIMENTO == "E")
                    {
                        // Informações de compras  (Nota de Empenho, Pedido e Contrato)
                        xml.AppendLine("            <compra>");

                        // Informação da Nota de Empenho de compras públicas (NT2011/004)
                        // Minimo 1
                        // Maximo 22
                        //xml.AppendLine("                <xNEmp>" + "</xNEmp>");

                        // Informação do pedido
                        // Minimo 1
                        // Maximo 60
                        //xml.AppendLine("                <xPed>" + "</xPed>");

                        // Informação do contrato
                        // Minimo 1
                        // Maximo 60
                        //xml.AppendLine("                <xCont>" + "</xCont>");

                        xml.AppendLine("            </compra>");
                    }

                    #endregion

                    #region Informações de registro aquisições de cana

                    // Informações de registro aquisições de cana
                    //xml.AppendLine("            <cana versao=\"" + dados.CONFIGURACAO_VERSAO + "\">");

                    // Identificação da safra
                    // Minimo 4
                    // Maximo 9
                    //xml.AppendLine("                <safra>" + "</safra>");

                    // Mês e Ano de Referência, formato: MM/AAAA
                    // (0[1-9]|1[0-2])([/][2][0-9][0-9][0-9])
                    //xml.AppendLine("                <ref>" + "</ref>");

                    #region Fornecimentos diários

                    // Fornecimentos diários
                    //xml.AppendLine("                <forDia>");

                    // Utilizar Foreach! Minimo 1 / Maximo 31
                    // Quantidade em quilogramas - peso líquido
                    //xml.AppendLine("                    <qtde=\"" + "\">" + "</qtde>");

                    //xml.AppendLine("                </forDia>");

                    #endregion

                    // Total do mês
                    //xml.AppendLine("                <qTotMes>" + "</qTotMes>");

                    // Total Anterior
                    //xml.AppendLine("                <qTotAnt>" + "</qTotAnt>");

                    // Total Geral
                    //xml.AppendLine("                <qTotGer>" + "</qTotGer>");

                    #region Deduções - Taxas e Contribuições

                    // Utilizar Foreach! Maximo 10.

                    // Deduções - Taxas e Contribuições
                    //xml.AppendLine("                <deduc>");

                    // Descrição da Dedução
                    // Minimo 1
                    // Maximo 60
                    //xml.AppendLine("                    <xDed>" + "</xDed>");

                    // Valor da dedução
                    //xml.AppendLine("                    <vDed>" + "</vDed>");

                    //xml.AppendLine("                </deduc>");

                    #endregion

                    // Valor  dos fornecimentos
                    //xml.AppendLine("                <vFor>" + "</vFor>");

                    // Valor Total das Deduções
                    //xml.AppendLine("                <vTotDed>" + "</vTotDed>");

                    // Valor Líquido dos fornecimentos
                    //xml.AppendLine("                <vLiqFor>" + "</vLiqFor>");

                    //xml.AppendLine("            </cana>");

                    #endregion

                    xml.AppendLine("        </infNFe>");


                    

                    var xmlAssinado = xml.ToString();

                    Assinar(ref xmlAssinado, "infNFe", certificado.ID_SERIAL);

                    #region Informações suplementares Nota Fiscal

                    // Informações suplementares Nota Fiscal
                    xml.AppendLine("        <infNFeSupl>");

                    var cpfCNPJConsumidor = (dados.DESTINATARIO_CPF ?? dados.DESTINATARIO_CNPJ).Padrao();
                    var vlPagar = dados.NOTA_ITENS.Sum(a => a.VL_SUBTOTAL.Padrao());
                    

                    var assinadoConvertido = new XmlDocument();
                    assinadoConvertido.LoadXml(xmlAssinado);
                    var digestValue = assinadoConvertido.GetElementById("digVal").Value.SHA1HashStringForUTF8String();

                    var qrCode = "http://www.dfeportal.fazenda.pr.gov.br/dfe-portal/rest/servico/consultaNFCe?chNFe=" + chaveAcesso +
                                 "&nVersao=100" +
                                 "&tpAmb=" + tpAmb +
                                 "&cDest=" + cpfCNPJConsumidor +
                                 "&dhEmi=" + Convert.ToInt64(dados.NOTA_EMISSAO.Padrao().ToUniversalTime().ToString("yyyy-MM-ddTHH\\:mm\\:ssTZD")).ToString("x") +
                                 "&vNF=" + vlPagar.ENUS(2) +
                                 "&vICMS=" + vlICMS_Total.ENUS(2) +
                                 "&digVal=" + digestValue +
                                 "&cIdToken=" + "000000";  //000005
                                 //"&cHashQRCode=891BAEC47503A0E2930DCCA3179ADA303BF37078";

                    // Texto com o QR-Code impresso no DANFE NFC-e
                    xml.AppendLine("            <qrCode>"+ qrCode + "</qrCode>");

                    xml.AppendLine("        </infNFeSupl>");

                    #endregion
                    
                    // !!!!!!!!!!!!!!!!! Enviar a sefaz assim que possivel
                    var posicaoTransacao = 0;
                    new QDocumentoEletronico().Gravar(new TB_FIS_DOCUMENTOELETRONICO
                    {
                        ID_EMPRESA = id_empresa,
                        TB_CON_ARQUIVO = new TB_CON_ARQUIVO { ARQUIVO = new Binary(UTF8Encoding.UTF8.GetBytes(xmlAssinado)) },
                        CHAVE_ACESSO = chaveAcesso,
                        CHAVE_VERIFICACAO = digestValue,
                        TP_AMBIENTE = tpAmb,
                        ID_PROCESSO = dados.CONFIGURACAO_PROCESSO.ToString(),
                        ID_PROCESSO_VERSAO = dados.CONFIGURACAO_VERSAO,
                        TP_EMISSAO = emissao.ToString(),
                        ID_LOTE = dados.CONFIGURACAO_LOTE.Padrao(),
                        ID_SINCRONO = dados.CONFIGURACAO_SINCRONO,
                        TP_DOCUMENTO = modelo == Modelo.NFe ? "1" :
                                      (modelo == Modelo.NFCe ? "2" :
                                      (modelo == Modelo.NFSe ? "3" :
                                      (modelo == Modelo.CTe ? "4" :
                                      (modelo == Modelo.MDFe ? "5" : null))))
                    }, ref posicaoTransacao);

                    for (int vezes = 1; vezes <= 2; vezes++)
                    {
                        switch (impressao)
                        {
                            case ImpressaoDANFE.NFCe:
                                {
                                    var danfce = new Relatorios.Fiscal.RDANFCE();
                                    danfce.Parameters["EMPRESA"].Value = dados.EMITENTE_NM.Validar();
                                    danfce.Parameters["CNPJ"].Value = dados.EMITENTE_CNPJ.Validar();
                                    danfce.Parameters["IM"].Value = dados.EMITENTE_IM.Validar();
                                    danfce.Parameters["ENDERECO"].Value = dados.EMITENTE_ENDERECO_NMRUA.Padrao() + ", " + dados.EMITENTE_ENDERECO_NMBAIRRO.Validar() + ", " + dados.EMITENTE_ENDERECO_NR.Padrao();

                                    var unidades = new QUnidade().Unidades;

                                    danfce.bsITENS.DataSource = dados.NOTA_ITENS.Select(a => new
                                    {
                                        ID_PRODUTO = a.ID_PRODUTO,
                                        NM_PRODUTO = a.TB_EST_PRODUTO.NM,
                                        QT = a.QT.Padrao(),
                                        NM_UNIDADE = unidades.FirstOrDefault(b => b.ID == a.TB_EST_PRODUTO.ID_UNIDADE).NM,
                                        VL_UNITARIO = a.VL_UNITARIO.Padrao(),
                                        VL_TOTAL = a.VL_SUBTOTAL.Padrao()
                                    });

                                    danfce.Parameters["QT_TOTAL"].Value = dados.NOTA_ITENS.Count().ENUS(2);
                                    danfce.Parameters["VL_TOTAL"].Value = "R$ " + dados.NOTA_ITENS.Sum(a => a.VL_SUBTOTAL.Padrao()).ENUS(2);
                                    danfce.Parameters["VL_DESCONTOS"].Value = "R$ " + dados.NOTA_ITENS.Sum(a => a.VL_DESCONTO.Padrao()).ENUS(2);
                                    danfce.Parameters["VL_ACRESCIMOS"].Value = "R$ " + dados.NOTA_ITENS.Sum(a => a.VL_ACRESCIMO.Padrao()).ENUS(2);

                                   

                                    danfce.Parameters["VL_PAGAR"].Value = "R$ " + vlPagar.ENUS(2);

                                    var vlTributos = dados.NOTA_ITENS.Sum(a => a.TB_FAT_NOTA_ITEM_TRIBUTOs.Sum(b => b.VL.Padrao()));

                                    var pcTributos = Math.Round(vlPagar / vlTributos, 2);

                                    danfce.Parameters["VL_TRIBUTOS"].Value = "R$ " + vlTributos.ENUS(2);
                                    danfce.Parameters["PC_TRIBUTOS"].Value = pcTributos.ENUS(2) + " %";

                                    var tpAmbiente = "EMISSÃO ";

                                    switch (emissao)
                                    {
                                        case Emissao.Normal: tpAmbiente += "NORMAL"; break;
                                        case Emissao.ContigenciaOFFLINE: tpAmbiente += "OFFLINE"; break;
                                    }

                                    danfce.Parameters["AMBIENTE"].Value = tpAmbiente;
                                    danfce.Parameters["NF_ID"].Value = dados.NOTA_ID;
                                    danfce.Parameters["NF_EMISSAO"].Value = dados.NOTA_EMISSAO;
                                    danfce.Parameters["NF_SERIE"].Value = dados.CONFIGURACAO_SERIE;
                                    danfce.Parameters["AMBIENTE"].Value = tpAmbiente;
                                    danfce.Parameters["VIA"].Value = vezes == 1 ? "VIA DO CONSUMIDOR" : "VIA DO EMISSOR";


                                    var siglaUF = paisesUFsCidades.UFs.FirstOrDefault(a => a.ID_UF == dados.EMITENTE_ENDERECO_UF).SIGLA;
                                    var siteSefaz = "";
                                    switch(siglaUF)
                                    {
                                        case "PR": siteSefaz = "www.fazenda.pr.gov.br"; break;
                                    }

                                    danfce.Parameters["SITE_SEFAZUF"].Value = siteSefaz;
                                    danfce.Parameters["NF_CHAVE"].Value = chaveAcesso;

                                    
                                    danfce.Parameters["NF_DESTINATARIO"].Value = cpfCNPJConsumidor.TemValor() ? cpfCNPJConsumidor : "COSUMIDOR NÃO IDENTIFICADO";

                                    danfce.Parameters["NF_QRCODE"].Value = qrCode;

                                    danfce.xrlProtocolo.Visible = false;
                                    danfce.Parameters["ID_PROTOCOLO"].Value = "";
                                    danfce.Parameters["DT_AUTENTICACAO"].Value = "";
                                }; break;
                        }
                    }
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        public void Receber()
        {

        }
        
    }
}