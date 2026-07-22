<%@ Page Title="Regiões - Opus"
    Language="C#"
    MasterPageFile="~/View/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Regiao.aspx.cs"
    Inherits="Opus.View.Telas.Moderador.Regiao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <center>
        <h2>Cadastro de Estados e Cidades</h2>
    </center>

    <hr />

    <!-- ========================= ESTADO ========================= -->

    <h4>Cadastrar Estado</h4>

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

    <asp:Button
        ID="btnCadastrarEstado"
        runat="server"
        Text="Cadastrar Estado"
        CssClass="btn cor-roxa btn-dark"
        OnClick="btnCadastrarEstado_Click" />

    <br />
    <br />

    <asp:GridView
        ID="gvEstados"
        runat="server"
        CssClass="table table-striped table-bordered"
        AutoGenerateColumns="False"
        DataKeyNames="ID"
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

    <hr />

    <!-- ========================= CIDADE ========================= -->

    <h4>Cadastrar Cidade</h4>

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

    <asp:Button
        ID="btnCadastrarCidade"
        runat="server"
        Text="Cadastrar Cidade"
        CssClass="btn cor-roxa btn-dark"
        OnClick="btnCadastrarCidade_Click" />

    <br />
    <br />

    <asp:GridView
        ID="gvCidades"
        runat="server"
        CssClass="table table-striped table-bordered"
        AutoGenerateColumns="False"
        DataKeyNames="ID"
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