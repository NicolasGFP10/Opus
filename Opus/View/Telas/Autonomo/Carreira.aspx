<%@ Page Title="Carreira - Opus"
    Language="C#"
    MasterPageFile="~/View/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Carreira.aspx.cs"
    Inherits="Opus.View.Telas.Autonomo.Carreira" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>

<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <center>
        <h2>Carreira</h2>
    </center>

    <br />

    <!-- ===================================================== -->
    <!-- SERVIÇOS -->
    <!-- ===================================================== -->

    <h5>Serviços oferecidos</h5>

    <br />

    <div class="mb-3">

        <label class="form-label">
            Serviço
        </label>

        <asp:DropDownList
            ID="ddlServico"
            runat="server"
            CssClass="form-select">
        </asp:DropDownList>

    </div>

    <asp:Button
        ID="btnAdicionarServico"
        runat="server"
        Text="Adicionar Serviço"
        CssClass="btn cor-roxa btn-dark"
        OnClick="btnAdicionarServico_Click" />

    <br />
    <br />

    <asp:GridView
        ID="gvServicos"
        runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="ID"
        CssClass="table table-striped table-bordered"
        OnRowDeleting="gvServicos_RowDeleting">

        <Columns>

            <asp:BoundField
                DataField="Nome"
                HeaderText="Serviço" />

            <asp:CommandField
                ShowDeleteButton="True"
                DeleteText="Remover"
                ButtonType="Button" />

        </Columns>

    </asp:GridView>

    <br />

    <hr />

    <br />

    <!-- ===================================================== -->
    <!-- REGIÕES DE ATENDIMENTO -->
    <!-- ===================================================== -->

    <h5>Regiões de atendimento</h5>

    <p>
        Selecione os locais onde você oferece seus serviços.
    </p>

    <br />

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

    <asp:Button
        ID="btnSalvarCidade"
        runat="server"
        Text="Adicionar Cidade"
        CssClass="btn cor-roxa btn-dark"
        OnClick="btnSalvarCidade_Click" />

    <br />
    <br />

    <!-- CIDADES ESCOLHIDAS -->

    <asp:GridView
        ID="gvRegiao"
        runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="ID"
        CssClass="table table-striped table-bordered"
        OnRowDeleting="gvRegiao_RowDeleting">

        <Columns>

            <asp:BoundField
                DataField="Estado"
                HeaderText="Estado" />

            <asp:BoundField
                DataField="Cidade"
                HeaderText="Cidade" />

            <asp:CommandField
                ShowDeleteButton="True"
                DeleteText="Remover"
                ButtonType="Button" />

        </Columns>

    </asp:GridView>

</asp:Content>
