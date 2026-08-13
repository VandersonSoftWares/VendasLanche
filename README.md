🍔 MVCLanche
Aplicação web de pedidos desenvolvida com ASP.NET Core MVC, utilizando C#, Entity Framework Core, SQL Server e ASP.NET Core Identity.
O projeto foi desenvolvido com o objetivo de colocar em prática conceitos de desenvolvimento web no ecossistema .NET, desde o cadastro de produtos e categorias até o processamento de pedidos, administração, dashboard de vendas e geração de relatórios em Excel.
________________________________________
📌 Sobre o Projeto
O MVCLanche é um sistema completo para gerenciamento e realização de pedidos de lanches.
A aplicação possui uma área destinada aos clientes, onde é possível visualizar produtos, navegar por categorias, adicionar itens ao carrinho e realizar pedidos.
Também possui uma Área Administrativa, protegida por autenticação e autorização, permitindo o gerenciamento dos produtos, categorias e pedidos, além de apresentar indicadores e gráficos de vendas.
________________________________________
🚀 Principais Funcionalidades
👤 Área do Cliente
•	Página inicial 
•	Listagem de lanches 
•	Filtro por categoria 
•	Busca de produtos 
•	Carrinho de compras 
•	Inclusão e alteração de quantidade de itens 
•	Finalização de pedidos 
•	Visualização dos pedidos realizados 
•	Cadastro e autenticação de usuários 
🔐 Área Administrativa
•	Autenticação de administradores 
•	Controle de acesso por perfil 
•	Gerenciamento de categorias 
•	Gerenciamento de lanches 
•	Cadastro, edição e exclusão de produtos 
•	Upload de imagens 
•	Gerenciamento de pedidos 
•	Visualização dos detalhes dos pedidos 
•	Paginação de pedidos 
📊 Dashboard
Dashboard administrativo com visualização gráfica das informações de vendas:
•	Vendas dos últimos 30 dias 
•	Vendas dos últimos 360 dias 
•	Produtos mais vendidos 
•	Faturamento por período 
 
📥 Exportação para Excel
O sistema permite exportar os pedidos para um relatório Excel (.xlsx).
O relatório apresenta:
•	Um pedido por linha 
•	Número do pedido 
•	Data do pedido 
•	Dados do cliente 
•	E-mail 
•	Telefone 
•	Endereço 
•	Complemento 
•	CEP 
•	Cidade 
•	Estado 
•	Total de itens 
•	Valor total do pedido 
•	Filtros nas colunas 
•	Cabeçalho congelado 
•	Formatação de valores monetários 
•	Resumo final 
•	Total de pedidos 
•	Total de itens 
•	Faturamento total 
•	Data e hora da exportação 
________________________________________
📸 Screenshots
🛒 Compra / Pedido
Tela de confirmação do pedido, apresentando os produtos selecionados, quantidades, preços e valor total.
________________________________________
🧾 Administração de Pedidos
Tela administrativa para acompanhamento dos pedidos realizados, com paginação, valores, datas e acesso aos detalhes.
 
________________________________________
📊 Relatório de Pedidos
Relatório exportado para Excel contendo os pedidos e um resumo geral com quantidade de pedidos, itens e faturamento.
 
________________________________________
🏠 Página Inicial
 ________________________________________
🛡️ Área Administrativa
 ________________________________________
📈 Dashboard
 ________________________________________
🛠️ Tecnologias Utilizadas
Backend
•	C# 
•	ASP.NET Core MVC 
•	.NET 
•	Entity Framework Core 
•	ASP.NET Core Identity 
•	LINQ 
Banco de Dados
•	SQL Server 
•	Entity Framework Core Migrations 
Frontend
•	HTML5 
•	CSS3 
•	Bootstrap 5 
•	Bootstrap Icons 
•	JavaScript 
•	jQuery 
Visualização de Dados
•	Google Charts 
Relatórios
•	ClosedXML 
•	Excel 
Desenvolvimento
•	Visual Studio 
•	Git / GitHub 
________________________________________
🏗️ Estrutura do Projeto
O projeto utiliza a arquitetura ASP.NET Core MVC, organizada em diferentes responsabilidades.
MVCLanche
│
├── Areas
│   └── Admin
│       ├── Controllers
│       └── Views
│
├── Context
│
├── Controllers
│
├── Models
│
├── Repositories
│   └── Interfaces
│
├── Services
│
├── ViewModels
│
├── Views
│
├── wwwroot
│   ├── css
│   ├── js
│   └── images
│
├── appsettings.json
├── Program.cs
├── .gitignore
└── MVCLanche.sln
________________________________________
🔐 Autenticação e Autorização
O projeto utiliza ASP.NET Core Identity para gerenciamento de usuários e autenticação.
A Área Administrativa utiliza autorização baseada em função, permitindo que somente usuários com a função Admin tenham acesso às funcionalidades administrativas.
Também foram configurados:
•	Regras de senha 
•	Bloqueio após tentativas de login inválidas 
•	Controle de sessão 
•	Autorização por função 
•	Tokens padrão do Identity 
•	Temos 2 Usuários @Vanderson1, Admin@localhost Senha: Numsey#2026
________________________________________
🗄️ Banco de Dados
O sistema utiliza SQL Server com Entity Framework Core. .NET9
O AppDbContext integra as entidades da aplicação com o ASP.NET Core Identity.
Principais entidades:
Lanche
Categoria
CarrinhoCompraItem
Pedido
PedidoDetalhe
IdentityUser
IdentityRole
________________________________________
🧩 Organização e Boas Práticas
Durante o desenvolvimento foram utilizados diversos conceitos e recursos do ecossistema .NET:
•	MVC 
•	Dependency Injection 
•	Repository Pattern 
•	Services 
•	ViewModels 
•	Entity Framework Core 
•	LINQ 
•	ASP.NET Core Identity 
•	Role-based Authorization 
•	Session 
•	Data Annotations 
•	Upload de arquivos 
•	Validação de dados 
•	Localização pt-BR 
•	Geração de relatórios 
•	Visualização de dados através de gráficos 
________________________________________
⚙️ Como Executar o Projeto
Pré-requisitos
Para executar o projeto localmente é necessário possuir:
•	Visual Studio 
•	.NET SDK compatível com o projeto 
•	SQL Server 
•	SQL Server Express ou outra instância SQL Server compatível 
Configuração do Banco de Dados
A conexão com o banco de dados deve ser configurada localmente.
O projeto utiliza:
appsettings.json
para configurações gerais e:
appsettings.Development.json
para configurações específicas do ambiente de desenvolvimento.
⚠️ O arquivo appsettings.Development.json não deve ser enviado para o repositório, pois pode conter informações específicas do ambiente local.
Executando
1.	Clone o repositório. 
2.	Abra a solução: 
MVCLanche.sln
3.	Configure a conexão com o SQL Server. 
4.	Execute as migrations do Entity Framework Core, caso necessário. 
5.	Execute o projeto através do Visual Studio. 
________________________________________
🗂️ Relatório Excel
A aplicação possui uma funcionalidade específica para geração de relatórios administrativos.
O relatório apresenta os pedidos de forma consolidada, mantendo um pedido por linha, mesmo quando o pedido possui vários produtos.
Ao final da planilha são apresentados:
Total de Pedidos
Total de Itens
Faturamento Total
Data da Exportação
Essa funcionalidade foi desenvolvida utilizando a biblioteca ClosedXML.
________________________________________
📊 Dashboard Administrativo
O Dashboard apresenta uma visão geral das vendas através de gráficos.
Os indicadores disponíveis incluem:
Vendas - Últimos 30 dias
Vendas - Últimos 360 dias
Produtos Mais Vendidos
Faturamento por Período
O Dashboard também disponibiliza a exportação dos pedidos para Excel.
________________________________________
🎯 Objetivo do Projeto
O principal objetivo deste projeto foi desenvolver uma aplicação web completa utilizando tecnologias do ecossistema .NET, aplicando na prática conceitos de arquitetura, persistência de dados, autenticação, autorização, desenvolvimento de interfaces e geração de relatórios.
O projeto também serviu como oportunidade para evoluir conhecimentos em ASP.NET Core MVC, C#, Entity Framework Core, SQL Server e desenvolvimento de aplicações web.
________________________________________
🚀 Possíveis Evoluções
O projeto foi desenvolvido de forma que novas funcionalidades possam ser adicionadas futuramente.
Algumas possibilidades:
•	Novos indicadores no Dashboard 
•	Novos tipos de relatórios 
•	Filtros avançados 
•	Melhorias de responsividade 
•	Novas funcionalidades administrativas 
•	Integrações com serviços externos 
________________________________________
👨‍💻 Autor
Vanderson Cavalcante Freitas – f.c.vanderson@gmail.com
Projeto desenvolvido para fins de estudo, prática e portfólio profissional C#.
Como eu me encontro Desempregado, se o(a) Amigo(a), quiser me ajudar por favor me envie um Pix de qualquer valor, que ficarei imensamente grato.
Vanderson Cavalcante de Freitas
Banco Itau
Pix 22693255830.

