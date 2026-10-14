<%@ page title="Serviços - Opus" language="C#" masterpagefile="~/View/Site.Master" autoeventwireup="true" codebehind="Servicos.aspx.cs" inherits="Opus.View.Telas.Usuario.Servicos" %>

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
            <asp:LinkButton ID="btnPesquisar" runat="server" CssClass="btn btn-dark cor-roxa botao" height="37px">
                <asp:Image runat="server" ImageUrl="../../Img/IconePesquisa.png"></asp:Image>
            </asp:LinkButton>

        </div>
    </div>

    <hr />
</asp:Content>
