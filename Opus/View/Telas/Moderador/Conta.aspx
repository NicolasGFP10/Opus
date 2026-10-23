<%@ page title="Contas - Opus" language="C#" masterpagefile="~/View/Site.Master" autoeventwireup="true" codebehind="Conta.aspx.cs" inherits="Opus.View.Telas.Moderador.Conta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <h2>Contas cadastradas</h2>
    </center>

    <br />
    <br />

    <asp:Label ID="lblMensagemPesquisa" CssClass="fonte-vermelha" runat="server"></asp:Label>

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
                    ImageUrl='<%# Eval("Foto") %>' />

                <div class="dados-autonomo">

                    <h5>
                        <%#: Eval("Nome") %>
                    </h5>
                    <p>
                        <%#: Eval("Email") %>
                    </p>

                </div>

                <asp:LinkButton
                    ID="btnSaibaMais"
                    runat="server"
                    Text="Saiba mais"
                    CssClass="btn btn-dark cor-roxa"
                    CommandName="Detalhes"
                    CommandArgument='<%# Eval("ID") %>' />

            </div>

        </itemtemplate>

    </asp:Repeater>

    <asp:HiddenField
        ID="hfUsuarioDenunciadoID"
        runat="server" />

    <div class="modal fade"
        id="modalDado"
        tabindex="-1"
        aria-hidden="true">

        <div class="modal-dialog modal-xl modal-dialog-centered modal-dialog-scrollable">

            <div class="modal-content">

                <div class="modal-header">

                    <h5 class="modal-title">Conta do u suário
                    </h5>

                    <button
                        type="button"
                        class="btn-close"
                        data-bs-dismiss="modal"
                        aria-label="Fechar">
                    </button>

                </div>

                <div class="modal-body">

                    <!-- DADOS PRINCIPAIS -->

                    <div class="text-center mb-4">

                        <br />

                        <asp:Image
                            ID="imgPerfil"
                            runat="server"
                            CssClass="foto-autonomo" />

                        <h4 class="mt-3">
                            <asp:Label
                                ID="lblNomeAutonomo"
                                runat="server" />
                        </h4>

                        <p class="mt-2">
                            <asp:Label
                                ID="lblNotaAutonomo"
                                runat="server"
                                CssClass="text-secondary" />
                        </p>

                        <p class="mt-2">
                            <asp:Label
                                ID="lblTelefone"
                                runat="server"
                                CssClass="text-secondary" />
                        </p>

                        <p class="mt-2">
                            <asp:Label
                                ID="lblEmail"
                                runat="server"
                                CssClass="text-secondary" />
                        </p>

                        <br />

                        <asp:Label
                            ID="lblDescricaoAutonomo"
                            runat="server" />

                    </div>

                    <div class="card-titulo">

                        <hr />

                        <!-- SERVIÇOS -->

                        <h5>Serviços oferecidos</h5>

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

                    <hr />

                    <!-- REGIÕES -->

                    <h5>Regiões de atendimento</h5>

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

                <hr />

                <!-- PORTFÓLIOS -->

                <h5>Portfólios</h5>

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

                <hr />

                <!-- AVALIAÇÕES -->

                <h5>Avaliações recebidas</h5>

            </div>

            <asp:HiddenField
                ID="hfDenunciadoSelecionado"
                runat="server" />

            <asp:Repeater
                ID="rptAvaliacoesPerfil"
                runat="server">

                <itemtemplate>

                    <div class="card-avaliacao">

                        <asp:LinkButton
                            ID="lbtDenunciarUsuario"
                            runat="server"
                            Text="Denunciar"
                            CssClass="btn btn-danger"
                            CommandArgument='<%# Eval("UsuarioID") %>'
                            OnClick="DenunciarUsuario_Click" />

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
</asp:Content>
