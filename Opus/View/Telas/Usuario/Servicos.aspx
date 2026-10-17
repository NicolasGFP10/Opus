<%@ Page Title="Serviços - Opus" Language="C#" MasterPageFile="~/View/Site.Master" AutoEventWireup="true" CodeBehind="Servicos.aspx.cs" Inherits="Opus.View.Telas.Usuario.Servicos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <center>
        <h2>Serviços
        </h2>
    </center>

    <br />

    <div class="caixa-pesquisa">

        <div class="servico">

            <div class="mb-3">

                <label class="form-label">
                    Tipo de serviço
                </label>

                <asp:DropDownList
                    ID="ddlServico"
                    runat="server"
                    CssClass="form-select"
                    PlaceHolder="Escolha o Serviço">
                </asp:DropDownList>

            </div>

        </div>

        <div class="estado">

            <!-- ESTADO -->

            <div class="mb-3">

                <label class="form-label">
                    Estado
                </label>

                <asp:DropDownList
                    ID="ddlEstado"
                    runat="server"
                    CssClass="form-select"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
                </asp:DropDownList>

            </div>

        </div>

        <div class="cidade">

            <!-- CIDADE -->

            <div class="mb-3">

                <label class="form-label">
                    Cidade
                </label>

                <asp:DropDownList
                    ID="ddlCidade"
                    runat="server"
                    CssClass="form-select"
                    Enabled="false">
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

    <hr />

    <asp:Label
        ID="lblMensagemPesquisa"
        runat="server"
        CssClass="text-danger" />

    <asp:Repeater
        ID="rptAutonomos"
        runat="server"
        OnItemCommand="rptAutonomos_ItemCommand">

        <ItemTemplate>

            <div class="card-autonomo">

                <asp:Image
                    ID="imgAutonomo"
                    runat="server"
                    CssClass="foto-autonomo"
                    ImageUrl='<%# Eval("Foto") %>' />

                <div class="dados-autonomo">

                    <h5>
                        <%#: Eval("Nome") %>
                    </h5>

                    <p>
                        <%# Convert.ToInt32(Eval("QuantidadeAvaliacoes")) == 0
                        ? "Sem avaliações"
                        : "Nota: " + String.Format("{0:N1}", Eval("NotaMedia")) + " / 5" %>
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

        </ItemTemplate>

    </asp:Repeater>

    <div class="modal fade"
        id="modalAutonomo"
        tabindex="-1"
        aria-hidden="true">

        <div class="modal-dialog modal-xl modal-dialog-centered modal-dialog-scrollable">

            <div class="modal-content">

                <div class="modal-header">

                    <h5 class="modal-title">Perfil do profissional
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

                    </div>



                    <asp:Repeater
                        ID="rptServicosPerfil"
                        runat="server">

                        <ItemTemplate>
                            <span class="badge bg-secondary me-2 mb-2">
                                <%#: Eval("Nome") %>
                            </span>
                        </ItemTemplate>

                    </asp:Repeater>

                    <div class="card-titulo">

                        <hr />

                        <!-- REGIÕES -->

                        <h5>Regiões de atendimento</h5>

                    </div>

                    <asp:Repeater
                        ID="rptRegioesPerfil"
                        runat="server">

                        <ItemTemplate>

                            <p>
                                <%#: Eval("Cidade") %> -
                            <%#: Eval("Estado") %>
                            </p>

                        </ItemTemplate>

                    </asp:Repeater>

                    <div class="card-titulo">

                        <hr />

                        <!-- PORTFÓLIOS -->

                        <h5>Portfólios</h5>

                    </div>


                    <asp:Repeater
                        ID="rptPortifoliosPerfil"
                        runat="server">

                        <ItemTemplate>

                            <div class="card-portifolio">

                                <p>
                                    <%#: Eval("Descricao") %>
                                </p>

                                <div class="portifolio-fotos">

                                    <asp:Repeater
                                        runat="server"
                                        DataSource='<%# Eval("Fotos") %>'>

                                        <ItemTemplate>

                                            <asp:Image
                                                runat="server"
                                                CssClass="portifolio-foto"
                                                ImageUrl='<%# Eval("Caminho") %>' />

                                        </ItemTemplate>

                                    </asp:Repeater>

                                </div>

                            </div>

                        </ItemTemplate>

                    </asp:Repeater>

                    <div class="card-titulo">

                        <hr />

                        <!-- AVALIAÇÕES -->

                        <h5>Avaliações recebidas</h5>

                    </div>

                    <asp:Repeater
                        ID="rptAvaliacoesPerfil"
                        runat="server">

                        <ItemTemplate>

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

                                        <ItemTemplate>

                                            <asp:Image
                                                runat="server"
                                                CssClass="foto-avaliacao"
                                                ImageUrl='<%# Eval("Imagem") %>' />

                                        </ItemTemplate>

                                    </asp:Repeater>

                                </div>

                            </div>

                        </ItemTemplate>

                    </asp:Repeater>

                </div>

                <div class="modal-footer">

                    <button
                        type="button"
                        class="btn btn-secondary"
                        data-bs-dismiss="modal">
                        Fechar

                    </button>

                </div>

            </div>

        </div>

    </div>

</asp:Content>
