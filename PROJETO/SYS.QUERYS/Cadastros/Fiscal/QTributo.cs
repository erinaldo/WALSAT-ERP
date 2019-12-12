using SYS.UTILS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SYS.QUERYS.Cadastros.Fiscal
{
    public class QTributo
    {
        public IQueryable<TB_FIS_TRIBUTO> Buscar(int id_tributo = 0)
        {
            var consulta = from a in Conexao.BancoDados.TB_FIS_TRIBUTOs
                           select a;

            if (id_tributo.TemValor())
                consulta = consulta.Where(a => a.ID_TRIBUTO == id_tributo);

            return consulta;
        }

        public void Gravar(TB_FIS_TRIBUTO tributo, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_FIS_TRIBUTOs.FirstOrDefault(a => a.ID_TRIBUTO == tributo.ID_TRIBUTO);

                #region Inserção

                if (existente == null)
                {
                    if (tributo.TB_FIS_IMPOSTO != null)
                        tributo.TB_FIS_IMPOSTO.ID_IMPOSTO = (Conexao.BancoDados.TB_FIS_IMPOSTOs.Any() ? Conexao.BancoDados.TB_FIS_IMPOSTOs.Max(a => a.ID_IMPOSTO) : 0) + 1;

                    if (tributo.TB_FIS_TAXA != null)
                        tributo.TB_FIS_TAXA.ID_TAXA = (Conexao.BancoDados.TB_FIS_TAXAs.Any() ? Conexao.BancoDados.TB_FIS_TAXAs.Max(a => a.ID_TAXA) : 0) + 1;

                    if (tributo.TB_FIS_CONTRIBUICAO != null)
                        tributo.TB_FIS_CONTRIBUICAO.ID_CONTRIBUICAO = (Conexao.BancoDados.TB_FIS_CONTRIBUICAOs.Any() ? Conexao.BancoDados.TB_FIS_CONTRIBUICAOs.Max(a => a.ID_CONTRIBUICAO) : 0) + 1;

                    Conexao.Enviar();

                    tributo.ID_TRIBUTO = (Conexao.BancoDados.TB_FIS_TRIBUTOs.Any() ? Conexao.BancoDados.TB_FIS_TRIBUTOs.Max(a => a.ID_TRIBUTO) : 0) + 1;

                    Conexao.BancoDados.TB_FIS_TRIBUTOs.InsertOnSubmit(tributo);

                    Conexao.Enviar();
                }

                #endregion

                #region Atualização

                else
                {
                    existente.ID_DESONERACAO = tributo.ID_DESONERACAO;
                    existente.ID_MODALIDADE = tributo.ID_MODALIDADE;
                    existente.PC_MVA= tributo.PC_MVA;
                    existente.PC_CREDITO= tributo.PC_CREDITO;
                    existente.PC_DIFERIDO = tributo.PC_DIFERIDO;
                    existente.PC_REDUCAO = tributo.PC_REDUCAO;
                    existente.ST_SUBSTITUICAOTRIBUTARIA = tributo.ST_SUBSTITUICAOTRIBUTARIA;
                    existente.ST_DESONERADO = tributo.ST_DESONERADO;
                    existente.ST_DIFERIDO = tributo.ST_DIFERIDO;
                    existente.ST_RETIDO = tributo.ST_RETIDO;
                    existente.ST_REDUCAO = tributo.ST_REDUCAO;

                    Conexao.Enviar();
                }

                #endregion

                Conexao.Enviar();

                Conexao.Finalizar(ref posicaoTransacao);
            }
            catch (Exception excessao)
            {
                Conexao.Voltar(ref posicaoTransacao);
                throw excessao;
            }
        }

        public void Deletar(TB_FIS_TRIBUTO tributo, ref int posicaoTransacao)
        {
            try
            {
                Conexao.Iniciar(ref posicaoTransacao);

                var existente = Conexao.BancoDados.TB_FIS_TRIBUTOs.FirstOrDefault(a => a.ID_TRIBUTO == tributo.ID_TRIBUTO);
                if (existente != null)
                    Conexao.BancoDados.TB_FIS_TRIBUTOs.DeleteOnSubmit(tributo);

                Conexao.Enviar();

                Conexao.Finalizar(ref posicaoTransacao);
            }
            catch (Exception excessao)
            {
                Conexao.Voltar(ref posicaoTransacao);
                throw excessao;
            }
        }

        

        public string Nome(TB_FIS_TRIBUTO tributo)
        {
            var retorno = "";

            if (tributo != null)
            {
                // IMPOSTOS
                if (tributo.TB_FIS_IMPOSTO != null)
                {
                    //  FEDERAIS
                    if (tributo.TB_FIS_IMPOSTO.I01.Padrao())
                        retorno = "(IMPOSTO FEDERAL) - II - Imposto sobre a importação de produtos estrangeiros";
                    else if (tributo.TB_FIS_IMPOSTO.I02.Padrao())
                        retorno = "(IMPOSTO FEDERAL) - IE - Imposto sobre a exportação de produtos nacionais ou nacionalizados";
                    else if (tributo.TB_FIS_IMPOSTO.I03.Padrao())
                        retorno = "(IMPOSTO FEDERAL) - IR - Imposto sobre a renda e proventos de qualquer natureza";
                    else if (tributo.TB_FIS_IMPOSTO.I04.Padrao())
                        retorno = "(IMPOSTO FEDERAL) - IPI - Imposto sobre Produtos Industrializados";
                    else if (tributo.TB_FIS_IMPOSTO.I05.Padrao())
                        retorno = "(IMPOSTO FEDERAL) - IOF - Imposto sobre Operações Financeiras";
                    else if (tributo.TB_FIS_IMPOSTO.I06.Padrao())
                        retorno = "(IMPOSTO FEDERAL) - ITR - Imposto Territorial Rural";
                    else if (tributo.TB_FIS_IMPOSTO.I07.Padrao())
                        retorno = "(IMPOSTO FEDERAL) - IGF - Imposto sobre Grandes Fortunas (não esta sendo aplicado)";

                    //  ESTADUAIS
                    else if (tributo.TB_FIS_IMPOSTO.I08.Padrao())
                        retorno = "(IMPOSTO ESTADUAL) - ICMS - Imposto de Circulação de Mercadorias e Serviços";
                    else if (tributo.TB_FIS_IMPOSTO.I09.Padrao())
                        retorno = "(IMPOSTO ESTADUAL) - IPVA - Imposto sobre Propriedade de Veículos Automotores";
                    else if (tributo.TB_FIS_IMPOSTO.I10.Padrao())
                        retorno = "(IMPOSTO ESTADUAL) - ITCMD - Imposto sobre Transmissões Causa Mortis e Doações de Qualquer Bem ou Direito";

                    //  MUNICIPAIS
                    else if (tributo.TB_FIS_IMPOSTO.I11.Padrao())
                        retorno = "(IMPOSTO MUNICIPAL) - IPTU - Imposto sobre a Propriedade Predial e Territorial Urbana";
                    else if (tributo.TB_FIS_IMPOSTO.I12.Padrao())
                        retorno = "(IMPOSTO MUNICIPAL) - ITBI - Imposto sobre Transmissão Inter Vivos de Bens e Imóveis e de Direitos Reais a eles Relativos";
                    else if (tributo.TB_FIS_IMPOSTO.I13.Padrao())
                        retorno = "(IMPOSTO MUNICIPAL) - ISSQN - Impostos sobre Serviços de Qualquer Natureza";
                }
                // TAXAS
                else if (tributo.TB_FIS_TAXA != null)
                {
                    //  FEDERAIS
                    if (tributo.TB_FIS_TAXA.T01.Padrao())
                        retorno = "(TAXA FEDERAL) - Taxa de Avaliação in loco das Instituições de Educação e Cursos de Graduação – Lei 10.870/2004";
                    else if (tributo.TB_FIS_TAXA.T02.Padrao())
                        retorno = "(TAXA FEDERAL) - Taxa de Classificação, Inspeção e Fiscalização de produtos animais e vegetais ou de consumo nas atividades agropecuárias – Decreto Lei 1.899/1981";
                    else if (tributo.TB_FIS_TAXA.T03.Padrao())
                        retorno = "(TAXA FEDERAL) - Taxa de Controle e Fiscalização Ambiental – TCFA – Lei 10.165/2000";
                    else if (tributo.TB_FIS_TAXA.T04.Padrao())
                        retorno = "(TAXA FEDERAL) - Taxa de Controle e Fiscalização de Produtos Químicos – Lei 10.357/2001, art. 16";
                    else if (tributo.TB_FIS_TAXA.T05.Padrao())
                        retorno = "(TAXA FEDERAL) - Taxa de Emissão de Documentos";
                    else if (tributo.TB_FIS_TAXA.T06.Padrao())
                        retorno = "(TAXA FEDERAL) - Taxa de Fiscalização de Vigilância Sanitária Lei 9.782/1999, art. 23";
                    else if (tributo.TB_FIS_TAXA.T07.Padrao())
                        retorno = "(TAXA FEDERAL) - Taxa de Fiscalização dos Produtos Controlados pelo Exército Brasileiro – TFPC – Lei 10.834/2003";
                    else if (tributo.TB_FIS_TAXA.T08.Padrao())
                        retorno = "(TAXA FEDERAL) - Taxa de Fiscalização e Controle da Previdência Complementar – TAFIC – art. 12 da MP 233/2004";
                    else if (tributo.TB_FIS_TAXA.T09.Padrao())
                        retorno = "(TAXA FEDERAL) - Taxa de Pesquisa Mineral DNPM – Portaria Ministerial 503/1999";
                    else if (tributo.TB_FIS_TAXA.T10.Padrao())
                        retorno = "(TAXA FEDERAL) - Taxa de Serviços Administrativos – TSA – Zona Franca de Manaus – Lei 9960/2000";
                    else if (tributo.TB_FIS_TAXA.T11.Padrao())
                        retorno = "(TAXA FEDERAL) - Taxa de Serviços Metrológicos – art. 11 da Lei 9933/1999";
                    else if (tributo.TB_FIS_TAXA.T12.Padrao())
                        retorno = "(TAXA FEDERAL) - Taxas ao Conselho Nacional de Petróleo (CNP)";
                    else if (tributo.TB_FIS_TAXA.T13.Padrao())
                        retorno = "(TAXA FEDERAL) - Taxas de Outorgas (Radiodifusão, Telecomunicações, Transporte Rodoviário e Ferroviário, etc.)";
                    else if (tributo.TB_FIS_TAXA.T14.Padrao())
                        retorno = "(TAXA FEDERAL) - Taxas de Saúde Suplementar – ANS – Lei 9.961/2000, art. 18";
                    else if (tributo.TB_FIS_TAXA.T15.Padrao())
                        retorno = "(TAXA FEDERAL) - Taxa de Utilização do MERCANTE – Decreto 5.324/2004";
                    else if (tributo.TB_FIS_TAXA.T16.Padrao())
                        retorno = "(TAXA FEDERAL) - Taxa Processual Conselho Administrativo de Defesa Econômica – CADE – Lei 9.718/1998";
                    else if (tributo.TB_FIS_TAXA.T17.Padrao())
                        retorno = "(TAXA FEDERAL) - Taxa de Autorização do Trabalho Estrangeiro";

                    //  ESTADUAIS
                    else if (tributo.TB_FIS_TAXA.T18.Padrao())
                        retorno = "(TAXA ESTADUAL) - Taxa de Emissão de Documentos";
                    else if (tributo.TB_FIS_TAXA.T19.Padrao())
                        retorno = "(TAXA ESTADUAL) - Taxa de Licenciamento Anual de Veículo";

                    // MUNICIPAIS
                    else if (tributo.TB_FIS_TAXA.T20.Padrao())
                        retorno = "(TAXA MUNICIPAL) - Taxa de Coleta de Lixo";
                    else if (tributo.TB_FIS_TAXA.T21.Padrao())
                        retorno = "(TAXA MUNICIPAL) - Taxa de Conservação e Limpeza Pública";
                    else if (tributo.TB_FIS_TAXA.T22.Padrao())
                        retorno = "(TAXA MUNICIPAL) - Taxa de Emissão de Documentos";
                    else if (tributo.TB_FIS_TAXA.T23.Padrao())
                        retorno = "(TAXA MUNICIPAL) - Taxa de Licenciamento para Funcionamento e Alvará Municipal";

                    //  ADVERSAS
                    else if (tributo.TB_FIS_TAXA.T24.Padrao())
                        retorno = "(TAXA ADVERSA) - Taxa de Fiscalização CVM (Comissão de Valores Mobiliários) – Lei 7.940/1989";
                    else if (tributo.TB_FIS_TAXA.T25.Padrao())
                        retorno = "(TAXA ADVERSA) - Taxas do Registro do Comércio (Juntas Comerciais)";
                }
                // CONTRIBUIÇÕES
                else if (tributo.TB_FIS_CONTRIBUICAO != null)
                {
                    // FEDERAIS
                    if (tributo.TB_FIS_CONTRIBUICAO.C01.Padrao())
                        retorno = "(CONTRIBUIÇÃO FEDERAL) - INSS Autônomos e Empresários";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C02.Padrao())
                        retorno = "(CONTRIBUIÇÃO FEDERAL) - INSS Empregados";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C03.Padrao())
                        retorno = "(CONTRIBUIÇÃO FEDERAL) - INSS Patronal";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C04.Padrao())
                        retorno = "(CONTRIBUIÇÃO FEDERAL) - FGTS (contribuição)";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C05.Padrao())
                        retorno = "(CONTRIBUIÇÃO FEDERAL) - Contribuição Social Adicional para Reposição das Perdas Inflacionárias do FGTS – Lei Complementar 110/2001";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C06.Padrao())
                        retorno = "(CONTRIBUIÇÃO FEDERAL) - PIS/PASEP (contribuição) – Programa de Integração Social (PIS) e Programa de Formação do Patrimônio do Servidor Publico (PASEP)";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C07.Padrao())
                        retorno = "(CONTRIBUIÇÃO FEDERAL) - COFINS – Contribuição Social para o Financiamento da Seguridade Social";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C08.Padrao())
                        retorno = "(CONTRIBUIÇÃO FEDERAL) - CSLL – Contribuição Social sobre o Lucro Líquido";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C09.Padrao())
                        retorno = "(CONTRIBUIÇÃO FEDERAL) - Contribuição ao Fundo Nacional de Desenvolvimento Científico e Tecnológico – FNDCT – Lei 10.168/2000";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C10.Padrao())
                        retorno = "(CONTRIBUIÇÃO FEDERAL) - Contribuição ao Fundo Nacional de Desenvolvimento da Educação (FNDE), também chamado “Salário Educação”";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C11.Padrao())
                        retorno = "(CONTRIBUIÇÃO FEDERAL) - Contribuição ao Funrural – LC 11/71";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C12.Padrao())
                        retorno = "(CONTRIBUIÇÃO FEDERAL) - Contribuição ao Instituto Nacional de Colonização e Reforma Agrária (INCRA) – Lei 2.613/1955";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C13.Padrao())
                        retorno = "(CONTRIBUIÇÃO FEDERAL) - Contribuição ao Seguro Acidente de Trabalho (SAT)";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C14.Padrao())
                        retorno = "(CONTRIBUIÇÃO FEDERAL) - Contribuição à Direção de Portos e Costas (DPC) – Lei 5.461/1968";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C15.Padrao())
                        retorno = "(CONTRIBUIÇÃO FEDERAL) - Contribuição de Intervenção do Domínio Econômico – CIDE Combustíveis – Lei 10.336/2001";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C16.Padrao())
                        retorno = "(CONTRIBUIÇÃO FEDERAL) - Contribuição para o Desenvolvimento da Indústria Cinematográfica Nacional – CONDECINE – art. 32 da Medida Provisória 2228-1/2001 e Lei 10.454/2002";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C17.Padrao())
                        retorno = "(CONTRIBUIÇÃO FEDERAL) - Fundo Aeroviário (FAER) – Decreto Lei 1.305/1974";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C18.Padrao())
                        retorno = "(CONTRIBUIÇÃO FEDERAL) - Fundo de Fiscalização das Telecomunicações (FISTEL) – Lei 5.070/96 e Lei 9.472/97.";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C19.Padrao())
                        retorno = "(CONTRIBUIÇÃO FEDERAL) - Fundo de Universalização dos Serviços de Telecomunicações (FUST) – art. 6 da Lei 9.998/00.";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C20.Padrao())
                        retorno = "(CONTRIBUIÇÃO FEDERAL) - Fundo Especial de Desenvolvimento e Aperfeiçoamento das Atividades de Fiscalização (Fundaf) – art. 6 do Decreto-lei 1.437/75 e art. 10 da IN SRF 180/02.";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C21.Padrao())
                        retorno = "(CONTRIBUIÇÃO FEDERAL) - Adicional de Frete para Renovação da Marinha Mercante (AFRMM) – Lei 10.893/04";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C22.Padrao())
                        retorno = "(CONTRIBUIÇÃO FEDERAL) - Fundo da Marinha Mercante (FMM) – Lei 10.893/04";
                    //  MUNICIPAIS
                    else if (tributo.TB_FIS_CONTRIBUICAO.C23.Padrao())
                        retorno = "(CONTRIBUIÇÃO MUNICIPAL) - Contribuição para Custeio do Serviço de Iluminação Pública – Emenda Constitucional 39/2002";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C24.Padrao())
                        retorno = "(CONTRIBUIÇÃO MUNICIPAL) - Contribuições de melhoria: asfalto, calçamento, esgoto, rede de água, rede de esgoto, etc.";
                    //  ADVERSAS
                    else if (tributo.TB_FIS_CONTRIBUICAO.C25.Padrao())
                        retorno = "(CONTRIBUIÇÃO ADVERSA) - Contribuição ao Serviço Brasileiro de Apoio a Pequena Empresa (Sebrae) – Lei 8.029/1990";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C26.Padrao())
                        retorno = "(CONTRIBUIÇÃO ADVERSA) - Contribuição ao Serviço Nacional de Aprendizado Comercial (SENAC) – Lei 8.621/1946";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C27.Padrao())
                        retorno = "(CONTRIBUIÇÃO ADVERSA) - Contribuição ao Serviço Nacional de Aprendizado dos Transportes (SENAT) – Lei 8.706/1993";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C28.Padrao())
                        retorno = "(CONTRIBUIÇÃO ADVERSA) - Contribuição ao Serviço Nacional de Aprendizado Industrial (SENAI) – Lei 4.048/1942";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C29.Padrao())
                        retorno = "(CONTRIBUIÇÃO ADVERSA) - Contribuição ao Serviço Nacional de Aprendizado Rural (SENAR) – Lei 8.315/1991";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C30.Padrao())
                        retorno = "(CONTRIBUIÇÃO ADVERSA) - Contribuição ao Serviço Social da Indústria (SESI) – Lei 9.403/1946";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C31.Padrao())
                        retorno = "(CONTRIBUIÇÃO ADVERSA) - Contribuição ao Serviço Social do Comércio (SESC) – Lei 9.853/1946";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C32.Padrao())
                        retorno = "(CONTRIBUIÇÃO ADVERSA) - Contribuição ao Serviço Social do Cooperativismo (SESCOOP)";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C33.Padrao())
                        retorno = "(CONTRIBUIÇÃO ADVERSA) - Contribuição ao Serviço Social dos Transportes (SEST) – Lei 8.706/1993";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C34.Padrao())
                        retorno = "(CONTRIBUIÇÃO ADVERSA) - Contribuição Confederativa Laboral (dos empregados)";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C35.Padrao())
                        retorno = "(CONTRIBUIÇÃO ADVERSA) - Contribuição Confederativa Patronal (das empresas)";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C36.Padrao())
                        retorno = "(CONTRIBUIÇÃO ADVERSA) - Contribuição Sindical Laboral (não se confunde com a Contribuição Confederativa Laboral, vide comentários sobre a Contribuição Sindical Patronal)";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C37.Padrao())
                        retorno = "(CONTRIBUIÇÃO ADVERSA) - Contribuição Sindical Patronal (não se confunde com a Contribuição Confederativa Patronal, já que a Contribuição Sindical Patronal é obrigatória, pelo artigo 578 da CLT, e a Confederativa foi instituída pelo art. 8º, inciso IV, da Constituição Federal e é obrigatória em função da assembléia do Sindicato que a instituir para seus associados, independentemente da contribuição prevista na CLT)";
                    else if (tributo.TB_FIS_CONTRIBUICAO.C38.Padrao())
                        retorno = "(CONTRIBUIÇÃO ADVERSA) - Contribuições aos Órgãos de Fiscalização Profissional (OAB, CRC, CREA, CRECI, CORE, etc.)";
                }
            }

            return retorno;
        }
        
    }
}