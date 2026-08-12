<%@ Page Title="Regiões - Opus"
    Language="C#"
    MasterPageFile="~/View/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Regiao.aspx.cs"
    Inherits="Opus.View.Telas.Moderador.Regiao" %>

<asp:Content
    ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>

<asp:Content
    ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <center>
        <h2>Gerenciamento de Regiões</h2>
    </center>

    <br />

    <!-- ===================================================== -->
    <!-- ESTADO -->
    <!-- ===================================================== -->

    <h5>Cadastrar Estado</h5>

    <br />

    <div class="mb-3">

        <label class="form-label">
            Estado
        </label>

        <asp:TextBox
            ID="tbxEstado"
            runat="server"
            CssClass="form-control">
        </asp:TextBox>

    </div>

    <center>

        <asp:Button
            ID="btnCadastrarEstado"
            runat="server"
            Text="Cadastrar Estado"
            CssClass="btn cor-roxa btn-dark"
            OnClick="CadastrarEstado" />

    </center>

    <br />

    <!-- GRID DOS ESTADOS -->

    <asp:GridView
        ID="gvEstados"
        runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="ID"
        CssClass="table table-striped table-bordered"
        OnRowDeleting="gvEstados_RowDeleting">

        <Columns>

            <asp:BoundField
                DataField="ID"
                HeaderText="Código" />

            <asp:BoundField
                DataField="Nome"
                HeaderText="Estado" />

            <asp:CommandField
                ShowDeleteButton="True"
                DeleteText="Excluir"
                ButtonType="Button" />

        </Columns>

    </asp:GridView>

    <br />

    <hr />

    <br />

    <!-- ===================================================== -->
    <!-- CIDADE -->
    <!-- ===================================================== -->

    <h5>Cadastrar Cidade</h5>

    <br />

    <div class="mb-3">

        <label class="form-label">
            Estado
        </label>

        <asp:DropDownList
            ID="ddlEstado"
            runat="server"
            CssClass="form-select">
        </asp:DropDownList>

    </div>

    <div class="mb-3">

        <label class="form-label">
            Cidade
        </label>

        <asp:TextBox
            ID="tbxCidade"
            runat="server"
            CssClass="form-control">
        </asp:TextBox>

    </div>

    <center>

        <asp:Button
            ID="btnCadastrarCidade"
            runat="server"
            Text="Cadastrar Cidade"
            CssClass="btn cor-roxa btn-dark"
            OnClick="CadastrarCidade" />

    </center>

    <br />

    <!-- GRID DAS CIDADES -->

    <asp:GridView
        ID="gvCidades"
        runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="ID"
        CssClass="table table-striped table-bordered"
        OnRowDeleting="gvCidades_RowDeleting">

        <Columns>

            <asp:BoundField
                DataField="ID"
                HeaderText="Código" />

            <asp:BoundField
                DataField="Estado"
                HeaderText="Estado" />

            <asp:BoundField
                DataField="Cidade"
                HeaderText="Cidade" />

            <asp:CommandField
                ShowDeleteButton="True"
                DeleteText="Excluir"
                ButtonType="Button" />

        </Columns>

    </asp:GridView>

</asp:Content>