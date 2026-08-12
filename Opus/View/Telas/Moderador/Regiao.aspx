<%@ page title="Regiões - Opus"
    language="C#"
    masterpagefile="~/View/Site.Master"
    autoeventwireup="true"
    codebehind="Regiao.aspx.cs"
    inherits="Opus.View.Telas.Moderador.Regiao" %>

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

        <columns>

            <asp:BoundField
                DataField="ID"
                HeaderText="Código" />

            <asp:BoundField
                DataField="Nome"
                HeaderText="Estado" />

            <asp:TemplateField HeaderText="Ações">
                <itemtemplate>

                    <asp:Button
                        ID="btnExcluirEstado"
                        runat="server"
                        Text="Excluir"
                        CssClass="btn btn-danger btn-sm"
                        CommandName="Delete"
                        OnClientClick="return confirm('Deseja excluir esta cidade?');" />

                </itemtemplate>
            </asp:TemplateField>

        </columns>

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

        <columns>

            <asp:BoundField
                DataField="ID"
                HeaderText="Código" />

            <asp:BoundField
                DataField="Estado"
                HeaderText="Estado" />

            <asp:BoundField
                DataField="Cidade"
                HeaderText="Cidade" />

            <asp:TemplateField HeaderText="Ações">
                <itemtemplate>

                    <asp:Button
                        ID="btnExcluirCidade"
                        runat="server"
                        Text="Excluir"
                        CssClass="btn btn-danger btn-sm"
                        CommandName="Delete"
                        OnClientClick="return confirm('Deseja excluir esta cidade?');" />

                </itemtemplate>
            </asp:TemplateField>

        </columns>

    </asp:GridView>

</asp:Content>