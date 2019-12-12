using SYS.QUERYS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SYS.UTILS;
using SYS.QUERYS.Cadastros.Fiscal;
using DevExpress.XtraLayout.Utils;

namespace SYS.FORMS.Cadastros.Fiscal
{
    public partial class FTributo_Cadastro : SYS.FORMS.FBase_Cadastro
    {
        #region Declarações

        public TB_FIS_TRIBUTO Tributo = null;

        #endregion

        #region Métodos

        public FTributo_Cadastro()
        {
            InitializeComponent();
        }

        public override void Gravar()
        {
            try
            {
                Validar();

                //Tributo.ID_TRIBUTO = teID_TRIBUTO.Text.ToInt32().Padrao();

                //// Impostos
                //if (rgImpostoFederal.SelectedIndex != -1 || rgImpostoEstadual.SelectedIndex != -1 || rgImpostoMunicipal.SelectedIndex != -1)
                //{
                //    Tributo.TB_FIS_IMPOSTO = new TB_FIS_IMPOSTO();

                //    if (rgImpostoFederal.SelectedIndex != -1)
                //        switch (rgImpostoFederal.EditValue.ToString())
                //        {
                //            case "I01": Tributo.TB_FIS_IMPOSTO.I01 = true; break;
                //            case "I02": Tributo.TB_FIS_IMPOSTO.I02 = true; break;
                //            case "I03": Tributo.TB_FIS_IMPOSTO.I03 = true; break;
                //            case "I04": Tributo.TB_FIS_IMPOSTO.I04 = true; break;
                //            case "I05": Tributo.TB_FIS_IMPOSTO.I05 = true; break;
                //            case "I06": Tributo.TB_FIS_IMPOSTO.I06 = true; break;
                //            case "I07": Tributo.TB_FIS_IMPOSTO.I07 = true; break;
                //        }
                //    else if (rgImpostoEstadual.SelectedIndex != -1)
                //        switch (rgImpostoFederal.EditValue.ToString())
                //        {
                //            case "I08": Tributo.TB_FIS_IMPOSTO.I08 = true; break;
                //            case "I09": Tributo.TB_FIS_IMPOSTO.I09 = true; break;
                //            case "I10": Tributo.TB_FIS_IMPOSTO.I10 = true; break;
                //        }
                //    else if (rgImpostoMunicipal.SelectedIndex != -1)
                //        switch (rgImpostoMunicipal.EditValue.ToString())
                //        {
                //            case "I11": Tributo.TB_FIS_IMPOSTO.I11 = true; break;
                //            case "I12": Tributo.TB_FIS_IMPOSTO.I12 = true; break;
                //            case "I13": Tributo.TB_FIS_IMPOSTO.I13 = true; break;
                //        }
                //}
                //// Taxas
                //else if (rgTaxaFederal.SelectedIndex != -1 || rgTaxaEstadual.SelectedIndex != -1 || rgTaxaMunicipal.SelectedIndex != -1 || rgTaxaAdversa.SelectedIndex != -1)
                //{
                //    Tributo.TB_FIS_TAXA = new TB_FIS_TAXA();

                //    if (rgTaxaFederal.SelectedIndex != -1)
                //        switch (rgTaxaFederal.EditValue.ToString())
                //        {
                //            case "T01": Tributo.TB_FIS_TAXA.T01 = true; break;
                //            case "T02": Tributo.TB_FIS_TAXA.T02 = true; break;
                //            case "T03": Tributo.TB_FIS_TAXA.T03 = true; break;
                //            case "T04": Tributo.TB_FIS_TAXA.T04 = true; break;
                //            case "T05": Tributo.TB_FIS_TAXA.T05 = true; break;
                //            case "T06": Tributo.TB_FIS_TAXA.T06 = true; break;
                //            case "T07": Tributo.TB_FIS_TAXA.T07 = true; break;
                //            case "T08": Tributo.TB_FIS_TAXA.T08 = true; break;
                //            case "T09": Tributo.TB_FIS_TAXA.T09 = true; break;
                //            case "T10": Tributo.TB_FIS_TAXA.T10 = true; break;
                //            case "T11": Tributo.TB_FIS_TAXA.T11 = true; break;
                //            case "T12": Tributo.TB_FIS_TAXA.T12 = true; break;
                //            case "T13": Tributo.TB_FIS_TAXA.T13 = true; break;
                //            case "T14": Tributo.TB_FIS_TAXA.T14 = true; break;
                //            case "T15": Tributo.TB_FIS_TAXA.T15 = true; break;
                //            case "T16": Tributo.TB_FIS_TAXA.T16 = true; break;
                //        }
                //    else if (rgTaxaEstadual.SelectedIndex != -1)
                //        switch (rgTaxaEstadual.EditValue.ToString())
                //        {
                //            case "T17": Tributo.TB_FIS_TAXA.T17 = true; break;
                //            case "T18": Tributo.TB_FIS_TAXA.T18 = true; break;
                //        }
                //    else if (rgTaxaMunicipal.SelectedIndex != -1)
                //        switch (rgTaxaMunicipal.EditValue.ToString())
                //        {
                //            case "T19": Tributo.TB_FIS_TAXA.T19 = true; break;
                //            case "T20": Tributo.TB_FIS_TAXA.T20 = true; break;
                //            case "T21": Tributo.TB_FIS_TAXA.T21 = true; break;
                //            case "T22": Tributo.TB_FIS_TAXA.T22 = true; break;
                //        }
                //    else if (rgTaxaAdversa.SelectedIndex != -1)
                //        switch (rgTaxaAdversa.EditValue.ToString())
                //        {
                //            case "T23": Tributo.TB_FIS_TAXA.T23 = true; break;
                //            case "T24": Tributo.TB_FIS_TAXA.T24 = true; break;
                //        }
                //}
                //// Federal
                //else if (rgContribuicaoFederal.SelectedIndex != -1 || rgContribuicaoMunicipal.SelectedIndex != -1 || rgTaxaAdversa.SelectedIndex != -1)
                //{
                //    Tributo.TB_FIS_CONTRIBUICAO = new TB_FIS_CONTRIBUICAO();

                //    if (rgContribuicaoFederal.SelectedIndex != -1)
                //        switch (rgContribuicaoFederal.EditValue.ToString())
                //        {
                //            case "C01": Tributo.TB_FIS_CONTRIBUICAO.C01 = true; break;
                //            case "C02": Tributo.TB_FIS_CONTRIBUICAO.C02 = true; break;
                //            case "C03": Tributo.TB_FIS_CONTRIBUICAO.C03 = true; break;
                //            case "C04": Tributo.TB_FIS_CONTRIBUICAO.C04 = true; break;
                //            case "C05": Tributo.TB_FIS_CONTRIBUICAO.C05 = true; break;
                //            case "C06": Tributo.TB_FIS_CONTRIBUICAO.C06 = true; break;
                //            case "C07": Tributo.TB_FIS_CONTRIBUICAO.C07 = true; break;
                //            case "C08": Tributo.TB_FIS_CONTRIBUICAO.C08 = true; break;
                //            case "C09": Tributo.TB_FIS_CONTRIBUICAO.C09 = true; break;
                //            case "C10": Tributo.TB_FIS_CONTRIBUICAO.C10 = true; break;
                //            case "C11": Tributo.TB_FIS_CONTRIBUICAO.C11 = true; break;
                //            case "C12": Tributo.TB_FIS_CONTRIBUICAO.C12 = true; break;
                //            case "C13": Tributo.TB_FIS_CONTRIBUICAO.C13 = true; break;
                //            case "C14": Tributo.TB_FIS_CONTRIBUICAO.C14 = true; break;
                //            case "C15": Tributo.TB_FIS_CONTRIBUICAO.C15 = true; break;
                //            case "C16": Tributo.TB_FIS_CONTRIBUICAO.C16 = true; break;
                //            case "C17": Tributo.TB_FIS_CONTRIBUICAO.C17 = true; break;
                //            case "C18": Tributo.TB_FIS_CONTRIBUICAO.C18 = true; break;
                //            case "C19": Tributo.TB_FIS_CONTRIBUICAO.C19 = true; break;
                //            case "C20": Tributo.TB_FIS_CONTRIBUICAO.C20 = true; break;
                //            case "C21": Tributo.TB_FIS_CONTRIBUICAO.C21 = true; break;
                //            case "C22": Tributo.TB_FIS_CONTRIBUICAO.C22 = true; break;
                //        }
                //    else if (rgContribuicaoMunicipal.SelectedIndex != -1)
                //        switch (rgContribuicaoMunicipal.EditValue.ToString())
                //        {
                //            case "C23": Tributo.TB_FIS_CONTRIBUICAO.C23 = true; break;
                //            case "C24": Tributo.TB_FIS_CONTRIBUICAO.C24 = true; break;
                //        }
                //    else if (rgTaxaAdversa.SelectedIndex != -1)
                //        switch (rgTaxaAdversa.EditValue.ToString())
                //        {
                //            case "C25": Tributo.TB_FIS_CONTRIBUICAO.C25 = true; break;
                //            case "C26": Tributo.TB_FIS_CONTRIBUICAO.C26 = true; break;
                //            case "C27": Tributo.TB_FIS_CONTRIBUICAO.C27 = true; break;
                //            case "C28": Tributo.TB_FIS_CONTRIBUICAO.C28 = true; break;
                //            case "C29": Tributo.TB_FIS_CONTRIBUICAO.C29 = true; break;
                //            case "C30": Tributo.TB_FIS_CONTRIBUICAO.C30 = true; break;
                //            case "C31": Tributo.TB_FIS_CONTRIBUICAO.C31 = true; break;
                //            case "C32": Tributo.TB_FIS_CONTRIBUICAO.C32 = true; break;
                //            case "C33": Tributo.TB_FIS_CONTRIBUICAO.C33 = true; break;
                //            case "C34": Tributo.TB_FIS_CONTRIBUICAO.C34 = true; break;
                //            case "C35": Tributo.TB_FIS_CONTRIBUICAO.C35 = true; break;
                //            case "C36": Tributo.TB_FIS_CONTRIBUICAO.C36 = true; break;
                //            case "C37": Tributo.TB_FIS_CONTRIBUICAO.C37 = true; break;
                //            case "C38": Tributo.TB_FIS_CONTRIBUICAO.C38 = true; break;
                //        }
                //}

                Validar();

                var posicaoTransacao = 0;
                new QTributo().Gravar(Tributo, ref posicaoTransacao);

                base.Gravar();
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        public override void Validar()
        {
            base.Validar();

            if (Tributo.TB_FIS_IMPOSTO == null && Tributo.TB_FIS_TAXA == null && Tributo.TB_FIS_CONTRIBUICAO == null)
                throw new SYSException(Mensagens.Necessario("imposto/taxa/contribuição"));
        }

        #endregion

        #region Eventos

        private void FTributo_Cadastro_Shown(object sender, EventArgs e)
        {
            try
            {
                if (Modo == Modo.Cadastrar)
                    Tributo = new TB_FIS_TRIBUTO();

                cbeTIPO.SetList(new List<IdentificadorDescricao>
                {
                    new IdentificadorDescricao
                    {
                        ID = "I",
                        DESCRICAO = "IMPOSTO"
                    },
                    new IdentificadorDescricao
                    {
                        ID = "T",
                        DESCRICAO = "TAXA"
                    },
                    new IdentificadorDescricao
                    {
                        ID = "C",
                        DESCRICAO = "CONTRIBUIÇÃO"
                    }
                });
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void cbeTIPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selecionado = cbeTIPO.GetID().Validar();

                if (!selecionado.TemValor())
                {
                    cbeORIGEM.SetList(new List<IdentificadorDescricao>());
                    cbeORIGEM.SelectedIndex = -1;
                    cbeORIGEM.Enabled = false;
                    return;
                }

                var lista = new List<IdentificadorDescricao>
                {
                    new IdentificadorDescricao
                    {
                        ID = "F",
                        DESCRICAO = "FEDERAL"
                    },
                    new IdentificadorDescricao
                    {
                        ID = "E",
                        DESCRICAO = "ESTADUAL"
                    },
                    new IdentificadorDescricao
                    {
                        ID = "M",
                        DESCRICAO = "MUNICIPAL"
                    }
                };

                if (selecionado != "I")
                    lista.Add(new IdentificadorDescricao
                    {
                        ID = "A",
                        DESCRICAO = "ADVERSA"
                    });

                cbeORIGEM.Enabled = true;
                cbeORIGEM.SetList(lista);

                if (selecionado == "I")
                {
                    cbeCLASSIFICACAO.Enabled = true;
                    cbeCLASSIFICACAO.SetList(new List<IdentificadorDescricao>
                    {
                        new IdentificadorDescricao
                        {
                            ID = "SUB",
                            DESCRICAO = "SUBSTITUIÇÃO TRIBUTÁRIA"
                        },
                        new IdentificadorDescricao
                        {
                            ID = "DES",
                            DESCRICAO = "DESONERADO"
                        },
                        new IdentificadorDescricao
                        {
                            ID = "DIF",
                            DESCRICAO = "DIFERIDO"
                        },
                        new IdentificadorDescricao
                        {
                            ID = "RET",
                            DESCRICAO = "RETIDO"
                        },
                        new IdentificadorDescricao
                        {
                            ID = "RED",
                            DESCRICAO = "REDUZIDO"
                        }
                    });
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void cbeORIGEM_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var tipo = cbeTIPO.GetID();
                var origem = cbeORIGEM.GetID();

                if (!tipo.TemValor() || !origem.TemValor())
                {
                    cbeOPCAO.SetList(new List<IdentificadorDescricao>());
                    cbeOPCAO.SelectedIndex = -1;

                    return;
                }

                var lista = new List<IdentificadorDescricao>();

                #region Impostos

                if (tipo == "I")
                {
                    #region Federal

                    if (origem == "F")
                        lista = new List<IdentificadorDescricao>
                            {
                                new IdentificadorDescricao
                                {
                                    ID = "I01",
                                    DESCRICAO = "II (IMPOSTO SOBRE A IMPORTAÇÃO DE PRODUTOS ESTRANGEIROS)"
                                },
                                new IdentificadorDescricao
                                {
                                    ID = "I02",
                                    DESCRICAO = "IE (IMPOSTO SOBRE A EXPORTAÇÃO DE PRODUTOS NACIONAIS OU NACIONALIZADOS)"
                                },
                                new IdentificadorDescricao
                                {
                                    ID = "I03",
                                    DESCRICAO = "IR (IMPOSTO SOBRE A RENDA E PROVENTOS DE QUALQUER NATUREZA)"
                                },
                                new IdentificadorDescricao
                                {
                                    ID="I04",
                                    DESCRICAO = "IPI (IMPOSTO SOBRE PRODUTOS INDUSTRIALIZADOS)"
                                },
                                new IdentificadorDescricao
                                {
                                    ID = "I05",
                                    DESCRICAO = "IOF (IMPOSTO SOBRE OPERAÇÕES FINANCEIRAS)"
                                },
                                new IdentificadorDescricao
                                {
                                    ID = "I06",
                                    DESCRICAO = "ITR (IMPOSTO TERRITORIAL RURAL)"
                                },
                                new IdentificadorDescricao
                                {
                                    ID = "I07",
                                    DESCRICAO = "IGF (IMPOSTO SOBRE GRANDES FORTUNAS)"
                                }
                        };

                    #endregion

                    #region Estadual

                    if (origem == "E")
                        lista = new List<IdentificadorDescricao>
                            {
                                new IdentificadorDescricao
                                {
                                    ID = "I08",
                                    DESCRICAO = "ICMS (IMPOSTO DE CIRCULAÇÃO DE MERCADORIAS E SERVIÇOS)"
                                },
                                new IdentificadorDescricao
                                {
                                    ID = "I09",
                                    DESCRICAO = "IPVA (IMPOSTO SOBRE PROPRIEDADE DE VEÍCULOS AUTOMOTORES)"
                                },
                                new IdentificadorDescricao
                                {
                                    ID = "I10",
                                    DESCRICAO = "ITCMD (IMPOSTO SOBRE TRANSMISSÕES CAUSA MORTIS E DOAÇÕES DE QUALQUER BEM OU DIREITO)"
                                }
                            };

                    #endregion

                    #region Municipal

                    if (origem == "M")
                        lista = new List<IdentificadorDescricao>
                            {
                                new IdentificadorDescricao
                                {
                                    ID = "I11",
                                    DESCRICAO = "IPTU (IMPOSTO SOBRE A PROPRIEDADE PREDIAL E TERRITORIAL URBANA)"
                                },
                                new IdentificadorDescricao
                                {
                                    ID = "I12",
                                    DESCRICAO = "ITBI (IMPOSTO SOBRE TRANSMISSÃO INTER VIVOS DE BENS E IMÓVEIS E DE DIREITOS REAIS A ELES RELATIVOS)"
                                },
                                new IdentificadorDescricao
                                {
                                    ID = "I12",
                                    DESCRICAO = "ITBI (IMPOSTO SOBRE TRANSMISSÃO INTER VIVOS DE BENS E IMÓVEIS E DE DIREITOS REAIS A ELES RELATIVOS)"
                                },
                                new IdentificadorDescricao
                                {
                                    ID = "I13",
                                    DESCRICAO = "ISSQN (IMPOSTOS SOBRE SERVIÇOS DE QUALQUER NATUREZA"
                                }
                            };

                    #endregion
                }

                #endregion

                #region Taxa

                if (tipo == "T")
                {
                    #region Federal

                    if (origem == "F")
                        lista = new List<IdentificadorDescricao>
                            {
                                new IdentificadorDescricao
                                {
                                    ID = "T01",
                                    DESCRICAO = "TAXA DE AVALIAÇÃO IN LOCO DAS INSTITUIÇÕES DE EDUCAÇÃO E CURSOS DE GRADUAÇÃO – LEI 10.870/2004"
                                },
                                new IdentificadorDescricao
                                {
                                    ID = "T02",
                                    DESCRICAO = "TAXA DE CLASSIFICAÇÃO, INSPEÇÃO E FISCALIZAÇÃO DE PRODUTOS ANIMAIS E VEGETAIS OU DE CONSUMO NAS ATIVIDADES AGROPECUÁRIAS – DECRETO LEI 1.899/1981"
                                },
                                new IdentificadorDescricao
                                {
                                    ID = "T03",
                                    DESCRICAO = "TAXA DE CONTROLE E FISCALIZAÇÃO AMBIENTAL – TCFA – LEI 10.165/2000"
                                },
                                new IdentificadorDescricao
                                {
                                    ID = "T04",
                                    DESCRICAO = "TAXA DE CONTROLE E FISCALIZAÇÃO DE PRODUTOS QUÍMICOS – LEI 10.357/2001, ART. 16"
                                },
                                new IdentificadorDescricao
                                {
                                    ID = "T05",
                                    DESCRICAO = "Taxa de Emissão de Documentos"
                                },
                                new IdentificadorDescricao
                                {
                                    ID = "T06",
                                    DESCRICAO = "TAXA DE FISCALIZAÇÃO DE VIGILÂNCIA SANITÁRIA LEI 9.782/1999, ART. 23"
                                },
                                new IdentificadorDescricao
                                {
                                    ID = "T07",
                                    DESCRICAO = "TAXA DE FISCALIZAÇÃO DOS PRODUTOS CONTROLADOS PELO EXÉRCITO BRASILEIRO – TFPC – LEI 10.834/2003"
                                },
                                new IdentificadorDescricao
                                {
                                    ID = "T08",
                                    DESCRICAO = "TAXA DE FISCALIZAÇÃO E CONTROLE DA PREVIDÊNCIA COMPLEMENTAR – TAFIC – ART. 12 DA MP 233/2004"
                                },
                                new IdentificadorDescricao
                                {
                                    ID = "T09",
                                    DESCRICAO = "TAXA DE PESQUISA MINERAL DNPM – PORTARIA MINISTERIAL 503/1999"
                                },
                                new IdentificadorDescricao
                                {
                                    ID = "T10",
                                    DESCRICAO = "TAXA DE SERVIÇOS ADMINISTRATIVOS – TSA – ZONA FRANCA DE MANAUS – LEI 9960/2000"
                                },
                                new IdentificadorDescricao
                                {
                                    ID = "T11",
                                    DESCRICAO = "TAXA DE SERVIÇOS METROLÓGICOS – ART. 11 DA LEI 9933/1999"
                                },
                                new IdentificadorDescricao
                                {
                                    ID = "T12",
                                    DESCRICAO = "TAXAS AO CONSELHO NACIONAL DE PETRÓLEO (CNP)"
                                },
                                new IdentificadorDescricao
                                {
                                    ID = "T13",
                                    DESCRICAO = "TAXAS DE OUTORGAS (RADIODIFUSÃO, TELECOMUNICAÇÕES, TRANSPORTE RODOVIÁRIO E FERROVIÁRIO, ETC.)"
                                },
                                new IdentificadorDescricao
                                {
                                    ID = "T14",
                                    DESCRICAO = "TAXAS DE SAÚDE SUPLEMENTAR – ANS – LEI 9.961/2000, ART. 18"
                                },
                                new IdentificadorDescricao
                                {
                                    ID = "T15",
                                    DESCRICAO = "TAXA DE UTILIZAÇÃO DO MERCANTE – DECRETO 5.324/2004"
                                },
                                new IdentificadorDescricao
                                {
                                    ID = "T16",
                                    DESCRICAO = "TAXA PROCESSUAL CONSELHO ADMINISTRATIVO DE DEFESA ECONÔMICA – CADE – LEI 9.718/1998"
                                },
                                new IdentificadorDescricao
                                {
                                    ID = "T17",
                                    DESCRICAO = "TAXA DE AUTORIZAÇÃO DO TRABALHO ESTRANGEIRO"
                                },
                        };

                    #endregion

                    #region Estadual

                    if (origem == "E")
                        lista = new List<IdentificadorDescricao>
                    {
                        new IdentificadorDescricao
                        {
                            ID = "T18",
                            DESCRICAO = "TAXA DE EMISSÃO DE DOCUMENTOS"
                        },
                        new IdentificadorDescricao
                        {
                            ID = "T19",
                            DESCRICAO = "TAXA DE LICENCIAMENTO ANUAL DE VEÍCULO"
                        }
                    };

                    #endregion

                    #region Municipal

                    if (origem == "M")
                        lista = new List<IdentificadorDescricao>
                        {
                            new IdentificadorDescricao
                            {
                                ID = "T20",
                                DESCRICAO = "TAXA DE COLETA DE LIXO"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "T21",
                                DESCRICAO = "TAXA DE CONSERVAÇÃO E LIMPEZA PÚBLICA"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "T22",
                                DESCRICAO = "TAXA DE EMISSÃO DE DOCUMENTOS"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "T23",
                                DESCRICAO = "TAXA DE LICENCIAMENTO PARA FUNCIONAMENTO E ALVARÁ MUNICIPAL"
                            },
                        };

                    #endregion

                    #region Adversa

                    if (origem == "A")
                        lista = new List<IdentificadorDescricao>
                        {
                            new IdentificadorDescricao
                            {
                                ID = "T24",
                                DESCRICAO = "TAXA DE FISCALIZAÇÃO CVM (COMISSÃO DE VALORES MOBILIÁRIOS) – LEI 7.940/1989"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "T25",
                                DESCRICAO= "TAXAS DO REGISTRO DO COMÉRCIO (JUNTAS COMERCIAIS)"
                            }
                        };

                    #endregion
                }

                #endregion

                #region Contribuição

                if (tipo == "C")
                {
                    #region Federal

                    if (origem == "F")
                        lista = new List<IdentificadorDescricao>
                        {
                            new IdentificadorDescricao
                            {
                                ID = "C01",
                                DESCRICAO = "INSS (INSTITUTO NACIONAL DO SEGURO SOCIAL - AUTÔNOMOS E EMPRESÁRIOS)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C02",
                                DESCRICAO = "INSS (INSTITUTO NACIONAL DO SEGURO SOCIAL - EMPREGADOS)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C03",
                                DESCRICAO = "INSS (INSTITUTO NACIONAL DO SEGURO SOCIAL - PATRONAL)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C04",
                                DESCRICAO = "FGTS (FUNDO DE GARANTIA POR TEMPO DE SERVIÇO)"
                            },
                            new IdentificadorDescricao
                            {
                                ID= "C05",
                                DESCRICAO = "CONTRIBUIÇÃO SOCIAL ADICIONAL PARA REPOSIÇÃO DAS PERDAS INFLACIONÁRIAS DO FGTS"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C06",
                                DESCRICAO = "PIS/PASEP (PROGRAMA DE INTEGRAÇÃO SOCIAL / PROGRAMA DE FORMAÇÃO DO PATRIMÔNIO DO SERVIDOR PUBLICO"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C07",
                                DESCRICAO = "COFINS (CONTRIBUIÇÃO SOCIAL PARA O FINANCIAMENTO DA SEGURIDADE SOCIAL)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C08",
                                DESCRICAO = "CSLL (CONTRIBUIÇÃO SOCIAL SOBRE O LUCRO LÍQUIDO)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C09",
                                DESCRICAO = "FNDCT (CONTRIBUIÇÃO AO FUNDO NACIONAL DE DESENVOLVIMENTO CIENTÍFICO E TECNOLÓGICO)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C10",
                                DESCRICAO = "FNDE (CONTRIBUIÇÃO AO FUNDO NACIONAL DE DESENVOLVIMENTO DA EDUCAÇÃO)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C11",
                                DESCRICAO = "FUNRURAL (CONTRIBUIÇÃO AO FUNDO DE ASSISTÊNCIA E PREVIDÊNCIA DO TRABALHADOR RURAL)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C12",
                                DESCRICAO = "INCRA (CONTRIBUIÇÃO AO INSTITUTO NACIONAL DE COLONIZAÇÃO E REFORMA AGRÁRIA)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C13",
                                DESCRICAO = "SAT (CONTRIBUIÇÃO AO SEGURO ACIDENTE DE TRABALHO)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C14",
                                DESCRICAO = "DPC (CONTRIBUIÇÃO À DIREÇÃO DE PORTOS E COSTAS)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C15",
                                DESCRICAO = "CIDE (CONTRIBUIÇÃO DE INTERVENÇÃO DO DOMÍNIO ECONÔMICO)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C16",
                                DESCRICAO = "CONDECINE (CONTRIBUIÇÃO PARA O DESENVOLVIMENTO DA INDÚSTRIA CINEMATOGRÁFICA NACIONAL)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C17",
                                DESCRICAO = "FAER (FUNDO AEROVIÁRIO)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C18",
                                DESCRICAO = "FISTEL (FUNDO DE FISCALIZAÇÃO DAS TELECOMUNICAÇÕES)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C19",
                                DESCRICAO = "FUST (FUNDO DE UNIVERSALIZAÇÃO DOS SERVIÇOS DE TELECOMUNICAÇÕES)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C20",
                                DESCRICAO = "FUNDAF (FUNDO ESPECIAL DE DESENVOLVIMENTO E APERFEIÇOAMENTO DAS ATIVIDADES DE FISCALIZAÇÃO)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C21",
                                DESCRICAO = "AFRMM (ADICIONAL DE FRETE PARA RENOVAÇÃO DA MARINHA MERCANTE)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C22",
                                DESCRICAO = "FMM (FUNDO DA MARINHA MERCANTE)"
                            }
                        };

                    #endregion

                    #region Municipal

                    if (origem == "M")
                        lista = new List<IdentificadorDescricao>
                        {
                            new IdentificadorDescricao
                            {
                                ID = "C23",
                                DESCRICAO = "CONTRIBUIÇÃO PARA CUSTEIO DO SERVIÇO DE ILUMINAÇÃO PÚBLICA"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C24",
                                DESCRICAO = "CONTRIBUIÇÕES DE MELHORIA: ASFALTO, CALÇAMENTO, ESGOTO, REDE DE ÁGUA, REDE DE ESGOTO, ETC."
                            }
                        };

                    #endregion

                    #region Adversa

                    if (origem == "A")
                        lista = new List<IdentificadorDescricao>
                        {
                            new IdentificadorDescricao
                            {
                                ID = "C25",
                                DESCRICAO = "SEBRAE (CONTRIBUIÇÃO AO SERVIÇO BRASILEIRO DE APOIO A PEQUENA EMPRESA)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C26",
                                DESCRICAO = "SENAC (CONTRIBUIÇÃO AO SERVIÇO NACIONAL DE APRENDIZADO COMERCIAL)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C27",
                                DESCRICAO = "SENAT (CONTRIBUIÇÃO AO SERVIÇO NACIONAL DE APRENDIZADO DOS TRANSPORTES)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C28",
                                DESCRICAO = "SENAI (CONTRIBUIÇÃO AO SERVIÇO NACIONAL DE APRENDIZADO INDUSTRIAL)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C29",
                                DESCRICAO = "SENAR (CONTRIBUIÇÃO AO SERVIÇO NACIONAL DE APRENDIZADO RURAL)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C30",
                                DESCRICAO = "SESI (CONTRIBUIÇÃO AO SERVIÇO SOCIAL DA INDÚSTRIA)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C31",
                                DESCRICAO = "SESC (CONTRIBUIÇÃO AO SERVIÇO SOCIAL DO COMÉRCIO)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C32",
                                DESCRICAO = "SESCOOP (CONTRIBUIÇÃO AO SERVIÇO SOCIAL DO COOPERATIVISMO)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C33",
                                DESCRICAO = "SEST (CONTRIBUIÇÃO AO SERVIÇO SOCIAL DOS TRANSPORTES)"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C34",
                                DESCRICAO = "CONTRIBUIÇÃO CONFEDERATIVA LABORAL - EMPREGADOS"
                            },
                            new IdentificadorDescricao
                            {
                                ID="C35",
                                DESCRICAO = "CONTRIBUIÇÃO CONFEDERATIVA PATRONAL - EMPRESAS"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C36",
                                DESCRICAO = "CONTRIBUIÇÃO SINDICAL LABORAL - EMPREGADOS"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C37",
                                DESCRICAO = "CONTRIBUIÇÃO SINDICAL PATRONAL - EMPRESAS"
                            },
                            new IdentificadorDescricao
                            {
                                ID = "C38",
                                DESCRICAO = "CONTRIBUIÇÕES AOS ÓRGÃOS DE FISCALIZAÇÃO PROFISSIONAL (OAB, CRC, CREA, CRECI, CORE, ETC.)"
                            }
                        };

                    #endregion
                }

                #endregion

                cbeOPCAO.SetList(lista);
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void cbeCLASSIFICACAO_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selecionado = cbeCLASSIFICACAO.GetID().Validar();

                if (selecionado == "SUB")
                    cbeMODALIDADE.SetList(new List<IdentificadorDescricao>
                    {
                        new IdentificadorDescricao
                        {
                            ID=0,
                            DESCRICAO = "PREÇO TABELADO OU MÁXIMO SUGERIDO"
                        },
                        new IdentificadorDescricao
                        {
                            ID = 1,
                            DESCRICAO = "LISTA NEGATIVA (VALOR)"
                        },
                        new IdentificadorDescricao
                        {
                            ID = 2,
                            DESCRICAO = "LISTA POSITIVA (VALOR)"
                        },
                        new IdentificadorDescricao
                        {
                            ID = 3,
                            DESCRICAO = "LISTA NEUTRA (VALOR)"
                        },
                        new IdentificadorDescricao
                        {
                            ID = 4,
                            DESCRICAO = "MARGEM VALOR AGREGADO (PORCENTAGEM)"
                        },
                        new IdentificadorDescricao
                        {
                            ID = 5,
                            DESCRICAO = "PAUTA (VALOR)"
                        }
                    });
                else
                {
                    cbeMODALIDADE.SetList(new List<IdentificadorDescricao>
                    {
                        new IdentificadorDescricao
                        {
                            ID=0,
                            DESCRICAO = "MARGEM VALOR AGREGADO (PORCENTAGEM)"
                        },
                        new IdentificadorDescricao
                        {
                            ID = 1,
                            DESCRICAO = "PAUTA (VALOR)"
                        },
                        new IdentificadorDescricao
                        {
                            ID = 2,
                            DESCRICAO = "PREÇO MÁXIMO TABELADO (VALOR)"
                        },
                        new IdentificadorDescricao
                        {
                            ID = 3,
                            DESCRICAO = "VALOR DA OPERAÇÃO"
                        },
                    });

                    if (selecionado == "DES")
                    {
                        cbeDESONERACAO.SetList(new List<IdentificadorDescricao>
                        {
                            new IdentificadorDescricao
                            {
                                ID = 1,
                                DESCRICAO = "TÁXI"
                            },
                            new IdentificadorDescricao
                            {
                                ID = 2,
                                DESCRICAO = "FOMENTO AGROPECUÁRIO"
                            },
                            new IdentificadorDescricao
                            {
                                ID = 3,
                                DESCRICAO = "PRODUTOR AGROPECUÁRIO"
                            },
                            new IdentificadorDescricao
                            {
                                ID = 4,
                                DESCRICAO = "FROTISTA / LOCADORA"
                            },
                            new IdentificadorDescricao
                            {
                                ID = 5,
                                DESCRICAO = "DIPLOMÁTICO / CONSULAR"
                            },
                            new IdentificadorDescricao
                            {
                                ID = 6,
                                 DESCRICAO = "UTILITÁRIOS E MOTOCICLETAS DA AMAZÔNIA OCIDENTAL E ÁREAS DE LIVRE COMÉRCIO"
                            },
                            new IdentificadorDescricao
                            {
                                ID = 7,
                                DESCRICAO = "Suframa"
                            },
                            new IdentificadorDescricao
                            {
                                ID = 8,
                                DESCRICAO = "VENDA A ÓRGÃO PÚBLICO"
                            },
                            new IdentificadorDescricao
                            {
                                ID = 9,
                                 DESCRICAO = "OUTROS"
                            },
                            new IdentificadorDescricao
                            {
                                ID = 10,
                                DESCRICAO = "DEFICIENTE CONDUTOR"
                            },
                            new IdentificadorDescricao
                            {
                                ID = 11,
                                DESCRICAO = "DEFICIENTE NÃO CONDUTOR"
                            },
                            new IdentificadorDescricao
                            {
                                ID = 16,
                                DESCRICAO = "OLIMPÍADAS"
                            }
                        });
                    }
                }

                cbeDESONERACAO.Enabled = selecionado == "DES";
                cbeMODALIDADE.Enabled = true;
                seREDUCAO.Enabled = selecionado == "RED";
                seDIFERIMENTO.Enabled = selecionado == "DIF";
                seCREDITO.Enabled = true;
                seMVA.Enabled = true;

                seREDUCAO.Value = 0;
                seDIFERIMENTO.Value = 0;
                seCREDITO.Value = 0;
                seMVA.Value = 0;
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void cbeOPCAO_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selecionado = cbeOPCAO.GetID();
                if (selecionado.TemValor())
                {
                    teDS.Text = cbeOPCAO.Text;
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        #endregion
    }
}