<%@ page title="Contas - Opus" language="C#" masterpagefile="~/View/Site.Master" autoeventwireup="true" codebehind="Conta.aspx.cs" inherits="Opus.View.Telas.Moderador.Conta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <h2>Contas cadastradas</h2>
    </center>

    <br />
    <br />

    <div class="caixa-pesquisa">

        <div class="servico">

            <div class="mb-3">

                <div class="mb-3">
                    <label for="exampleFormControlInput1" class="form-label">Pesquisa</label>
                    <asp:TextBox ID="tbxPesquisa" type="text" class="form-control" Style="width: 120%;" runat="server"></asp:TextBox>
                </div>

            </div>
        </div>

        <div class="estado">
            <div class="mb-3">

                <label class="form-label">
                    Filtro de pesquisa
                </label>

                <asp:DropDownList ID="ddlFiltro"
                    AutoPostBack="false"
                    runat="server"
                    CssClass="form-select"
                    Style="width: 120%;">

                    <asp:ListItem Selected="True" Value="0">Nome</asp:ListItem>
                    <asp:ListItem Value="1">E-mail</asp:ListItem>
                    <asp:ListItem Value="2">Telefone</asp:ListItem>

                </asp:DropDownList>

            </div>
        </div>

        <div class="pesquisar">

            <asp:LinkButton
                ID="btnPesquisar"
                runat="server"
                CssClass="btn btn-dark cor-roxa botao"
                OnClick="btnPesquisar_Click">

                <asp:Image
                    runat="server"
                    ImageUrl="../../Img/IconePesquisa.png" />

            </asp:LinkButton>

        </div>
    </div>

    <asp:Label ID="lblMensagemPesquisa" CssClass="fonte-vermelha padding-lat-0" runat="server"></asp:Label>

    <!-- REPEATER DADOS -->
    <div class="container-resultados">

        <asp:Repeater
            ID="rptDados"
            runat="server"
            OnItemCommand="rptDados_ItemCommand">

            <itemtemplate>

                <div class="card-autonomo">

                    <asp:Image
                        ID="imgDado"
                        runat="server"
                        CssClass="foto-autonomo"
                        ImageUrl='<%# Eval("Imagem") %>' />

                    <div class="dados-autonomo">

                        <h5><%#: Eval("Nome") %></h5>

                        <p><%#: Eval("Email") %></p>

                        <p><%#: Eval("Telefone") %></p>

                    </div>

                    <asp:LinkButton
                        ID="btnSaibaMais"
                        runat="server"
                        Text="Ver mais"
                        CssClass="btn btn-dark cor-roxa"
                        CommandName="Detalhes"
                        CommandArgument='<%# Eval("Id") %>' />

                </div>

            </itemtemplate>

        </asp:Repeater>

    </div>

    <asp:HiddenField
        ID="hfUsuarioDadoID"
        runat="server" />

    <div class="modal fade"
        id="modalDado"
        tabindex="-1"
        aria-hidden="true">

        <div class="modal-dialog modal-xl modal-dialog-centered modal-dialog-scrollable">

            <div class="modal-content">

                <div class="modal-header">

                    <h5 class="modal-title">Conta do usuário
                    </h5>

                    <button
                        type="button"
                        class="btn-close"
                        data-bs-dismiss="modal"
                        aria-label="Fechar">
                    </button>

                </div>

                <div class="modal-body">

                    <button type="button" class="btn btn-danger float-end" data-bs-toggle="modal" data-bs-target="#alterarStatus">
                        Alterar o Status
                    </button>

                    <br />

                    <!-- DADOS DE USUÁRIO -->

                    <div class="text-center mb-4">

                        <br />
                        <asp:Image
                            ID="imgPerfilUsuario"
                            runat="server"
                            CssClass="foto-autonomo"
                            Style="" />

                        <h4 class="mt-3">
                            <asp:Label
                                ID="lblNomeUsuario"
                                runat="server" />
                        </h4>

                        <p class="mt-2">
                            <asp:Label
                                ID="lblEmailUsuario"
                                runat="server"
                                CssClass="text-secondary" />
                        </p>

                        <p class="mt-2">
                            <asp:Label
                                ID="lblTelefoneUsuario"
                                runat="server"
                                CssClass="text-secondary" />
                        </p>

                        <p class="mt-2">
                            <asp:Label
                                ID="lblStatusUsuario"
                                runat="server"
                                CssClass="text-secondary" />
                        </p>

                    </div>

                    <!-- DADOS PRINCIPAIS -->

                    <div class="text-center mb-4">

                        <p class="mt-2">
                            <asp:Label
                                ID="lblNotaAutonomo"
                                runat="server"
                                CssClass="text-secondary" />
                        </p>

                        <p class="mt-2">
                            <asp:Label
                                ID="lblTelefoneCorp"
                                runat="server"
                                CssClass="text-secondary" />
                        </p>

                        <p class="mt-2">
                            <asp:Label
                                ID="lblEmailCorp"
                                runat="server"
                                CssClass="text-secondary" />
                        </p>

                        <br />

                        <asp:Label
                            ID="lblDescricaoAutonomo"
                            runat="server" />

                    </div>

                    <!-- Avaliações que o usuário fez -->

                    <h5 class="padding-lat-45">Avaliações do usuário</h5>

                    <asp:Repeater
                        ID="rptAvaliacoesUsuario"
                        runat="server">

                        <itemtemplate>

                            <div class="card-avaliacao">

                                <!-- Título -->
                                <h5 class="titulo-avaliacao">
                                    <%# Eval("Titulo") %>
                                </h5>


                                <!-- Descrição -->
                                <p class="descricao-avaliacao">
                                    <%# Eval("Descricao") %>
                                </p>

                                <!-- Nota -->
                                <p>
                                    Nota:
        <strong>
            <%# Eval("Nota") %> / 5
        </strong>
                                </p>

                                <!-- Fotos -->
                                <div class="fotos-avaliacao">

                                    <asp:Repeater
                                        ID="rptFotos"
                                        runat="server"
                                        DataSource='<%# Eval("Fotos") %>'>

                                        <itemtemplate>

                                            <asp:Image
                                                runat="server"
                                                CssClass="foto-avaliacao img-thumbnail"
                                                ImageUrl='<%# Eval("Imagem") %>' />

                                        </itemtemplate>

                                    </asp:Repeater>

                                </div>

                            </div>

                        </itemtemplate>

                    </asp:Repeater>

                    <!-- DADOS DE AUTÔNOMO -->

                    <div class="card-titulo">

                        <!-- SERVIÇOS -->

                        <asp:Label ID="lblServico" runat="server" Text="">

                            <hr />

                            <h5>Serviços oferecidos</h5>

                        </asp:Label>

                        <asp:Repeater
                            ID="rptServicosPerfil"
                            runat="server">

                            <headertemplate>
                                <div class="container-informacao padding-top-20">
                            </headertemplate>

                            <itemtemplate>
                                <div class="informacao cor-roxa">
                                    <%#: Eval("Nome") %>
                                </div>
                            </itemtemplate>

                            <footertemplate>
                    </div>
                    </FooterTemplate>

                        </asp:Repeater>

                </div>

                <div class="card-titulo">

                    <!-- REGIÕES -->

                    <asp:Label ID="lblRegioes" runat="server" Text="">

                        <hr />

                        <h5>Regiões de atendimento</h5>

                    </asp:Label>

                    <asp:Repeater ID="rptRegioesPerfil" runat="server">

                        <headertemplate>
                            <div class="container-informacao padding-top-20">
                        </headertemplate>

                        <itemtemplate>
                            <div class="informacao cor-roxa">
                                <%#: Eval("Cidade") %> - <%#: Eval("Estado") %>
                            </div>
                        </itemtemplate>

                        <footertemplate>
                </div>
                </FooterTemplate>

                        </asp:Repeater>

            </div>

            <div class="card-titulo">

                <!-- PORTFÓLIOS -->

                <asp:Label ID="lblPortifolio" runat="server" Text="">

                    <hr />

                    <h5>Portfólios</h5>

                </asp:Label>

            </div>

            <asp:Repeater
                ID="rptPortifoliosPerfil"
                runat="server">

                <itemtemplate>

                    <div class="card-portifolio-mais">

                        <p>
                            <%#: Eval("Descricao") %>
                        </p>

                        <div class="portifolio-fotos">

                            <asp:Repeater
                                runat="server"
                                DataSource='<%# Eval("Fotos") %>'>

                                <itemtemplate>

                                    <asp:Image
                                        runat="server"
                                        CssClass="portifolio-foto"
                                        ImageUrl='<%# Eval("Caminho") %>' />

                                </itemtemplate>

                            </asp:Repeater>

                        </div>

                    </div>

                </itemtemplate>

            </asp:Repeater>

            <div class="card-titulo">

                <!-- AVALIAÇÕES -->

                <asp:Label ID="lblAvaliacoes" runat="server" Text="">

                    <hr />

                    <h5>Avaliações recebidas</h5>

                </asp:Label>



            </div>

            <asp:HiddenField
                ID="hfDenunciadoSelecionado"
                runat="server" />

            <asp:Repeater
                ID="rptAvaliacoesPerfil"
                runat="server">

                <itemtemplate>

                    <div class="card-avaliacao">

                        <strong>
                            <%#: Eval("NomeUsuario") %>
                        </strong>



                        <h6 class="mt-3">
                            <%#: Eval("Titulo") %>
                        </h6>

                        <p>
                            <%#: Eval("Descricao") %>
                        </p>

                        <p>
                            Nota: <%# Eval("Nota") %> / 5
                        </p>

                        <div class="fotos-avaliacao">

                            <asp:Repeater
                                runat="server"
                                DataSource='<%# Eval("Fotos") %>'>

                                <itemtemplate>

                                    <asp:Image
                                        runat="server"
                                        CssClass="foto-avaliacao"
                                        ImageUrl='<%# Eval("Imagem") %>' />

                                </itemtemplate>

                            </asp:Repeater>

                        </div>

                    </div>

                </itemtemplate>

            </asp:Repeater>

        </div>

    </div>

    </div>

    </div>

    <!-- MODAL DE CONFIRMAÇÃO (FORA DO MODAL PRINCIPAL) -->

    <div class="modal fade"
        id="alterarStatus"
        tabindex="-1"
        aria-labelledby="alterarStatusLabel"
        aria-hidden="true">

        <div class="modal-dialog">

            <div class="modal-content">

                <div class="modal-header">

                    <h1 class="modal-title fs-5"
                        id="alterarStatusLabel">Alterar o status
                    </h1>

                    <button
                        type="button"
                        class="btn-close"
                        data-bs-dismiss="modal"
                        aria-label="Fechar">
                    </button>

                </div>

                <div class="modal-body">

                    <h6>Você tem certeza que deseja fazer isso?
                    </h6>

                </div>

                <div class="modal-footer">

                    <button
                        type="button"
                        class="btn btn-secondary"
                        data-bs-toggle="modal"
                        data-bs-target="#modalDado">
                        Não, voltar
                    </button>

                    <asp:LinkButton
                        ID="btnGerenciar"
                        runat="server"
                        Text="Sim"
                        CssClass="btn btn-danger"
                        OnClick="btnGerenciar_Click" />

                </div>

            </div>

        </div>

    </div>

</asp:Content>
