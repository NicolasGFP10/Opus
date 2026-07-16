<%@ Page Title="Opções - Opus" Language="C#" MasterPageFile="~/View/Site.Master" AutoEventWireup="true" CodeBehind="Configuracao.aspx.cs" Inherits="Opus.View.Telas.Usuario.Configuracao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <center>
        <asp:Button runat="server" ID="btnEdit" OnClick="btnEdit_Click" type="button" class="btn btn-dark cor-roxa" Text="Editar dados" Width="200px" /></center>
    <br />

    <center>
        <asp:Button runat="server" ID="btnAuto" OnClick="btnAuto_Click" type="button" class="btn btn-dark cor-roxa" Text="Tornar-se autônomo" Width="200px" /></center>
    <br />
    <br />
    <br />

    <center>
        <asp:Button runat="server" ID="btnSair" OnClick="btnSair_Click" type="button" class="btn btn-danger" Text="Sair" Width="200px" /></center>
    <br />

    <!-- Button trigger modal -->
    <center><button type="button" class="btn btn-danger" style=" width: 200px;" data-bs-toggle="modal"  data-bs-target="#exampleModal">
        Desativar Conta
    </button></center>

    <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Desativar conta</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    Tem certeza que deseja desativar sua conta? Ao desativar sua conta, você não poderá mais acessar o sistema.
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-dark cor-roxa" data-bs-dismiss="modal" style=" width: 200px;">Fechar</button>
                    <center>
                        <asp:Button runat="server" ID="btnDesativar" OnClick="btnDesativar_Click" type="button" class="btn btn-danger" Text="Desativar conta" Width="200px" /></center>
                    <br />
                </div>
            </div>
        </div>
    </div>



</asp:Content>
