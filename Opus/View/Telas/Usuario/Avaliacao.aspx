<%@ Page Title="Avaliações - Opus" Language="C#" MasterPageFile="~/View/Site.Master" AutoEventWireup="true" CodeBehind="Avaliacao.aspx.cs" Inherits="Opus.View.Telas.Usuario.Avaliacao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <center>
        <h2>Avaliação</h2>
    </center>

    <div class="d-grid gap-2 d-md-flex justify-content-md-end" style="padding-top: 20px; padding-right: 30px;">

        <asp:Label ID="lblTexto" runat="server" Text="Gerar código de Avaliação" Visible="false"></asp:Label>

        <asp:Label ID="lblCodigo" runat="server" Text="" CssClass="fonte-vermelha" Visible="false"></asp:Label>

        <asp:Button ID="btnGerar" runat="server" Text="Gerar" CssClass="btn cor-roxa btn-dark" Visible="false" OnClick="btnGerar_Click" />

    </div>

    <label class="form-label">
        Código de Avaliação
    </label>

    <asp:TextBox
        ID="tbxCodigoAvaliacao"
        runat="server"
        MaxLength="6"
        type="number"
        CssClass="form-control">
    </asp:TextBox>

    <asp:Label
        ID="lblErroCodigo"
        runat="server"
        CssClass="text-danger"
        Visible="false"></asp:Label>
    <br />

    <center>
        <asp:Button
            ID="btnAvaliar"
            runat="server"
            Text="Realizar avaliação"
            CssClass="btn cor-roxa btn-dark"
            OnClick="btnAvaliar_Click" />
    </center>

    <br />

    <hr />

    <br />

    <!-- REPEATER DAS AVALIAÇÕES DO USUÁRIO -->

    <h5 class="padding-lat-45">Avaliações que você fez</h5>

    <asp:Repeater
        ID="rptAvaliacoesUsuario"
        runat="server">

        <ItemTemplate>

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

                        <ItemTemplate>

                            <asp:Image
                                runat="server"
                                CssClass="foto-avaliacao img-thumbnail"
                                ImageUrl='<%# Eval("Imagem") %>' />

                        </ItemTemplate>

                    </asp:Repeater>

                </div>

            </div>

        </ItemTemplate>

    </asp:Repeater>

    <!-- Modal -->
    <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Avalie o Serviço</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">

                    <label class="form-label">
                        Título da avaliação
                    </label>

                    <div class="mb-3">
                        <asp:TextBox ID="tbxTitulo" class="form-control" runat="server" CssClass="form-control telefone"></asp:TextBox>
                    </div>

                    <label class="form-label">
                        Descreva o serviço
                    </label>

                    <div class="mb-3">
                        <asp:TextBox ID="tbxDescricao" class="form-control" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </div>

                    <label class="form-label">
                        Nota de 1 a 5 para o serviço
                    </label>
                    <br />
                    <center>
                        <div class="form-check form-check-inline">
                            <asp:RadioButton ID="rb1" runat="server"
                                GroupName="Avaliacao" />
                            <label class="form-check-label" for="rb1">1</label>
                        </div>

                        <div class="form-check form-check-inline">
                            <asp:RadioButton ID="rb2" runat="server"
                                GroupName="Avaliacao" />
                            <label class="form-check-label" for="rb2">2</label>
                        </div>

                        <div class="form-check form-check-inline">
                            <asp:RadioButton ID="rb3" runat="server"
                                GroupName="Avaliacao" />
                            <label class="form-check-label" for="rb3">3</label>
                        </div>

                        <div class="form-check form-check-inline">
                            <asp:RadioButton ID="rb4" runat="server"
                                GroupName="Avaliacao" />
                            <label class="form-check-label" for="rb4">4</label>
                        </div>

                        <div class="form-check form-check-inline">
                            <asp:RadioButton ID="rb5" runat="server"
                                GroupName="Avaliacao" />
                            <label class="form-check-label" for="rb5">5</label>
                        </div>
                    </center>
                    <br />
                    <label class="form-label">
                        Fotos do serviço (até 5 fotos)
                    </label>

                    <asp:FileUpload
                        ID="fuFotos"
                        runat="server"
                        AllowMultiple="true"
                        CssClass="form-control"
                        accept=".jpg,.jpeg,.png,.webp" />

                </div>
                <div class="modal-footer">

                    <button type="button" class="btn btn-dark" data-bs-dismiss="modal" style="width: 200px;">Fechar</button>

                    <asp:Button runat="server" ID="btnEnviar" type="button" class="btn btn-dark cor-roxa" OnClick="btnEnviar_Click" Text="Enviar avaliação" Width="200px" />

                    <br />
                </div>
            </div>
        </div>
    </div>

    <br />
    <br />

    <asp:HiddenField
        ID="hfDenunciadoSelecionado"
        runat="server" />

    <asp:Label ID="lblTituloAutonomo" Visible="false" runat="server" Text="">

        <br />
        <hr />
        <br />

        <h5>Avaliações que você recebeu</h5>

    </asp:Label>

    <asp:Repeater
        ID="rptAvaliacoes"
        runat="server">

        <ItemTemplate>

            <div class="card-avaliacao">

                <!-- Cabeçalho -->
                <div class="avaliacao-cabecalho">

                    <asp:LinkButton
                        ID="lbtDenunciarUsuario"
                        runat="server"
                        Text="Denunciar"
                        CssClass="btn btn-danger"
                        CommandArgument='<%# Eval("UsuarioID") %>'
                        OnClick="DenunciarUsuario_Click" />

                    <span class="nome-usuario">
                        <%# Eval("NomeUsuario") %>
                    </span>

                </div>

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

                        <ItemTemplate>

                            <asp:Image
                                runat="server"
                                CssClass="foto-avaliacao img-thumbnail"
                                ImageUrl='<%# Eval("Imagem") %>' />

                        </ItemTemplate>

                    </asp:Repeater>

                </div>

            </div>

        </ItemTemplate>

    </asp:Repeater>

    <!-- MODAL DE DENÚNCIA -->

    <div class="modal fade"
        id="modalDenunciaAutonomo"
        tabindex="-1"
        aria-hidden="true">

        <div class="modal-dialog modal-xl modal-dialog-centered modal-dialog-scrollable">

            <div class="modal-content">

                <div class="modal-header">

                    <h5 class="modal-title">Gerar denúncia
                    </h5>

                    <button
                        type="button"
                        class="btn-close"
                        data-bs-dismiss="modal"
                        aria-label="Fechar">
                    </button>

                </div>

                <div class="modal-body">
                    <div class="card-titulo">
                        <p>
                            (Após enviar a denuncia, fique atento ao seu e-mail, pois você receberá um retorno da nossa equipe de suporte)
                        </p>
                    </div>

                    <!-- Assunto -->

                    <div class="card-titulo">

                        <div class="mb-3">
                            <label for="exampleFormControlInput1" class="form-label">Título</label>
                            <asp:TextBox ID="tbxTituloDenuncia" type="text" class="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div class="mb-3">

                            <label for="exampleFormControlInput1" class="form-label">Escolha o que aconteceu</label>

                            <asp:DropDownList ID="ddlCategoria"
                                AutoPostBack="false"
                                runat="server"
                                CssClass="form-select">

                                <asp:ListItem Selected="True" Value="0">Escolha uma opção abaixo </asp:ListItem>
                                <asp:ListItem Value="1">Assédio sexual </asp:ListItem>
                                <asp:ListItem Value="2">Discriminação </asp:ListItem>
                                <asp:ListItem Value="3">Ameaças ou violência</asp:ListItem>
                                <asp:ListItem Value="4">Linguagem ofensiva </asp:ListItem>
                                <asp:ListItem Value="5">Fraude ou golpe</asp:ListItem>
                                <asp:ListItem Value="6">Não fornecimento do código de avaliação</asp:ListItem>
                                <asp:ListItem Value="7">Perfil falso ou informações enganosas</asp:ListItem>
                                <asp:ListItem Value="8">Cobrança indevida </asp:ListItem>
                                <asp:ListItem Value="9">Uso indevido de dados pessoais </asp:ListItem>
                                <asp:ListItem Value="10">Spam ou publicidade indevida</asp:ListItem>
                                <asp:ListItem Value="11">Outro</asp:ListItem>

                            </asp:DropDownList>

                        </div>

                        <div class="mb-3">
                            <label for="exampleFormControlInput1" class="form-label">Descreva o que aconteceu</label>
                            <asp:TextBox ID="tbxDescricaoDenuncia" TextMode="MultiLine" Rows="3" type="text" class="form-control" runat="server"></asp:TextBox>
                        </div>

                        <label class="form-label">
                            Fotos de prova (opcional)
                        </label>

                        <asp:FileUpload
                            ID="fuDenuncia"
                            runat="server"
                            AllowMultiple="true"
                            CssClass="form-control"
                            accept=".jpg,.jpeg,.png,.webp" />

                        <div class="padding-top-20 text-end">
                            <asp:Button runat="server" Text="Enviar" CssClass="btn btn-dark cor-roxa" OnClick="btnEnviarDenuncia_Click" />
                        </div>
                    </div>

                </div>

                <div class="modal-footer">
                </div>

            </div>

        </div>

    </div>

</asp:Content>
