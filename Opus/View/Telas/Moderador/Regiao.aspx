<%@ Page Title="Cadastrar Regiões - Opus" Language="C#" MasterPageFile="~/View/Site.Master" AutoEventWireup="true" CodeBehind="Regiao.aspx.cs" Inherits="Opus.View.Telas.Moderador.Regiao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <h2>Cadastrar Regiões</h2>
    </center>

    <div class="mb-3">
        <label for="exampleFormControlInput1" class="form-label">Estado</label>
        <asp:TextBox ID="tbxEstado" type="text" class="form-control" runat="server"></asp:TextBox>
    </div>

    <div class="mb-3">
        <label for="exampleFormControlInput1" class="form-label">Cidade</label>
        <asp:TextBox ID="tbxCidade" type="text" class="form-control" runat="server"></asp:TextBox>
    </div>
    <center>
        <asp:Button runat="server" ID="btnEnviar" type="button" OnClick="CadastrarRegiao" Width="200px" class="btn cor-roxa btn-dark" Text="Enviar" /></center>
    <br />

    <asp:GridView
        ID="gvRegioes"
        runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="ID"
        CssClass="table table-striped"
        OnRowEditing="gvRegioes_RowEditing"
        OnRowCancelingEdit="gvRegioes_RowCancelingEdit"
        OnRowUpdating="gvRegioes_RowUpdating"
        OnRowDeleting="gvRegioes_RowDeleting">

        <Columns>

            <asp:BoundField
                DataField="ID"
                HeaderText="Código"
                ReadOnly="true" />

            <asp:BoundField
                DataField="Estado"
                HeaderText="Estado" />

            <asp:BoundField
                DataField="Cidade"
                HeaderText="Cidade" />

            <asp:TemplateField HeaderText="Ações">

                <ItemTemplate>

                    <asp:Button
                        ID="btnEditar"
                        runat="server"
                        Text="Editar"
                        CssClass="btn btn-dark cor-roxa btn-sm"
                        CommandName="Edit" />

                    <asp:Button
                        ID="btnExcluir"
                        runat="server"
                        Text="Excluir"
                        CssClass="btn btn-danger btn-sm"
                        CommandName="Delete"
                        OnClientClick="return confirm('Deseja excluir?');" />

                </ItemTemplate>

                <EditItemTemplate>

                    <asp:Button
                        ID="btnSalvar"
                        runat="server"
                        Text="Salvar"
                        CssClass="btn btn-dark cor-roxa btn-sm"
                        CommandName="Update" />

                    <asp:Button
                        ID="btnCancelar"
                        runat="server"
                        Text="Cancelar"
                        CssClass="btn btn-danger btn-sm"
                        CommandName="Cancel" />

                </EditItemTemplate>

            </asp:TemplateField>

        </Columns>

    </asp:GridView>
    <br />

    <div class="mb-3">
        <asp:Label ID="lblEditEstado" CssClass="form-label" runat="server" Text="Editar Estado" Visible="false"></asp:Label>
        <asp:TextBox ID="tbxEditEstado" type="text" class="form-control" runat="server" Visible="false"></asp:TextBox>
    </div>

    <div class="mb-3">
        <asp:Label ID="lblEditCidade" CssClass="form-label" runat="server" Text="Editar Cidade" Visible="false"></asp:Label>
        <asp:TextBox ID="tbxEditCidade" type="text" class="form-control" runat="server" Visible="false"></asp:TextBox>
    </div>
    <center>
        <asp:Button runat="server" ID="btnConfirmar" type="button" Width="200px" class="btn cor-roxa btn-dark" Text="Editar" Visible="false" /></center>
    <br />

</asp:Content>
